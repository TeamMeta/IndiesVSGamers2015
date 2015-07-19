using UnityEngine;
using System.Collections;
using ScoreUtilities;

public delegate void CatchScoreEvent();
//Use the organ tyoe to determine where the UI popup should show.
public delegate void HealScoreEvent(OrganType type ,int score);


public class ScoreManager : MonoBehaviour {

	//Deep's Score Calculator
	private ScoreCalculator calculator;

	public float distanceSpeed;

	public static ScoreManager Instance;


	void Awake(){
		Instance = this;

		calculator =  new ScoreCalculator();

	}



	void Update(){

		if(_isZombieMoving()){

			calculator.setMilesPoint( distanceSpeed * Time.deltaTime);



//			Debug.Log("SCORE:"+calculator.CalcVal());
		}

		Scoreboard.Instance.Score = (int) calculator.CalcVal();
	}


	//Update score only when game is running and when zombie is movinf
	bool _isZombieMoving(){

		if(GameStateManager.Instance.State == GameManager.GameState.Running){

			if(ZombieStateManager.Instance.State == PlayerManager.PlayerState.Ducking || ZombieStateManager.Instance.State == PlayerManager.PlayerState.Running || ZombieStateManager.Instance.State == PlayerManager.PlayerState.Jumping){
				return true;
			}

		}

		return false;
	}

	//Called when an organ is healed
	public void OnOrganHealed(OrganType type, int scoreIncrement){
		Debug.Log(type.ToString() +"  " + scoreIncrement);

		calculator.setButtonMashPoint(scoreIncrement);
	}


	//Call when the human is caught
	public void OnHumanCaught(){
		Debug.Log("HUMAN CAUGHT");

		calculator.setHumanEaterPoint(1);

		Scoreboard.Instance.Multiplier = calculator.getHumanEaterPoint();
	}

}
