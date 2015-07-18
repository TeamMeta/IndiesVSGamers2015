using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FailedOrgan {

	//static reference to all the keys that can be used
	//made this static so we don't have to re-load it repeatedly
	private static string[] keys;
	private static List<string> alreadyMappedKeys;

	//the keycode that needs to be pressed to save the organ
	public KeyCode savingControl;

	//how often the player needs to tap the savingControl
	//default value is 1 per second
	public float rhythmSpeed = 3;
	protected float rhythmTimer = 0;

	//the acceptable time-range in which the player can miss the "rhythm" without major penalty
	public float acceptableError = 0.1f;

	//the health of the organ, can be increased/reduced by quality of pressing of keys
	//starts out at 50 and if increased to 100 can be fixed (suggested gameplay mechanic by Kartik)
	public int organHealth = 50;

	public float RhythmPercentage() {
		if(rhythmTimer > 0) {
			return (rhythmTimer - acceptableError*2f)/rhythmSpeed;
		} else {
			return 0;
		}
	}

	public bool WithinTimeRange() {
		return Mathf.Abs(rhythmTimer - rhythmSpeed) < (acceptableError * 2f);
	}

	//override this in subclasses to handle the different organ failings
	//this default implementation handles basic rhythmic tapping and works only for the heart
	//returns the "quality" of how well the player handled saving the organ
	//this must be put in an update function so that it runs properly
	public virtual float HandleOrgan() {
		rhythmTimer += Time.deltaTime;
		float quality = 0f;
		if(Input.GetKeyDown(savingControl)) {
			if(Mathf.Abs(rhythmTimer - rhythmSpeed) < (acceptableError * 2f)) {
				organHealth += 5;
				//if you are super accurate then give extra health back
				if(Mathf.Abs(rhythmTimer - rhythmSpeed) < acceptableError) {
					organHealth += 5;
					Debug.Log("Perfect! Organ health is now: " + organHealth);
				} else {
					Debug.Log("Good! Organ health is now: " + organHealth);
				}
				rhythmTimer = 0;
				quality = Mathf.Abs(rhythmTimer - rhythmSpeed);
			} else {
				organHealth -= 5;
				rhythmTimer = 0;
				Debug.Log("Oh no! You didn't do the thing! Organ health is now: " + organHealth);
				quality = -1;
			}
		}
		if(rhythmTimer > (rhythmSpeed + (acceptableError*2))) {
			organHealth -= 5;
			rhythmTimer = 0;
			Debug.Log("Oh no! You didn't do the thing! Organ health is now: " + organHealth);
			quality = -1;
		}
		return quality;
	}

	public void SwapControl() {
		alreadyMappedKeys.Remove(savingControl.ToString().ToLower());
		SelectControl();
	}

	//What should happen when the organ actually fails
	//override this in subclasses and setup the savingControl in this method
	//be sure to run this default code too
	public virtual void OrganFail() {
		if(keys == null) {
			alreadyMappedKeys = new List<string>();
			List<String> keysList = new List<string>();
			/*
			keysList.AddRange(Enum.GetNames(typeof(KeyCode)));
			//Removing keys that SHOULDN'T ever be mapped
			keysList.Remove("Escape");
			keysList.Remove("BackQuote");
			keysList.Remove("LeftShift");
			keysList.Remove("RightShift");
			keysList.Remove("LeftBracket");
			keysList.Remove("RightBracket");
			keysList.Remove("LeftAlt");
			keysList.Remove("RightAlt");
			keysList.Remove("LeftCommand");
			keysList.Remove("RightCommand");
			keysList.Remove("LeftControl");
			keysList.Remove("RightControl");
			keysList.Remove("Break");
			keysList.Remove("CapsLock");
			keysList.Remove("AltGr");
			Input.GetKey
			for(int i =0; i < 15; i++) {
				keysList.Remove("F" + (i+1));
			}
			for(int i = 0; i < 9; i++) {
				for(int j = 0; j < 20; j++) {
					keysList.Remove("Joystick" + ((i != 0) ? (i+"") : "") + "Button" +  j);
				}
			}
			*/


			//currently adding all the letters manually
			//see above code for originally 
			keysList.Add("q");
			keysList.Add("w");
			keysList.Add("e");
			keysList.Add("r");
			keysList.Add("t");
			keysList.Add("y");
			keysList.Add("u");
			keysList.Add("i");
			keysList.Add("o");
			keysList.Add("p");
			keysList.Add("a");
			keysList.Add("s");
			keysList.Add("d");
			keysList.Add("f");
			keysList.Add("g");
			keysList.Add("h");
			keysList.Add("j");
			keysList.Add("k");
			keysList.Add("l");
			keysList.Add("z");
			keysList.Add("x");
			keysList.Add("c");
			keysList.Add("v");
			keysList.Add("b");
			keysList.Add("n");
			keysList.Add("m");

			//copying values into static array
			keys = keysList.ToArray();
		}

		SelectControl();
		Debug.Log(savingControl);
	}

	private void SelectControl() {
		string tempSelectedControl = keys[UnityEngine.Random.Range(0,keys.Length)];
		//probably not the best way to do this but all i've got right now
		//trying to make sure we don't "double map" some controls
		while(alreadyMappedKeys.Contains(tempSelectedControl)) {
			tempSelectedControl = keys[UnityEngine.Random.Range(0,keys.Length)];
		}
		alreadyMappedKeys.Add(tempSelectedControl);
		savingControl = (KeyCode)Enum.Parse(typeof(KeyCode), tempSelectedControl, true);
	}
}
