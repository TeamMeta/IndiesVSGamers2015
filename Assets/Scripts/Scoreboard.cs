using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// Static singleton for the scoreboard again, just set variable scoe and Multiplier to update the Ui 
/// </summary>
public class Scoreboard : MonoBehaviour {


	public static Scoreboard Instance;


	public Animator _anim;
	public Text scoreText;
	public Text multiplierText;


	void Awake(){
		Instance = this;
	}


	public void Init(){
		_anim.SetTrigger("Init");
	}



	#region SETTING_SCORES
	private int _score;
	public int Score{
		get{
			return _score;
		}
		set{
			_setScore(value);
		}
	}


	private int _multiplier;
	public int Multiplier{
		get{
			return _multiplier;
		}

		set{
			_setMultiplier(value);
		}
	}
	

	private void _setScore(int score){
		_score = score;

		scoreText.text = score+"";


	}


	private void _setMultiplier(int multiplier){
		_multiplier = multiplier;

		multiplierText.text = "x"+multiplier;
	}

	#endregion



}
