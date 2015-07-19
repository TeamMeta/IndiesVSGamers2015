using UnityEngine;
using System.Collections;

public class LoopingRoad : MonoBehaviour {

	public float reposition = 34;
	public float length = 27;

	public GameObject goToAnchor, anchor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < length || (goToAnchor != null && goToAnchor.transform.position.x < transform.position.x)) {
			Debug.Log("HERE");
			transform.position = new Vector3(anchor != null ? (goToAnchor.transform.position.x - anchor.transform.localPosition.x) : reposition, transform.position.y, transform.position.z);
		} else {
			transform.Translate(new Vector3(-3*Time.deltaTime, 0f, 0f));
		}
	}
}
