using UnityEngine;
using System.Collections;

public class ScoreManager{
    //Function to update the score in the GameJolt Leader
    public string UpdateScore(int scoreVal, bool isGuest, string guestName)
    {
        string scoreText = scoreVal + " miles";
        int tableID = 0; // Set it to 0 for main highscore table.
        string extraData = ""; // This will not be shown on the website. You can store any information.
        if (!isGuest)
        {
            GameJolt.API.Scores.Add(scoreVal, scoreText, tableID, extraData, (bool success) =>
            {
                Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
            });
        }
        else
        {
            GameJolt.API.Scores.Add(scoreVal, scoreText, guestName, tableID, extraData, (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
            });
        }
        return scoreText;
    }    
}

public class ScoreStore
{
    //All weights for score calculation
    private int pwrWght = 2;
    private int normalWght = 1;
    private int humanWght = 5;

    private int milesPoint = 0;     //Any moment of time stores the points for the run distance
    private int buttonMashPoint = 0;    //Any moment of time stores the points for each button mash
    private int humanEatPoint = 0;      //Any moment of time stores the points for humans caught
    private int comboGenerator = 0;     //Any moment of time stores the last combo generator

    public void setMilesPoint(int val)
    {
        this.milesPoint = val * this.comboGenerator;
    }
    public int getMilesPoint()
    {
        return this.milesPoint;
    }

    public void setButtonMashPoint(int val)
    {
        this.buttonMashPoint = val * this.comboGenerator;
    }
    public int getButtonMashPoint()
    {
        return this.buttonMashPoint;
    }

    public void setHumanEaterPoint(int val)
    {
        this.humanEatPoint = val * this.comboGenerator;
    }
    public int getHumanEaterPoint()
    {
        return this.humanEatPoint;
    }

    public void setComboGenerator(int val)
    {
        this.comboGenerator = val;
    }
    public int getComboGenerator()
    {
        return this.comboGenerator;
    }
    //Function to calculate score 
    public int CalcVal()
    {
        int retVal = pwrWght * this.buttonMashPoint + normalWght * this.milesPoint + humanWght * this.humanEatPoint;
        return retVal;
    }
}
