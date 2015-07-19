using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum OrganType{
	Heart = 0,
	Lungs = 1,
	Legs = 2, //Placeholder for whatever the last organ is going to be
	Length
}

public class OrganManager : MonoBehaviour {

	public static OrganManager Instance;


	//Keeping track of failed organs
	public Dictionary<OrganType, FailedOrgan> failedOrgans{get; private set;}

	//Keeping track of unfailed organs
	public Dictionary<OrganType, FailedOrgan> organsToFail{get; private set;}

	


	//Organ fail times
	public float firstOrganFailTime;
	public float secondOrganFailTime;
	public float thirdOrganFailTime;

	void Awake(){
		Instance = this;

		failedOrgans = new Dictionary<OrganType, FailedOrgan>();
		organsToFail = new Dictionary<OrganType, FailedOrgan>();

		//Populate Dictionary
		organsToFail.Add(OrganType.Heart, new FailedHeart());
		organsToFail.Add(OrganType.Lungs, new FailedLungs());
		organsToFail.Add(OrganType.Legs, new FailedLegs());
	}


	// Use this for initialization
	public void Start () {
		if(GameStateManager.Instance.State == GameManager.GameState.Running) {
			UnityTimer.Instance.CallAfterDelay(() => {

				FailOrgan();

				UnityTimer.Instance.CallAfterDelay(() => {

					FailOrgan();

					UnityTimer.Instance.CallAfterDelay(() => {

						FailOrgan();

					}, thirdOrganFailTime);

				}, secondOrganFailTime);

			}, firstOrganFailTime);
		}
	
	}


	
	// Update is called once per frame
	void Update () {
		if(GameStateManager.Instance.State == GameManager.GameState.Running) {
			UpdateOrgans();
		}
	}




	//Fail the organ of said organ type
	void FailOrgan(){
		OrganType organTypeToFail = PickRandomOrgan();

		//Remove from organstoFail
		FailedOrgan organToFail;
		organsToFail.TryGetValue(organTypeToFail,out organToFail);
		organsToFail.Remove(organTypeToFail);

		//Fail the organ
		organToFail.OrganFail();

		//Add to failed organs so its handled correctly
		failedOrgans.Add(organTypeToFail, organToFail);


		//Show UI

		UIManager.instance.FailOrgan(organToFail);

	}


	//Pick a random organ from whatever is left over
	OrganType PickRandomOrgan(){
		int upperLimit = organsToFail.Count - 1;

		int organToPick = Random.Range(0, upperLimit);

		OrganType[] leftOverOrgans = new OrganType[organsToFail.Count];

		organsToFail.Keys.CopyTo(leftOverOrgans, 0);

		return leftOverOrgans[organToPick];
	}

	//Iterate over dictionary to update each organ
	void UpdateOrgans(){

		foreach(KeyValuePair<OrganType, FailedOrgan> organEntry in failedOrgans){
			organEntry.Value.HandleOrgan();
		}
	}

	//Reset all organs' state
	public void ResetOrgans(){
		foreach(KeyValuePair<OrganType, FailedOrgan> organEntry in failedOrgans){
			organEntry.Value.organHealth = 50;
		}
	}
}
