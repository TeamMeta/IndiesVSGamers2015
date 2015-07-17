using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public static ObstacleSpawner Instance;

	public GameObject obstacle;
	public float spawnInterval;

	private Vector3 _groundPosition;
	private Vector3 _aerialPosition;

	void Awake(){
		Instance = this;
	}

	void Start(){
		_groundPosition = transform.FindChild("GroundObstacleSpawnPoint").position;
		_aerialPosition = transform.FindChild("AerialObstacleSpawnPoint").position;

		UnityTimer.Instance.CallRepeating(() =>{
			int determiner = Random.Range(0, 100);
			if(determiner < 50){
				GameObject _obstacle = Instantiate(obstacle, _groundPosition, Quaternion.identity) as GameObject;
			}
			else{
				GameObject _obstacle = Instantiate(obstacle, _aerialPosition, Quaternion.identity) as GameObject;
			}
		}, spawnInterval);
	}



}
