using UnityEngine;
using System.Collections;

public class OrganVisualTimer : MonoBehaviour {

	public FailedOrgan organ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(1f, 1 - organ.RhythmPercentage(), 1f);
	}
}
