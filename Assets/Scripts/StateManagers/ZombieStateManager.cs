using UnityEngine;
using System.Collections;
using PlayerManager;

namespace PlayerManager{
	public enum PlayerState{
		Idle,
		Running,
		Ducking,
		Jumping,
		Eating,
		Resetting
	}
}

/// <summary>
/// A singleton controlling the player
/// </summary>
public class ZombieStateManager : MonoBehaviour {




	public CharacterController Controller{ get; private set;}
	
	#region MOVEMENT_PARAMS
	public Vector2 velocity;
	public Vector2 jumpVec;
	public Vector2 gravity;
	//Debug check in inspector
	public bool IsGrounded;

	//Position to return to after finishing eating
	private Vector3 _initialPosition;

	//Use the GameManager to set this to be the same speec as the scrolling backgrounds's speed
	public float returnSpeed;
	#endregion

	//Static Instance
	public static ZombieStateManager Instance;

	//Eating duration
	public float eatDuration;



	private PlayerState _state;
	public PlayerState State{
		get{
			return _state;
		}
		set{
			_switchState(value);
		}
	}

	public SimpleStateMachine _stateMachine;
	private SimpleState idle,running,ducking,jumping, eating, resetting;


	void Awake(){
		Instance = this;

		//Inirialize states
		idle = new SimpleState(IdleEnter, IdleUpdate, IdleExit, "[PLAYER_STATE] : IDLE");
		running = new SimpleState(RunningEnter, RunningUpdate, RunningExit, "[PLAYER_STATE] : RUNNING");
		ducking = new SimpleState(DuckingEnter, DuckingUpdate, DuckingExit, "[PLAYER_STATE] : DUCKING");
		jumping = new SimpleState(JumpingEnter, JumpingUpdate, JumpingExit, "[PLAYER_STATE] : JUMPING");
		eating = new SimpleState(EatingEnter, EatingUpdate, EatingExit, "[PLAYER_STATE] : EATING");
		resetting = new SimpleState(ResettingEnter, ResettingUpdate, ResettingExit, "[PLAYER_STATE] : RESETTING");


	}


	void Start(){
		State = PlayerState.Running;

		Controller = GetComponent<CharacterController>();

		_initialPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {

		//Either my computation is right or my assertion is right
		IsGrounded = _isCharGrounded() || _assertGrounded;

		_stateMachine.Execute();


	}


	#region IDLE_STATE
	void IdleEnter(){

	}
	void IdleUpdate(){}
	void IdleExit(){}
	#endregion
	
	#region RUNNING_STATE
	//Asserting the fact that I know I'm grounded since I'm entering this state, have to do this since IsGrounded takes a while to catch up.
	private bool _assertGrounded = false;
	void RunningEnter(){
		//Set velocity to zero
		velocity = velocity.ZeroY();
		Debug.Log("STICK ZOMBIE TO:" + transform.position.SetY(Floor.Instance.FloorLevel));

		//Stick character to floor
		transform.position = transform.position.SetY(Floor.Instance.FloorLevel);

		_assertGrounded = true;
	}
	void RunningUpdate(){
	
	

		if(Input.GetKeyDown(KeyCode.UpArrow)){
			State = PlayerState.Jumping;
		}



	}
	void RunningExit(){

		_assertGrounded = false;
	}
	#endregion
	
	#region DUCKING_STATE
	void DuckingEnter(){}
	void DuckingUpdate(){}
	void DuckingExit(){}
	#endregion

	#region JUMPING_STATE
	//Record when jump starts, so than when IsGrounded again at a later time than this, I switch back to running
	float _jumpStartTime;

	void JumpingEnter(){
		velocity += jumpVec;
		_jumpStartTime = Time.time;
	}
	void JumpingUpdate(){

		if(!_isCharGrounded()){
			velocity+=gravity;
		}

		//When isgrounded is true after a while
		if(Time.time > _jumpStartTime && _isCharGrounded()){
			State = PlayerState.Running;
		}

		Controller.Move(velocity*Time.deltaTime);
	}
	void JumpingExit(){}
	#endregion

	#region EATING_STATE
	void EatingEnter(){

		UnityTimer.Instance.CallAfterDelay(() => {
			State = PlayerState.Resetting;
		}, eatDuration);

		//Temporary Visual Effect to show eating
		transform.FindChild("zombieSprite").GetComponent<SpriteRenderer>().color = Color.red;
		KillHuman.killthehuman = true;


	}
	void EatingUpdate(){}
	void EatingExit(){
		//Temporary Visual Effect to show eating
		transform.FindChild("zombieSprite").GetComponent<SpriteRenderer>().color = Color.white;
		//Tell scoring system
		ScoreManager.Instance.OnHumanCaught();

	}
	#endregion

	#region RESETTING_STATE
	Vector3 _slideBackVector;

	void ResettingEnter(){

		//Temporary Visual Effect to show eating
		transform.FindChild("zombieSprite").GetComponent<SpriteRenderer>().color = Color.green;

		_slideBackVector = Utilities.GetPreventOvershootMoveVector(transform, transform.position, _initialPosition, returnSpeed);

	}
	void ResettingUpdate(){

		Controller.Move(_slideBackVector);

		if(Utilities.CloseEnough(transform.position, _initialPosition)){
			State = PlayerState.Running;
		}

	}



	void ResettingExit(){

		//Temporary Visual Effect to show eating
		transform.FindChild("zombieSprite").GetComponent<SpriteRenderer>().color = Color.white;

		OrganManager.Instance.ResetOrgans();

//		GameEvents.emitEatenEvent();


	}
	#endregion

	void _switchState(PlayerState _targetState){
		_state = _targetState;

		switch(_targetState){
			case PlayerState.Idle:
				_stateMachine.SwitchStates(idle);
				break;
			case PlayerState.Running:
				_stateMachine.SwitchStates(running);
				break;
			case PlayerState.Ducking:
				_stateMachine.SwitchStates(ducking);
				break;
			case PlayerState.Jumping:
				_stateMachine.SwitchStates(jumping);
				break;
			case PlayerState.Eating:
				_stateMachine.SwitchStates(eating);
				break;
			case PlayerState.Resetting:
				_stateMachine.SwitchStates(resetting);
				break;
		}
	}

	//HACK Since character controllers isGrounded sucks
	private bool _isCharGrounded(){
		
		return (transform.position.y - Floor.Instance.FloorLevel < -0.1);
	}
	


	#region COLLISION_HANDLERS

	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Human"){
			Debug.Log("CAUGHT HUMAN");

			State = PlayerState.Eating;
		}
	}

	#endregion

}
