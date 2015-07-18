using UnityEngine;
using System.Collections;

public class FailedHeart : FailedOrgan {

//	private float onBeatTimer = 0.5f;
//	private float offBeatTimer = 0.75f;
	public bool onBeat = true;

	public override float HandleOrgan ()
	{
		rhythmTimer += Time.deltaTime;
		float quality = 0f;
		if(Input.GetKeyDown(savingControl)) {
			if(onBeat) {
				if(Mathf.Abs(rhythmTimer - rhythmSpeed) <= (acceptableError * 2f)) {
					rhythmTimer = 0;
					quality = Mathf.Abs(rhythmTimer - rhythmSpeed);
					onBeat = false;
					rhythmSpeed = 0.5f;
				} else {
					organHealth -= 5;
					rhythmTimer = 0;
					Debug.Log("Oh no! You didn't do the thing! Organ health is now: " + organHealth);
					quality = -1;
					onBeat = true;
					rhythmSpeed = 3f;
				}
			} else {
				if(rhythmTimer <= rhythmSpeed) {
					onBeat = true;
					organHealth += 5;
					rhythmTimer = 0;
					rhythmSpeed = 3f;
				}
			}
		}
		if(onBeat) {
			if(rhythmTimer > (rhythmSpeed + (acceptableError*2))) {
				organHealth -= 5;
				rhythmTimer = 0;
				Debug.Log("Oh no! You didn't do the thing! Organ health is now: " + organHealth);
				quality = -1;
				rhythmSpeed = 3f;
			}
		} else {
			if(rhythmTimer > rhythmSpeed) {
				onBeat = true;
				quality = -1;
				rhythmTimer = 0;
				rhythmSpeed = 3f;
			}
		}
		return quality;
	}
	
}
