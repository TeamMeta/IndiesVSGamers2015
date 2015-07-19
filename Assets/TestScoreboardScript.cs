using UnityEngine;
using System.Collections;

public class TestScoreboardScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Scoreboard.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () {
		Scoreboard.Instance.Score = 50000;
		Scoreboard.Instance.Multiplier = 3;
	}
}
