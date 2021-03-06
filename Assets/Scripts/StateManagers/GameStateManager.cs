﻿using UnityEngine;
using System.Collections;
using GameManager;




namespace GameManager{
	//add more states as needed
	public enum GameState{
		MainMenu,
		Paused,
		Running, 
		Ended
	}



}
public  delegate void GameEvent();

public static class GameEvents{
	public static event GameEvent emitEaten; 

	public static void emitEatenEvent(){
		emitEaten();
	}
}

/// <summary>
/// This singleton persists across all scenes and is used to manage the game state overall.
/// DO stuff like switch levels, pause and resume, load assets, open network connects etc here.
/// </summary>
public class GameStateManager : MonoBehaviour {

	public static GameStateManager Instance;

	private GameState _state;

	public GameState State{
		get { return _state;}
		set{
			_switchState(value);
		}
	}

	public SimpleStateMachine _stateMachine;
	private SimpleState paused,running, mainMenu, ended;

	public GameObject PauseMenuCanvas, MainMenuCanvas, InGameCanvas, ScoreCanvas, FinalScoreCanvas; 


	public void Pause() {
		State = GameState.Paused;
	}

	#region MONOBEHAVIOUR_METHODS
	void Awake(){
		//Lazy Singleton
		Instance = this;
		
		// DontDestroyOnLoad(PauseMenuCanvas); 

		//Initialize State Machine
		paused =  new SimpleState(PausedEnter, PausedUpdate, PausedExit, "[GAME-STATE] :  PAUSED");
		running = new SimpleState(RunningEnter, RunningUpdate, RunningExit, "[GAME-STATE] :  RUNNING");
		mainMenu = new SimpleState(MainMenuEnter, MainMenuUpdate, MainMenuExit, "[GAME-STATE] : MAINMENU"); 
		ended = new SimpleState(EndedEnter, EndedUpdate, EndedExit, "[GAME-STATE] : ENDED"); 


	}

	void Start(){
		//Start the state machine
		State = GameState.MainMenu;
	}

	void Update(){
		_stateMachine.Execute();

		if(LoginUtilities.LoginManager.isLoggedIn()){
			Debug.Log("I AM LOGGED IN");
		}

	}
	#endregion


	#region PAUSED_STATE
	void PausedEnter(){
		Time.timeScale = 0; // stops the update loops
		PauseMenuCanvas.SetActive(true); 
	}

	void PausedUpdate(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			State = GameState.Running; 
			// figure out how set timescale back
		}
	}

	void PausedExit(){

	}
	#endregion


	#region RUNNING_STATE
	void RunningEnter(){
		Time.timeScale = 1; 
		PauseMenuCanvas.SetActive(false);
		InGameCanvas.SetActive(true);
		UnityTimer.Instance.CallAfterDelay(() => {
			MainMenuCanvas.SetActive(false);
			OrganManager.Instance.Start();
		}, 1f);

		//Enable Scoreboard
		UnityTimer.Instance.CallAfterDelay( () => {
			Scoreboard.Instance.Init();
		}, 1f);

	}

	void RunningUpdate(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Pause(); 
		}
	}

	void RunningExit(){

	}
	#endregion

	#region MAINMENU_STATE
	void MainMenuEnter(){
		Time.timeScale = 1; 

		//Enable Mainmenu
		MainMenuCanvas.SetActive(true);

		//Trigger Animation after delay, animation system triggers bool before cavas is active
		UnityTimer.Instance.CallAfterDelay(() => {

			MainMenuCanvas.GetComponent<Animator>().SetTrigger("ReloadMenu");

		}, 0.1f);

//		//Disable the finalScoreboard
//		FinalScoreCanvas.SetActive(false);
//
//		ZombieSpeedManager.Instance.ResetZombie();

	}
	
	void MainMenuUpdate(){

	}
	
	void MainMenuExit(){
		
	}
	#endregion


	#region ENDED_STATE
	void EndedEnter(){
		Time.timeScale = 1; 

		//Idle the Zombie so he doesn't move again
		ZombieStateManager.Instance.State = PlayerManager.PlayerState.Idle;

        FinalScoreCanvas.SetActive(true);
        FinalScoreManager.Instance.Init();
        FinalScoreManager.Instance.SetAll((int)ScoreManager.Instance.calculator.GetFinalScore(), ScoreManager.Instance.calculator.getHumanEaterPoint(),
            (int)ScoreManager.Instance.calculator.getMilesPoint(), ScoreManager.Instance.calculator.getButtonMashPoint());
        ScoreManager.Instance.calculator.UpdateScore();

        UIManager.instance.CleanUI();
        ScoreCanvas.SetActive(false);
        InGameCanvas.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        
    }
	
	void EndedUpdate(){
		
	}
	
	void EndedExit(){
		
	}
	#endregion


	/// <summary>
	/// Switches Game State 
	/// </summary>
	/// <param name="_targetState">Target state.</param>
	void _switchState(GameState _targetState){
		//Update private
		_state = _targetState;

		//Switch state of state machine
		switch(_targetState){
		case GameState.Paused:
			_stateMachine.SwitchStates(paused);
			break;
		case GameState.Running:
			_stateMachine.SwitchStates(running);
			break;
		case GameState.MainMenu:
			_stateMachine.SwitchStates(mainMenu);
			break;
		case GameState.Ended:
			_stateMachine.SwitchStates(ended);
			break;
		}
	}

	public void RestartGame(){
		//HACK: Reload
		Application.LoadLevel(0);
	}
}
