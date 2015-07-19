using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScoreManager : MonoBehaviour {

    public static FinalScoreManager Instance;


    public Animator _anim;
    public Text scoreText;
    public Text scoreVal;
    public float _switchOffTime;


    void Awake()
    {
        Instance = this;
        _anim = transform.FindChild("Container").GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Init()
    {
        _anim.SetTrigger("Init");
    }



    #region SETTING_SCORES
    
    public void SetAll(int totscore, int humaneaten, int distancecovered, int organshealed)
    {
        string txtDescription = "";
        string txtVal = "";
        if (distancecovered > 0)
        {
            txtDescription = txtDescription + "Distance Covered: \n";
            txtVal = txtVal + distancecovered + " \n";
        }
        if (organshealed > 0)
        {
            txtDescription = txtDescription + "Organ health points: \n";
            txtVal = txtVal + "+" + organshealed + " \n";
        }
        if (humaneaten > 0)
        {
            txtDescription = txtDescription + "Humans Caught): \n";
            txtVal = txtVal + "x" + humaneaten + " \n";
        }
        if (totscore > 0)
        {
            txtDescription = txtDescription + "Total Score: \n";
            txtVal = txtVal  + totscore + " \n";
        }
        scoreText.text = txtDescription;
        scoreVal.text = txtVal;
    }

    #endregion
}
