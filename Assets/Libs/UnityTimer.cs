using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void TimerDelegate();


/// <summary>
/// Simple class which allows you to pass in a delegate which will be called after a delay.
/// Gets around Unity's string based invoke
/// </summary>
public class UnityTimer : MonoBehaviour {

	public static UnityTimer Instance;
	//Lazy Singleton
	void Awake(){
		Instance=this;
	}


	//The delayed function pasted as a delegate
	public void CallAfterDelay(TimerDelegate theFunction, float duration){
		StartCoroutine(DelayCoroutine(theFunction, duration));
	}

	//
	public void CallRepeating(TimerDelegate theFunction, float interval){
		StartCoroutine(RepeatCoroutine(theFunction, interval));
	}

	//Coroutine used to create the delay
	IEnumerator DelayCoroutine(TimerDelegate theFunction, float duration){
		yield return new WaitForSeconds(duration);
		theFunction();
	}

	//Coroutine for to create interval
	IEnumerator RepeatCoroutine(TimerDelegate theFunction, float interval){
		while(true){
			theFunction();
			yield return new WaitForSeconds(interval);
		}
	}
}
