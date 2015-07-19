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

	public float organFailingTimeInterval;
	private float organFailingTimer;

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
		organFailingTimer = organFailingTimeInterval - 5;
//		if(GameStateManager.Instance.State == GameManager.GameState.Running) {
//			UnityTimer.Instance.CallAfterDelay(() => {
//
//				FailOrgan();
//
//				UnityTimer.Instance.CallAfterDelay(() => {
//
//					FailOrgan();
//
//					UnityTimer.Instance.CallAfterDelay(() => {
//
//						FailOrgan();
//
//					}, thirdOrganFailTime);
//
//				}, secondOrganFailTime);
//
//			}, firstOrganFailTime);
//		}
	
	}


	
	// Update is called once per frame
	void Update () {
		if(GameStateManager.Instance.State == GameManager.GameState.Running) {
			UpdateOrgans();
			organFailingTimer += Time.deltaTime;
			if(organFailingTimer > organFailingTimeInterval) {
				FailOrgan();
				organFailingTimer = 0f;
			}
		}
	}




	//Fail the organ of said organ type
	void FailOrgan(){
		OrganType organTypeToFail = PickRandomOrgan();
		if(organTypeToFail != OrganType.Length) {
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

	}


	//Pick a random organ from whatever is left over
	OrganType PickRandomOrgan(){
		int upperLimit = organsToFail.Count - 1;

		if(upperLimit == -1) {
			return OrganType.Length;
		}

		int organToPick = Random.Range(0, upperLimit);

		OrganType[] leftOverOrgans = new OrganType[organsToFail.Count];

		organsToFail.Keys.CopyTo(leftOverOrgans, 0);

		return leftOverOrgans[organToPick];
	}

	//Iterate over dictionary to update each organ
	void UpdateOrgans(){

		foreach(KeyValuePair<OrganType, FailedOrgan> organEntry in failedOrgans){
			organEntry.Value.HandleOrgan();
//
//			//If any organs health reaches 100 , heal it
//			if(organEntry.Value.organHealth > 100){
//				organEntry.Value.organHealth = 100;
//
//				UIManager.instance.HealOrgan(organEntry.Value);
//
//			}
//
//
			//If any organ dies the game ends
			if(organEntry.Value.organHealth < 0){
				organEntry.Value.organHealth = 0;
				
				GameStateManager.Instance.State = GameManager.GameState.Ended;
			}
		}

		FailedOrgan heart;
		failedOrgans.TryGetValue(OrganType.Heart, out heart);

		if(heart != null){
			if(heart.organHealth > 100){
				heart.organHealth = 100;
				HealOrgan(OrganType.Heart, heart);
			}
		}

		FailedOrgan lungs;
		failedOrgans.TryGetValue(OrganType.Lungs, out lungs);


		if(lungs != null){
			if(lungs.organHealth > 100){
				lungs.organHealth = 100;
				HealOrgan(OrganType.Lungs, lungs);
			}
		}

		FailedOrgan legs;
		failedOrgans.TryGetValue(OrganType.Legs, out legs);


		if(legs != null){
			if(legs.organHealth > 100){
				legs.organHealth = 100;
				HealOrgan(OrganType.Legs, legs);
			}
		}

	}

	//Reset all organs' state
	public void ResetOrgans(){

		Debug.Log("HEAL ALL ORGANS");

		FailedOrgan heart;
		failedOrgans.TryGetValue(OrganType.Heart, out heart);

		if(heart != null)
			HealOrgan(OrganType.Heart, heart);
	
		
		FailedOrgan lungs;
		failedOrgans.TryGetValue(OrganType.Lungs, out lungs);

		if(lungs != null)
			HealOrgan(OrganType.Lungs, lungs);

		
		FailedOrgan legs;
		failedOrgans.TryGetValue(OrganType.Legs, out legs);

		if(legs != null)
			HealOrgan(OrganType.Legs, legs);


		organFailingTimer = organFailingTimeInterval - 5;


	}

	public void HealOrgan(OrganType organType, FailedOrgan organ){

		//Update the UI
		UIManager.instance.HealOrgan(organ);

		//Remove organ from failedOrgan
		failedOrgans.Remove(organType);

		//reset orgasn health to 50
		organ.ResetOrgan();


		//Add to organs to fail
		organsToFail.Add(organType, organ);


	}

	public void HealOrganIndividual(OrganType organType, FailedOrgan organ){
		
		//Update the UI
		UIManager.instance.HealOrgan(organ);
		
		//Remove organ from failedOrgan
		failedOrgans.Remove(organType);
		
		//reset orgasn health to 50
		organ.ResetOrgan();
		
		//Add to organs to fail
		organsToFail.Add(organType, organ);

		//Reset Initial position so dela calculations are now wrt to this position
		ZombieSpeedManager.Instance._initialPosition = transform.position;


		
		
	}
}
