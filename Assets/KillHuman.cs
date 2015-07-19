using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillHuman : MonoBehaviour {

	public static bool killthehuman;

	private float timerToSpawnSplat1 = 0.2f,timerToSpawnSplat2=0.4f,timerToSpawnSplat3 = 0.6f,timerToSpawnSplat4 = 0.8f;
	private float lastTimer;

	// Use this for initialization
	void Start () {
		killthehuman = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(killthehuman) {
			if(timerToSpawnSplat1 <= 0 && timerToSpawnSplat1 != -100) {
				transform.GetChild(0).GetComponent<Image>().color = new Color(1,0,0,1);
				transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600f,600f),Random.Range(-300f,300f));
				timerToSpawnSplat1 = -100;
			}
			if(timerToSpawnSplat2 <= 0 && timerToSpawnSplat2 != -100) {
				transform.GetChild(1).GetComponent<Image>().color = new Color(1,0,0,1);
				transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600f,600f),Random.Range(-300f,300f));
				timerToSpawnSplat2 = -100;
			}
			if(timerToSpawnSplat3 <= 0 && timerToSpawnSplat3 != -100) {
				transform.GetChild(2).GetComponent<Image>().color = new Color(1,0,0,1);
				transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600f,600f),Random.Range(-300f,300f));
				timerToSpawnSplat3 = -100;
			}
			if(timerToSpawnSplat4 <= 0 && timerToSpawnSplat4 != -100) {
				transform.GetChild(3).GetComponent<Image>().color = new Color(1,0,0,1);
				transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600f,600f),Random.Range(-300f,300f));
				timerToSpawnSplat4 = -100;
			}
			if(timerToSpawnSplat4 == -100) {
				GetComponent<Image>().color = new Color(1,0,0,Mathf.MoveTowards(GetComponent<Image>().color.a, 1, Time.deltaTime*2F));
				lastTimer += Time.deltaTime;
			}
			if(timerToSpawnSplat1 != -100) {
				timerToSpawnSplat1 -= Time.deltaTime;
			}
			if(timerToSpawnSplat2 != -100) {
				timerToSpawnSplat2 -= Time.deltaTime;
			}
			if(timerToSpawnSplat3 != -100) {
				timerToSpawnSplat3 -= Time.deltaTime;
			}
			if(timerToSpawnSplat4 != -100) {
				timerToSpawnSplat4 -= Time.deltaTime;
			}

			if(lastTimer > 2f) {
				killthehuman = false;
			}
		} else {
			lastTimer = 0;
			timerToSpawnSplat1 = 0.2f;
			timerToSpawnSplat2 = 0.4f;
			timerToSpawnSplat3 = 0.6f;
			timerToSpawnSplat4 = 0.8f;
			GetComponent<Image>().color = new Color(1,0,0,Mathf.MoveTowards(GetComponent<Image>().color.a, 0, Time.deltaTime));
			transform.GetChild(0).GetComponent<Image>().color = new Color(1,0,0,Mathf.MoveTowards(transform.GetChild(0).GetComponent<Image>().color.a, 0, Time.deltaTime));
			transform.GetChild(1).GetComponent<Image>().color = new Color(1,0,0,Mathf.MoveTowards(transform.GetChild(1).GetComponent<Image>().color.a, 0, Time.deltaTime));
			transform.GetChild(2).GetComponent<Image>().color = new Color(1,0,0,Mathf.MoveTowards(transform.GetChild(2).GetComponent<Image>().color.a, 0, Time.deltaTime));
			transform.GetChild(3).GetComponent<Image>().color = new Color(1,0,0,Mathf.MoveTowards(transform.GetChild(3).GetComponent<Image>().color.a, 0, Time.deltaTime));
		}
	}


}
