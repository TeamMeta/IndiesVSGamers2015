using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrganVisualTimer : MonoBehaviour {

	public FailedOrgan organ;

	public Sprite[] images;

	//hack-y handling of flashingItems;
	private float flashTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(organ != null) {
			flashTimer += Time.deltaTime;

			if(organ.GetType() == typeof(FailedHeart)) {
				transform.GetChild(3).GetChild(0).GetComponent<Animator>().SetInteger("Health", 5 - ((int)(organ.organHealth/20)));
				if(!((FailedHeart)organ).onBeat) {
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
			} else {
				transform.GetChild(1).GetComponent<Image>().color = new Color(0,1,0,1);
				transform.GetChild(1).GetComponent<Image>().fillAmount = (organ.RhythmPercentage());
				transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = images[images.Length - ((int)(organ.organHealth/(100/images.Length)))];
			}
			

			if(organ.GetType() == typeof(FailedLegs)) {
				if((int)(flashTimer*10) % 2 == 0) {
					transform.GetChild(1).GetComponent<Image>().color = new Color(0,1,0,1);
				} else {
					transform.GetChild(1).GetComponent<Image>().color = new Color(0,1,0,0);
				}
			}
			transform.GetChild(2).GetComponent<Text>().text = organ.savingControl.ToString();
			if(organ.WithinTimeRange() && Input.GetKeyDown(organ.savingControl)) {
				transform.GetChild(2).GetComponent<Text>().color = Color.green;
			} else if (organ.WithinTimeRange()) {
				transform.GetChild(2).GetComponent<Text>().color = Color.red;
			} else {
				transform.GetChild(2).GetComponent<Text>().color = Color.white;
			}

			if(Input.GetKey(organ.savingControl)) {
				transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
				transform.GetChild(1).transform.localScale = new Vector3(-1,1,1);
				transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-10);
				transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-10);
				transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-10);
			} else {
				transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
				transform.GetChild(1).transform.localScale = new Vector3(1,1,1);
				transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
				transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
				transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
			}
		}
	}
}
