using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboTextCalc : MonoBehaviour {

    private int duration = 5;

	// Update is called once per frame
	void Update () {
        Fade();
	}

    //Fade function
    void Fade()
    {
        if(Time.time > duration)
        {
            Destroy(gameObject);
        }

        Color txtColor = GetComponent<Text>().color;
        float ratio = Time.time / duration;
        txtColor.a = Mathf.Lerp(1, 0, ratio);
        GetComponent<Text>().color = txtColor;
    }

}
