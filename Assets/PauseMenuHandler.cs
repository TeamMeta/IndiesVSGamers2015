using UnityEngine;
using System.Collections;

public class PauseMenuHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ContinuePressed() {
		GameStateManager.Instance.State = GameManager.GameState.Running;
	}

	public void QuitToMenuPressed() {
		GameStateManager.Instance.State = GameManager.GameState.MainMenu;
	}
}
