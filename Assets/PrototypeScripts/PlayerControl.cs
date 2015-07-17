using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	public int difficulty = 0;

	private float timeBeforeOrganFail;
	private float timeBeforeOrganButtonSwap;
	private int numOrgansFailed;

	private List<FailedOrgan> failedOrgans;

	//every time interval the game will decide whether or not to
	//fail an organ or to swap the controls for an existing failed organ
	private bool failOrgan = true;

	public GameObject organTimer;


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

	public Text info;

	// Use this for initialization
	void Start () {

		failedOrgans = new List<FailedOrgan>();
		timeBeforeOrganFail = Random.Range(3f,10f-difficulty);


		_rotHinge = transform.parent;
		_initialRot=_rotHinge.rotation;
		_controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		ManageOrgans();
		ManageMovement();
	}


	void ManageOrgans(){
		timeBeforeOrganFail -= Time.deltaTime;
		timeBeforeOrganButtonSwap -= Time.deltaTime;
		if(timeBeforeOrganFail <= 0 && failOrgan) {
			numOrgansFailed++;
			FailedOrgan fo = new FailedOrgan();
			fo.OrganFail();
			failedOrgans.Add(fo);
			GameObject temp = (GameObject)GameObject.Instantiate(organTimer, new Vector3(-10f + numOrgansFailed*2f, 4f, 0f), Quaternion.identity);
			temp.GetComponent<OrganVisualTimer>().organ = failedOrgans[numOrgansFailed-1];
			Debug.Log("An organ failed! Press " + fo.savingControl + " to save the organ.");
			if(Random.value < 0.25f && numOrgansFailed < 4) {
				failOrgan = true;
				timeBeforeOrganFail = Random.Range(10f, 20f-difficulty);
			} else {
				failOrgan = false;
				timeBeforeOrganButtonSwap = Random.Range(20f, 40f-difficulty);
			}
		} else if(timeBeforeOrganButtonSwap <= 0 && !failOrgan) {
			int tempOrgan = Random.Range(0, failedOrgans.Count);
			KeyCode prevKey = failedOrgans[tempOrgan].savingControl;
			failedOrgans[tempOrgan].SwapControl();
			Debug.Log("You no longer have to press " + prevKey + ", press " + failedOrgans[tempOrgan].savingControl + " to save the organ now.");
			if(Random.value < 0.25f && numOrgansFailed < 4) {
				failOrgan = true;
				timeBeforeOrganFail = Random.Range(3f, 10f-difficulty);
			} else {
				failOrgan = false;
				timeBeforeOrganButtonSwap = Random.Range(3f, 10f-difficulty);
			}
		}
		
		info.text = "";
		for(int i = 0; i < failedOrgans.Count; i++) {
			failedOrgans[i].HandleOrgan();
			info.text += "Organ " + i + "\n\tKey: " + failedOrgans[i].savingControl + ":\n\tTime: " + (100-(int)(failedOrgans[i].RhythmPercentage()*100)) + "\n\tHealth: " + failedOrgans[i].organHealth + "\n";
		}
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
				velocity+=jumpVec;
			}else{
				//Reset velocities y component
				velocity.y = 0;
			}

			//Can limbo when grounded
			if(Input.GetKeyDown(KeyCode.DownArrow) && !IsLimboing){
				Debug.Log("DO THE LIMBO");
				StartCoroutine("DoTheLimbo");
			}
		}

		//Move the character
		_controller.Move(velocity*Time.deltaTime);

	

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
