using UnityEngine;
using System.Collections;

public class FailedLungs : FailedOrgan {

	public override float HandleOrgan ()
	{
		float quality = 0f;
		if(Input.GetKey(savingControl)) {
			rhythmTimer += Time.deltaTime;
			if(rhythmTimer < rhythmSpeed) {
				if(((int)(rhythmTimer*10)) % 20 == 0) {
					organHealth++;
				}
			} else {
				if(((int)(rhythmTimer*10)) % 20 == 0) {
					organHealth--;
				}
			}
		} else {
			if(rhythmTimer > 0) {
				rhythmTimer -= Time.deltaTime;
			} else {
				if(((int)(rhythmTimer*10)) % 20 == 0) {
					organHealth--;
				}
			}
		}

		Debug.Log(organHealth);

		if(organHealth <= 0) {
			organHealth = 0;
		}
		if(organHealth >= 100) {
			organHealth = 100;
		}
		return quality;
	}
}
