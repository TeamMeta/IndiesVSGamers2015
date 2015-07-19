using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameStateManager.Instance.State == GameManager.GameState.MainMenu && GetComponent<AudioSource>().time > 17.42f) {
			GetComponent<AudioSource>().time = 8.7f;
		} else if (GameStateManager.Instance.State == GameManager.GameState.Running && (GetComponent<AudioSource>().clip.length - GetComponent<AudioSource>().time) < 0.01f) {
			GetComponent<AudioSource>().time = 26.2f;
		}
		if(OrganManager.Instance.failedOrgans.ContainsKey(OrganType.Lungs)) {
			transform.GetChild(0).GetComponent<AudioSource>().volume = Mathf.MoveTowards(transform.GetChild(0).GetComponent<AudioSource>().volume, 1, Time.deltaTime);
		} else {
			transform.GetChild(0).GetComponent<AudioSource>().volume = Mathf.MoveTowards(transform.GetChild(0).GetComponent<AudioSource>().volume, 0, Time.deltaTime);
		}

		if(OrganManager.Instance.failedOrgans.ContainsKey(OrganType.Heart)) {
			transform.GetChild(1).GetComponent<AudioSource>().volume = Mathf.MoveTowards(transform.GetChild(1).GetComponent<AudioSource>().volume, 1, Time.deltaTime);
		} else {
			transform.GetChild(1).GetComponent<AudioSource>().volume = Mathf.MoveTowards(transform.GetChild(1).GetComponent<AudioSource>().volume, 0, Time.deltaTime);
		}

		if(OrganManager.Instance.failedOrgans.ContainsKey(OrganType.Generic)) {
			transform.GetChild(2).GetComponent<AudioSource>().volume = Mathf.MoveTowards(transform.GetChild(2).GetComponent<AudioSource>().volume, 1, Time.deltaTime);
		} else {
			transform.GetChild(2).GetComponent<AudioSource>().volume = Mathf.MoveTowards(transform.GetChild(2).GetComponent<AudioSource>().volume, 0, Time.deltaTime);
		}
//		if(
	}
}
