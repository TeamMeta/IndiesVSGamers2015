using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrganVisualTimer : MonoBehaviour {

	public FailedOrgan organ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(organ != null) {
			transform.GetChild(0).GetComponent<Image>().fillAmount = organ.RhythmPercentage();
		}
	}
}
