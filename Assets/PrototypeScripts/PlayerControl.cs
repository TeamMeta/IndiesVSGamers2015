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

	public Text info;

	// Use this for initialization
	void Start () {
		failedOrgans = new List<FailedOrgan>();
		timeBeforeOrganFail = Random.Range(3f,10f-difficulty);
	}
	
	// Update is called once per frame
	void Update () {
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
}
