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
				PositiveFeedback.LungInstance.LungsWellDone();
                onBeat = true;
            } else {
				PositiveFeedback.LungInstance.LungsReset();
				organHealthf -= Time.deltaTime;
                onBeat = false;
            }
		} else {
			PositiveFeedback.LungInstance.LungsReset();
			if(rhythmTimer > 0) {
				rhythmTimer -= Time.deltaTime;
                onBeat = false;
            } else {
				organHealthf -= Time.deltaTime;
                onBeat = false;
            }
		}

		organHealth = (int)organHealthf;
		return quality;
	}
}
