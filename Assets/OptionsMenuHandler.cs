using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenuHandler : MonoBehaviour {

	public Button muteButton, unMuteButton;

	public AudioMixer master;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("MuteAudio", 0) == 0) {
			unMuteButton.interactable = false;
			muteButton.interactable = true;
			master.SetFloat("MasterVolume", 0);
//			unMuteButton.GetComponent<Animator>().SetTrigger("Disabled");
		} else {
			unMuteButton.interactable = true;
			muteButton.interactable = false;
			master.SetFloat("MasterVolume", -80f);
//			unMuteButton.GetComponent<Animator>().SetTrigger("Normal");
		}
	}

	public void MuteButtonPressed() {
		PlayerPrefs.SetInt("MuteAudio", 1);
	}

	public void UnMuteButtonPressed() {
		PlayerPrefs.SetInt("MuteAudio", 0);
	}
}
