using UnityEngine;
using System.Collections;
using PlayerManager;
using ScoreUtilities;

namespace PlayerManager{
	public enum PlayerState{
		Idle,
		Running,
		Ducking,
		Jumping
	}
}

/// <summary>
/// A singleton controlling the player
/// </summary>
public class PlayerStateManager : MonoBehaviour {


	private CharacterController _controller;
	
	#region MOVEMENT_PARAMS
	public Vector2 velocity;
	public Vector2 jumpVec;
	public Vector2 gravity;
	public bool IsLimboing = false;
	
	//Stuff to make the zombie rotate
	public float limboSpeed;
	private Quaternion _initialRot ;
	private Quaternion _finalRot = Quaternion.Euler(new Vector3(0,0,60));
	private Transform _rotHinge;
	//Debug check in inspector
	public bool Grounded;
	#endregion

	//Static Instance
	public static PlayerStateManager Instance;

	private PlayerState _state;
	public PlayerState State;

	public SimpleStateMachine _stateMachine;
	private SimpleState idle,running,ducking,jumping;


	void Awake(){
		Instance = this;
        PlayerPrefs.DeleteAll();
        //Inirialize states
        idle = new SimpleState(IdleEnter, IdleUpdate, IdleExit, "[PLAYER_STATE] : IDLE");
		running = new SimpleState(RunningEnter, RunningUpdate, RunningExit, "[PLAYER_STATE] : RUNNING");
		ducking = new SimpleState(DuckingEnter, DuckingUpdate, DuckingExit, "[PLAYER_STATE] : DUCKING");
		jumping = new SimpleState(JumpingEnter, JumpingUpdate, JumpingExit, "[PLAYER_STATE] : JUMPING");
	}


	void Start(){
		_rotHinge = transform.parent;
		_initialRot=_rotHinge.rotation;
		_controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		ManageMovement();
	}

	
	
	
	
	

	void ManageMovement(){
		
		//Debug Check in Inspector
		Grounded = _isCharGrounded();
		
		
		if(!_isCharGrounded()){
			//Add graivty to veocity
			velocity+=gravity;
		}else{
			
			
			//Can jump only when grounded
			if(Input.GetKeyDown(KeyCode.UpArrow)){
				Debug.Log("JUMP PRESSED!!!");

                ScoreManager objScore = new ScoreManager();
                objScore.bTest = true;
                objScore.setMilesPoint(1);
                velocity +=jumpVec;
			}else{
				//Reset velocities y component
				velocity.y = 0;
			}
			
			//Can limbo when grounded
			if(Input.GetKeyDown(KeyCode.DownArrow) && !IsLimboing){
				Debug.Log("DO THE LIMBO");
                ScoreManager objScore = new ScoreManager();
                objScore.bTest = true;
                objScore.bShowLeaderBoard = true;
                objScore.UpdateScore(false,"");
                StartCoroutine("DoTheLimbo");
			}
		}
		
		//Move the character
		_controller.Move(velocity*Time.deltaTime);
		
		
		
	}

	#region IDLE_STATE
	void IdleEnter(){

	}
	void IdleUpdate(){}
	void IdleExit(){}
	#endregion
	
	#region RUNNING_STATE
	void RunningEnter(){}
	void RunningUpdate(){}
	void RunningExit(){}
	#endregion
	
	#region DUCKING_STATE
	void DuckingEnter(){}
	void DuckingUpdate(){}
	void DuckingExit(){}
	#endregion

	#region JUMPING_STATE
	void JumpingEnter(){}
	void JumpingUpdate(){}
	void JumpingExit(){}
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
		}
	}

	//HACK Since character controllers isGrounded sucks
	private bool _isCharGrounded(){
		
		return (transform.position.y < -0.36);
	}
	
	//HACK: co routine for limbo, should be a different state in a FSM
	IEnumerator DoTheLimbo(){
		bool movingBack = true;
		bool movingForward = false;
		IsLimboing = true;
		Quaternion deltaRot = Quaternion.RotateTowards(_initialRot, _finalRot, limboSpeed*Time.deltaTime);
		while(movingBack){
			//			Debug.Log("MOVING BACK");
			_rotHinge.Rotate(deltaRot.eulerAngles);
			if(AlmostEquals(_rotHinge.rotation, _finalRot)){
				movingBack = false;
				movingForward = true;
			}
			
			yield return null;
		}
		
		yield return new WaitForSeconds(1.0f);
		
		while(movingForward){
			//			Debug.Log("MOVING FORWARD");
			Quaternion revDeltaRot = Quaternion.Euler(deltaRot.eulerAngles.x, deltaRot.eulerAngles.y, -deltaRot.eulerAngles.z);
			_rotHinge.Rotate(revDeltaRot.eulerAngles);
			if(AlmostEquals(_rotHinge.rotation, _finalRot)){
				movingBack = true;
				movingForward = false;
			}
			
			yield return null;
		}
		IsLimboing = false;
		
	}
	
	//Quick rotation comparator
	bool AlmostEquals(Quaternion _one, Quaternion _two){
		float _oneZ = _one.eulerAngles.z;
		float _twoZ = _two.eulerAngles.z;
		//		Debug.Log("ONE:"+_oneZ+"TWO:"+_twoZ);
		return ((_oneZ - _twoZ) > 0);
	}

}
