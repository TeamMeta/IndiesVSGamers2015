using UnityEngine;
using System.Collections;

public class FailedLungs : FailedOrgan {

	public override float HandleOrgan ()
	{
		float quality = 0f;
		if(Input.GetKey(savingControl)) {
			rhythmTimer += Time.deltaTime;
			if(rhythmTimer < rhythmSpeed) {
				if(((int)(rhythmTimer*10)) % 3 == 0) {
					organHealth++;
				}
			} else {
				if(((int)(rhythmTimer*10)) % 3 == 0) {
					organHealth--;
				}
			}
		} else {
			if(rhythmTimer > 0) {
				rhythmTimer -= Time.deltaTime;
			} else {
				if(((int)(rhythmTimer*10)) % 3 == 0) {
					organHealth--;
				}
			}
		}
		return quality;
	}
}
