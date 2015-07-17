using UnityEngine;
using System.Collections;
using GameManager;




namespace GameManager{
	//add more states as needed
	public enum GameState{
		Paused,
		Running
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
	private SimpleState paused,running;


	#region MONOBEHAVIOUR_METHODS
	void Awake(){
		//Lazy Singleton
		Instance = this;

		//Keep Gameobject persistent across all scenes
		DontDestroyOnLoad(this.gameObject);

		//Initialize State Machine
		paused =  new SimpleState(PausedEnter, PausedUpdate, PausedExit, "[GAME-STATE] :  PAUSED");
		running = new SimpleState(RunningEnter, RunningUpdate, RunningExit, "[GAME-STATE] :  RUNNING");

		//Start the state machine
		State = GameState.Running;
	}

	void Update(){
		_stateMachine.Execute();
	}
	#endregion


	#region PAUSED_STATE
	void PausedEnter(){

	}

	void PausedUpdate(){
		
	}

	void PausedExit(){
		
	}
	#endregion


	#region RUNNING_STATE
	void RunningEnter(){

	}

	void RunningUpdate(){

	}

	void RunningExit(){

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
		}
	}
}
