using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FailOrgan(FailedOrgan fo) {
		for(int i = 0; i < transform.childCount; i++) {
			if(transform.GetChild(i).GetComponent<CanvasGroup>().alpha == 0) {
				transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
				transform.GetChild(i).GetComponent<OrganVisualTimer>().organ = fo;
				break;
			}
		}
	}



}
