using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public static Floor Instance;

	void Awake(){
		Instance = this;
	}

	public float FloorLevel{
		get{
			return transform.position.y;
		}
	}
}
