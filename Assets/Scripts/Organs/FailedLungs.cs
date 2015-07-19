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
                onBeat = true;
            } else {
				organHealthf -= Time.deltaTime;
                onBeat = false;
            }
		} else {
			if(rhythmTimer > 0) {
				rhythmTimer -= Time.deltaTime;
                onBeat = false;
            } else {
				organHealthf -= Time.deltaTime;
                onBeat = false;
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
