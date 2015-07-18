using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrganVisualTimer : MonoBehaviour {

	public FailedOrgan organ;

	//hack-y handling of flashingItems;
	private float flashTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(organ != null) {
			flashTimer += Time.deltaTime;

			Debug.Log("ORGAN UPDATE"+organ.RhythmPercentage());
//			Debug.Log(flashTimer);
			if(organ.GetType() == typeof(FailedHeart) && !((FailedHeart)organ).onBeat) {
				Debug.Log((int)(flashTimer*10));
				transform.GetChild(1).GetComponent<Image>().fillAmount = 1;
				if((int)(flashTimer*10) % 2 == 0) {
					transform.GetChild(1).GetComponent<Image>().color = new Color(0,1,0,1);
				} else {
					transform.GetChild(1).GetComponent<Image>().color = new Color(0,1,0,0);
				}
			} else {
				transform.GetChild(1).GetComponent<Image>().color = new Color(0,1,0,1);
				transform.GetChild(1).GetComponent<Image>().fillAmount = (organ.RhythmPercentage());
			}
//			
//			transform.GetChild(2).GetComponent<Image>().color = organ.WithinTimeRange() ? Color.green : Color.red;
			transform.GetChild(2).GetComponent<Text>().text = organ.savingControl.ToString();
		}
	}
}
