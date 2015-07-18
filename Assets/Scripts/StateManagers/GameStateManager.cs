﻿using UnityEngine;
using System.Collections;
using GameManager;




namespace GameManager{
	//add more states as needed
	public enum GameState{
		MainMenu,
		Paused,
		Running
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
	private SimpleState paused,running, mainMenu;

	public GameObject PauseMenuCanvas, MainMenuCanvas, InGameCanvas; 


	public void Pause() {
		State = GameState.Paused;
	}

	#region MONOBEHAVIOUR_METHODS
	void Awake(){
		//Lazy Singleton
		Instance = this;

		//Keep Gameobject persistent across all scenes
		DontDestroyOnLoad(this.gameObject);
		// DontDestroyOnLoad(PauseMenuCanvas); 

		//Initialize State Machine
		paused =  new SimpleState(PausedEnter, PausedUpdate, PausedExit, "[GAME-STATE] :  PAUSED");
		running = new SimpleState(RunningEnter, RunningUpdate, RunningExit, "[GAME-STATE] :  RUNNING");
		mainMenu = new SimpleState(MainMenuEnter, MainMenuUpdate, MainMenuExit, "[GAME-STATE] : MAINMENU"); 

		//Start the state machine
		State = GameState.Running;
	}

	void Update(){
		_stateMachine.Execute();


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
		PauseMenuCanvas.SetActive(false);
		MainMenuCanvas.SetActive(false);
		InGameCanvas.SetActive(true);
	}
	
	void MainMenuUpdate(){

	}
	
	void MainMenuExit(){
		
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
		}
	}
}
