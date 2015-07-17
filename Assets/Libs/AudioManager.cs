using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public static AudioManager Instance;

	[System.Serializable]
	public class SoundClass{
		public string soundName;
		public AudioClip soundClip;
		[HideInInspector]
		public float lastPlayed = -1;

	}

	public List<SoundClass> sounds;


	private Dictionary<string, SoundClass> soundDict;
	void Awake(){
		Instance = this;

		//Build Dictionaary
		soundDict = new Dictionary<string, SoundClass>();
		foreach(SoundClass sound in sounds){
			soundDict.Add(sound.soundName, sound);
		}
	}

	public void playSound(string soundname){
		SoundClass sound;
		bool found = soundDict.TryGetValue(soundname, out sound);
		if(found){
			gameObject.GetComponent<AudioSource>().PlayOneShot(sound.soundClip);
		}
		else{
			Debug.Log (soundname+" is not registered with soundmanager");
		}
	}

	public void playWithMinInterval(string soundname, float interval){
		SoundClass sound;
		bool found = soundDict.TryGetValue(soundname, out sound);
		if(found){
			if(sound.lastPlayed < 0){
				gameObject.GetComponent<AudioSource>().PlayOneShot(sound.soundClip);
				sound.lastPlayed = Time.time;
			}
			else if(Time.time - sound.lastPlayed > interval){
				gameObject.GetComponent<AudioSource>().PlayOneShot(sound.soundClip);
				sound.lastPlayed = Time.time;
			}
		}

	}

	public void playSoundAt(string soundname, Vector3 position){
		SoundClass sound;
		bool found = soundDict.TryGetValue(soundname, out sound);
		if(found){
			AudioSource.PlayClipAtPoint(sound.soundClip, position);
		}
		else{
			Debug.Log (soundname+" is not registered with soundmanager");
		}
	}

	public void playSoundAtWithMinInterval(string soundname, Vector3 position, float interval){
		SoundClass sound;
		bool found = soundDict.TryGetValue(soundname, out sound);
		if(found){
			if(sound.lastPlayed < 0){
				AudioSource.PlayClipAtPoint(sound.soundClip, position);
				sound.lastPlayed = Time.time;
			}
			else if(Time.time - sound.lastPlayed > interval){
				AudioSource.PlayClipAtPoint(sound.soundClip, position);
				sound.lastPlayed = Time.time;
			}
		}
	}
}
