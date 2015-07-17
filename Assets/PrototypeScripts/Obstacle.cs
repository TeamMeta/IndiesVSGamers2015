using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public float MoveSpeed;

	private Vector2 _moveVector;
	void Awake(){
		_moveVector = new Vector2(-1,0);
	}

	void Start(){
		UnityTimer.Instance.CallAfterDelay(()=>{
			Destroy(this.gameObject);
		}, 5.0f);
	}

	// Update is called once per frame
	void Update () {
		transform.position+=(Vector3)_moveVector*MoveSpeed*Time.deltaTime;
	}
}
