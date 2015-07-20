using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FailedLegs : FailedOrgan {

	public KeyCode firstKey, otherKey;

	private float tempTimer, buttonPressTimer1, buttonPressTimer2;

    public override float HandleOrgan ()
	{
		tempTimer += Time.deltaTime;
		rhythmTimer = rhythmSpeed;
		if(Input.GetKeyDown(savingControl) && savingControl == firstKey) {
			buttonPressTimer1 = tempTimer;
			savingControl = otherKey;
		} else if (Input.GetKeyDown(savingControl) && savingControl == otherKey) {
			buttonPressTimer2 = tempTimer;
			savingControl = firstKey;
		}
//		Debug.Log(buttonPressTimer1 + " " + buttonPressTimer2);
		if(buttonPressTimer1 != -1 && buttonPressTimer2 != -1) {
			if(Mathf.Abs(buttonPressTimer1 - buttonPressTimer2) < 0.2f) {
				organHealth++;

				PositiveFeedback.LegInstance.LegsWellDone();
				ScoreManager.Instance.OnOrganHealed(OrganType.Legs, 1);


                onBeat = true;
            } else {
				organHealth--;
                onBeat = false;
            }
			buttonPressTimer1 = -1;
			buttonPressTimer2 = -1;
			tempTimer = 0;
		} else if(tempTimer > 1f && (buttonPressTimer1 == -1 || buttonPressTimer2 == -1)) {
			organHealth--;
			tempTimer = 0;
		}
//		else if(buttonPressTimer1 != -1 || buttonPressTimer2 != -1) {
//			if(Mathf.Abs(buttonPressTimer1 - buttonPressTimer2) >= 0.2f) {
//				organHealth--;
//				buttonPressTimer1 = -1;
//				buttonPressTimer2 = -1;
//			}
//		}
		return 0f;
	}

	protected override void SelectControl ()
	{
		int tempRnd = UnityEngine.Random.Range(0,keys.Length-1);
		string tempSelectedControl = keys[tempRnd];
		string tempSelectedControl2 = keys[tempRnd+1];
		while(this.alreadyMappedKeys.Contains(tempSelectedControl) || alreadyMappedKeys.Contains(tempSelectedControl2)) {
			tempRnd = UnityEngine.Random.Range(0,keys.Length-1);
			tempSelectedControl = keys[tempRnd];
			tempSelectedControl2 = keys[tempRnd+1];
		}
		this.alreadyMappedKeys.Add(tempSelectedControl);
		this.alreadyMappedKeys.Add(tempSelectedControl2);
		savingControl = (KeyCode)Enum.Parse(typeof(KeyCode), tempSelectedControl, true);
		firstKey = savingControl;
		otherKey = (KeyCode)Enum.Parse(typeof(KeyCode), tempSelectedControl2, true);
	}

	protected override string[] SetAptKeys ()
	{
		List<String> keysList = new List<string>();

		keysList.Add("y");
        keysList.Add("u");
		keysList.Add("i");
		keysList.Add("o");
		keysList.Add("p");


        return keysList.ToArray();
        
    }
}
