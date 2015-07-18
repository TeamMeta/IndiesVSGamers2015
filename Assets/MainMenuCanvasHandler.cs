using UnityEngine;
using System.Collections;

public class MainMenuCanvasHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HandlePlayButtonPressed() {
		GameStateManager.Instance.State = GameManager.GameState.Running;
	}
}
