using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardPopulator : MonoBehaviour {

	public List<Text> leaderboard;

	// Use this for initialization
	void Start () {
		GameJolt.API.Scores.Get((scores) => {

			for(int i=0; i< scores.Length; i++){
				leaderboard[i].text += "  "+scores[i].UserName+"       "+scores[i].Value;
			}
		}, 83423);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
