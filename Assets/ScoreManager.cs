﻿using UnityEngine;
using System.Collections;
using LoginUtilities;
using System;

namespace ScoreUtilities
{
    public class ScoreManager
    {
        //Table ID
        private enum TableIDs
        {
            MainTable = 82836,
            TestScoreBoardTable = 83424,
            ProductionLeaderBoard = 83423
        }

        private int tableID = (int)TableIDs.MainTable; //.TestScoreBoardTable;
        //All weights for score calculation
        private int pwrWght = 2;
        private int normalWght = 1;
        private int humanWght = 5;

        private int milesPoint = 0;     //Any moment of time stores the points for the run distance
        private int buttonMashPoint = 0;    //Any moment of time stores the points for each button mash
        private int humanEatPoint = 0;      //Any moment of time stores the points for humans caught
        private int comboGenerator = 1;     //Any moment of time stores the last combo generator

        //Store score board info for posting
        private GameJolt.API.Objects.Score[] lstTopScores;

        //Test Stub
        public bool bTest = false;

        //Turn on to show leaderboard on game run end
        //To make it easy to show the leaderboard on button click we just 
        //need to assign ShowLeaderBoards() function from GameJoltAPI>UI to the button.
        public bool bDefaultShowLeaderBoard = false;

        //variable to toggle use of custom scoreboard
        public bool bCustShowLeaderboard = false;

        //Variables for extracting scores
        public int nUsers = 10;
        public bool currentUserOnly = false;


        public void setMilesPoint(int val)
        {
            if (bTest) { this.milesPoint = PlayerPrefs.GetInt("milesPoint"); }
            this.milesPoint += val * this.comboGenerator;
            if (bTest) PlayerPrefs.SetInt("milesPoint",this.milesPoint);
        }
        public int getMilesPoint()
        {
            if (bTest) { this.milesPoint = PlayerPrefs.GetInt("milesPoint"); }
            return this.milesPoint;
        }

        public void setButtonMashPoint(int val)
        {
            if (bTest) { this.buttonMashPoint = PlayerPrefs.GetInt("buttonMashPoint"); }
            this.buttonMashPoint += val * this.comboGenerator;
            if (bTest) PlayerPrefs.SetInt("buttonMashPoint", this.buttonMashPoint);
        }
        public int getButtonMashPoint()
        {
            if (bTest) { this.buttonMashPoint = PlayerPrefs.GetInt("buttonMashPoint"); }
            return this.buttonMashPoint;
        }

        public void setHumanEaterPoint(int val)
        {
            if (bTest) { this.humanEatPoint = PlayerPrefs.GetInt("humanEatPoint"); }
            this.humanEatPoint += val * this.comboGenerator;
            if (bTest) PlayerPrefs.SetInt("humanEatPoint", this.humanEatPoint);
        }
        public int getHumanEaterPoint()
        {
            if (bTest) { this.humanEatPoint = PlayerPrefs.GetInt("humanEatPoint"); }
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

        //Set all values in one go
        public void SetScoreVals(int mPT, int hEPT, int btnMP, int cGn)
        {
            this.setComboGenerator(cGn);
            this.setButtonMashPoint(btnMP);
            this.setHumanEaterPoint(hEPT);
            this.setMilesPoint(mPT);
        }

        //Function to calculate score 
        public int CalcVal()
        {
            int retVal = pwrWght * this.getButtonMashPoint() + normalWght * this.getMilesPoint() + humanWght * this.getHumanEaterPoint();
            if (retVal == 0) retVal = 1;
            return retVal;
        }

        //Function to update the score in the GameJolt Leader
        public string UpdateScore()
        {
            int scoreVal = this.CalcVal();
            string scoreText = scoreVal + " miles";
            string extraData = ""; // This will not be shown on the website. You can store any information.
            Debug.Log("Score: "+scoreVal);
            if (LoginManager.isLoggedIn())
            {
                GameJolt.API.Scores.Add(scoreVal, scoreText, tableID, extraData, addSuccess);
            }
            else
            {
                GameJolt.API.Scores.Add(scoreVal, scoreText, LoginManager.guestName, tableID, extraData, addSuccess);
            }
            return scoreText;
        }

        private void addSuccess(bool worked)
        {
            Debug.Log(string.Format("Score Add {0}.", worked ? "Successful" : "Failed"));
            if (worked)
            {
                if (bTest)
                {
                    PlayerPrefs.DeleteAll();
                }
                if (bDefaultShowLeaderBoard)
                {
                    Debug.Log("Calling GameJolt Leaderboard");
                    GameJolt.UI.Manager.Instance.ShowLeaderboards((bool success) =>
                    {
                        Debug.Log(string.Format("Leaderboard Call {0}.", success ? "Successful" : "Failed"));
                    });
                }
                if (bCustShowLeaderboard)
                {
                    Debug.Log("Calling Custom Leaderboard");
                    GameJolt.API.Scores.Get(GenerateCustomMenu, tableID,nUsers,currentUserOnly);
                }
            }
        }

        //Function to generate custom scoreboard using GameJolt API
        private void GenerateCustomMenu(GameJolt.API.Objects.Score[] lstScores)
        {
            if(lstScores != null && lstScores.Length > 0)
            {
                //for (int i = 0; i<lstScores.Length; i++)
                //{
                //    Debug.Log((lstScores[i].UserName!=string.Empty? lstScores[i].UserName:lstScores[i].GuestName) + " " + lstScores[i].Text); 
                //}
                lstTopScores = lstScores;
            }
            if (LoginManager.isLoggedIn())
            {
                GameJolt.API.Scores.Get(GenerateCustomMenuFinalPhase, tableID, 100, true);
            }
            else
            {
                PopulateLeaderBoard();
            }
        }

        private void GenerateCustomMenuFinalPhase(GameJolt.API.Objects.Score[] lstScores)
        {
            if (lstScores != null && lstScores.Length > 0)
            {
                for (int i = 0; i < lstScores.Length; i++)
                {
                    //Debug.Log("User Data: " + lstScores[i].Value + " time:" + lstScores[i].Time);
                    if (lstScores[i].Time.Contains("seconds"))
                    {
                        lstTopScores[lstTopScores.Length - 1] = lstScores[i];
                        break;
                    }
                }
            }
            PopulateLeaderBoard();
        }

        public void PopulateLeaderBoard()
        {
            for (int i = 0; i < lstTopScores.Length; i++)
            {
                Debug.Log("User Data: " + (lstTopScores[i].UserName != string.Empty ? lstTopScores[i].UserName : lstTopScores[i].GuestName) + " Value: "
                    + lstTopScores[i].Value + " time:" + lstTopScores[i].Time);
            }
        }
    }
}