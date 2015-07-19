using UnityEngine;
using System.Collections;

public class PositiveFeedback : MonoBehaviour {

	public static PositiveFeedback HeartInstance, LungInstance, LegInstance;

	// Use this for initialization
	void Start () {
		if(this.gameObject.name.Equals("HeartPositiveFeedback")) {
			Debug.Log("EHRE");
			HeartInstance = this;
		}
		if(this.gameObject.name.Equals("LungPositiveFeedback")) {
			LungInstance = this;
		}
		if(this.gameObject.name.Equals("LegPositiveFeedback")) {
			LegInstance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HeartWellDone() {
		if(this.gameObject.name.Equals("HeartPositiveFeedback")) {
			GetComponent<Animator>().SetTrigger("Init");
		}
	}

	public void LungsWellDone() {
		if(this.gameObject.name.Equals("LungPositiveFeedback")) {
			GetComponent<Animator>().SetTrigger("Init");
		}
	}

	public void LegsWellDone() {
		if(this.gameObject.name.Equals("LegPositiveFeedback")) {
			GetComponent<Animator>().SetTrigger("Init");
		}
	}

	public void LungsReset() {
		GetComponent<Animator>().ResetTrigger("Init");
	}
}
