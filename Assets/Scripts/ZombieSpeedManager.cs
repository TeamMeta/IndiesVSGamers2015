using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerManager;


/// <summary>
/// This class is an observer, observers how the organsare doing and sets your distance to the human accordingly.
/// </summary>
public class ZombieSpeedManager : MonoBehaviour {

	public static ZombieSpeedManager Instance;

	//List of available organs
	private Dictionary<OrganType, FailedOrgan> _organs ;


	//Set in inspector for now, TODO:the overarching GameManager should set these depending on game mode and level.
	public GameObject zombie;

	//Base organ health
	public float baseOrganHealth;

	//Distance factor based on how well organs are handled
	public float distancePerHealth;

	//Zombie move speed
	public float moveSpeed;

	//Clamping
	public Transform lowerClamp;
	public Transform upperClamp;
	public Transform offscreenLimit;


	//Calibrate how much to move the zombie closer/farther based on how its initially set in the scene.
	public Vector3 _initialPosition;

	private Vector2 _dirVector;
	private Vector2 _finalPosition;

	public Vector2 FinalPosition{
		get{
			return _finalPosition;
		}
	}


	public AnimationCurve multiplierDistanceRelationship;


	void Awake(){
		Instance = this;

		_dirVector = new Vector2(1,0);
	
	}

	void Start(){

		//Subscrive to events
		GameEvents.emitEaten+=OnEaten;

		_initialPosition = zombie.transform.position;

		//Get list from organ manager
		_organs = OrganManager.Instance.failedOrgans;
	}


	void Update(){

		//Can move zombie only if he is is in running state
		if(ZombieStateManager.Instance.State == PlayerState.Running)
		{
			MoveZombie();

			//Determine if the zombie is offscreen

			if(transform.position.x < offscreenLimit.position.x){

                //Reset zombie state to idle
                ZombieStateManager.Instance.State = PlayerState.Idle;

				//End the Game once you are offscreen

				GameStateManager.Instance.State = GameManager.GameState.Ended;
			}
		}

		distancePerHealth = multiplierDistanceRelationship.Evaluate(Scoreboard.Instance.Multiplier);
	}


	//Handle Zombie movements
	void MoveZombie(){

		_finalPosition = (Vector2)_initialPosition +  _dirVector * DistanceDeltaAverage() * distancePerHealth ;


		Debug.DrawRay(_finalPosition, Vector3.up, Color.green);
		Debug.DrawRay(_initialPosition, Vector3.up, Color.red);

		//Clamping final position based oncamp points
		if(_finalPosition.x < lowerClamp.transform.position.x)
			_finalPosition = ((Vector3)_finalPosition).SetX(lowerClamp.transform.position.x);

		if(_finalPosition.x > upperClamp.transform.position.x)
			_finalPosition = ((Vector3)_finalPosition).SetX(upperClamp.transform.position.x);

		Vector3 movement = Utilities.GetPreventOvershootMoveVector(zombie.transform, zombie.transform.position, _finalPosition, moveSpeed);



		

		ZombieStateManager.Instance.Controller.Move(movement.ZeroZ().ZeroY());


//		Vector3 movementVector = (Vector3) (_dirVector * DistanceDeltaAverage()).normalized;



	}



	//Calculates the distance the zombie should be placed from the initial position.	
	float DistanceDelta(){


		float totalDelta = 0;

		string organsNames ="FAILED ORGANS: ";

		//Iterate over all failed organs 
		foreach(KeyValuePair<OrganType, FailedOrgan> _failedOrganEntery in _organs){
			FailedOrgan _failedOrgan = _failedOrganEntery.Value;
			if(_failedOrgan.organHealth != baseOrganHealth){


				if(_failedOrganEntery.Key == OrganType.Heart){
					totalDelta += (_failedOrgan.organHealth - baseOrganHealth) * 0.5f;
				}
				else{
					totalDelta += (_failedOrgan.organHealth - baseOrganHealth);
				}

			}
			organsNames += _failedOrganEntery.Key.ToString()+"   ";
		}
//		Debug.Log(organsNames);
		return totalDelta * distancePerHealth;
	}


	float DistanceDeltaAverage(){
		float sum = 0;


		foreach(KeyValuePair<OrganType, FailedOrgan> _failedOrganEntery in _organs){
			sum+= _failedOrganEntery.Value.organHealth;
		}

		if(_organs.Count != 0)
		{
			float avg = sum/_organs.Count;
			return avg - baseOrganHealth;
		}
		else{
			return 0;
		}



	}

	public void ResetZombie(){
		ZombieStateManager.Instance.Controller.enabled = false;
		transform.position.Set(-3.42f, -0.25f, -3.97f);
		ZombieStateManager.Instance.Controller.enabled = true;
	}


	void OnEaten(){
		OrganManager.Instance.ResetOrgans();
	}
}
