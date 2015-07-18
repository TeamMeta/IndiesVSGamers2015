using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is an observer, observers how the organsare doing and sets your distance to the human accordingly.
/// </summary>
public class ZombiePowerupManager : MonoBehaviour {

	public static ZombiePowerupManager Instance;

	//List of available organs
	private List<FailedOrgan> _organs ;


	//Set in inspector for now, TODO:the overarching GameManager should set these depending on game mode and level.
	public GameObject zombie;
	public GameObject human;

	//Base organ health
	public float baseOrganHealth;

	//Distance factor based on how well organs are handled
	public float distanceMultiplier;

	//Zombie move speed
	public float moveSpeed;

	//Clamping
	public Transform lowerClamp;
	public Transform upperClamp;


	//Calibrate how much to move the zombie closer/farther based on how its initially set in the scene.
	private float _initialDistance;
	private Vector3 _initialPosition;

	private Vector2 _dirVector;
	private Vector2 _finalPosition;


	void Awake(){
		Instance = this;

		_dirVector = new Vector2(1,0);
	
	}

	void Start(){

		//Get the initial distance
		_initialDistance = Vector2.Distance(zombie.transform.position, human.transform.position);

		_initialPosition = zombie.transform.parent.position;

		//Get list from organ manager
		_organs = OrganManager.Instance.failedOrgans;
	}

	void Update(){


		MoveZombie();
	}

	void MoveZombie(){

		_finalPosition = (Vector2)_initialPosition +  _dirVector * CalculateDistance();

		if(_finalPosition.x < lowerClamp.transform.position.x)
			_finalPosition = ((Vector3)_finalPosition).SetX(lowerClamp.transform.position.x);

		if(_finalPosition.x > upperClamp.transform.position.x)
			_finalPosition = ((Vector3)_finalPosition).SetX(upperClamp.transform.position.x);

		Vector3 movement = GetPreventOvershootMoveVector(zombie.transform.parent, zombie.transform.parent.position, _finalPosition, moveSpeed);

	

		PlayerStateManager.Instance.Controller.Move(movement.ZeroZ());




	}

	Vector3 GetPreventOvershootMoveVector(Transform objectToMove, Vector3 initialPosition, Vector3 finalPosition, float moveSpeed){
		Vector3 moveVector = Vector3.zero;
		if(!(Vector3.Distance(objectToMove.position.ZeroZ(), finalPosition.ZeroZ()) < 0.1)){
			moveVector = (finalPosition - initialPosition).normalized * moveSpeed * Time.deltaTime;
		}

		return moveVector;
	}

	//Calculates the distance the zombie should be placed from the human.
	float CalculateDistance(){


		float totalDelta = 0;
		foreach(FailedOrgan _faledOrgan in _organs){
			if(_faledOrgan.organHealth != baseOrganHealth){
				totalDelta += (_faledOrgan.organHealth - baseOrganHealth);
			}
		}

		return totalDelta * distanceMultiplier;
	}
}
