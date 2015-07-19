using UnityEngine;
using System.Collections;

public class FailedLungs : FailedOrgan {

	private float organHealthf = 50;

	public override float HandleOrgan ()
	{
		float quality = 0f;
		if(Input.GetKey(savingControl)) {
			rhythmTimer += Time.deltaTime;
			if(rhythmTimer < rhythmSpeed) {
				organHealthf += Time.deltaTime;
			} else {
				organHealthf -= Time.deltaTime;
			}
		} else {
			if(rhythmTimer > 0) {
				rhythmTimer -= Time.deltaTime;
			} else {
				organHealthf -= Time.deltaTime;
			}
		}

		organHealth = (int)organHealthf;

		if(organHealth <= 0) {
			organHealth = 0;
		}
		if(organHealth >= 100) {
			organHealth = 100;
		}
		return quality;
	}
}
