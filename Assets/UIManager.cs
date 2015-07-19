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
//		for(int i = 0; i < transform.childCount; i++) {
//			if(transform.GetChild(i).GetComponent<CanvasGroup>().alpha == 0) {
//				transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
//				transform.GetChild(i).GetComponent<OrganVisualTimer>().organ = fo;
//				break;
//			}
//		}

		if(fo.GetType() == typeof(FailedHeart)) {
			transform.FindChild("FailedHeart").GetComponent<CanvasGroup>().alpha = 1;
			transform.FindChild("FailedHeart").GetComponent<OrganVisualTimer>().organ = fo;
		} else if (fo.GetType() == typeof(FailedLungs)) {
			transform.FindChild("FailedLungs").GetComponent<CanvasGroup>().alpha = 1;
			transform.FindChild("FailedLungs").GetComponent<OrganVisualTimer>().organ = fo;
		} else if (fo.GetType() == typeof(FailedLegs)) {
			transform.FindChild("FailedLegs").GetComponent<CanvasGroup>().alpha = 1;
			transform.FindChild("FailedLegs").GetComponent<OrganVisualTimer>().organ = fo;
		}
	}
}
