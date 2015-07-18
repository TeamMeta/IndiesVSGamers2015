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
			transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(90f,(int)(90*organ.RhythmPercentage()/5f)*5f);
//			transform.GetChild(1).GetComponent<Image>().fillAmount = (organ.organHealth/100f);
//			transform.GetChild(2).GetComponent<Image>().color = organ.WithinTimeRange() ? Color.green : Color.red;
			transform.GetChild(3).GetComponent<Text>().text = organ.savingControl.ToString();
		}
	}
}
