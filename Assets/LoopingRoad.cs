using UnityEngine;
using System.Collections;

public class LoopingRoad : MonoBehaviour {

	public float reposition = 34;
	public float length = 27;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x > -length) {
			transform.Translate(new Vector3(-3*Time.deltaTime, 0f, 0f));
		} else {
			transform.position = new Vector3(reposition,transform.position.y, transform.position.z);
		}
	}
}
