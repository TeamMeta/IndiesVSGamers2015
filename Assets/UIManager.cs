using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {



	}

	public void FailOrgan(FailedOrgan fo) {
		if(fo.GetType() == typeof(FailedHeart)) {
			transform.FindChild("FailedHeart").GetComponent<CanvasGroup>().alpha = 1;
			transform.FindChild("FailedHeart").GetComponent<OrganVisualTimer>().organ = fo;
            //if (fo.onBeat)
            //{
            //    transform.FindChild("FailedHeart").transform.FindChild("Button Mash Counter").GetComponent<Animator>().SetTrigger("Init");
            //}
        } else if (fo.GetType() == typeof(FailedLungs)) {
			transform.FindChild("FailedLungs").GetComponent<CanvasGroup>().alpha = 1;
			transform.FindChild("FailedLungs").GetComponent<OrganVisualTimer>().organ = fo;
            //if (fo.onBeat)
            //{
            //    transform.FindChild("FailedLungs").transform.FindChild("Button Mash Counter").GetComponent<Animator>().SetTrigger("Init");
            //}
        } else if (fo.GetType() == typeof(FailedLegs)) {
			transform.FindChild("FailedLegs").GetComponent<CanvasGroup>().alpha = 1;
			transform.FindChild("FailedLegs").GetComponent<OrganVisualTimer>().organ = fo;
            //if (fo.onBeat)
            //{
            //    transform.FindChild("FailedLegs").transform.FindChild("Button Mash Counter").GetComponent<Animator>().SetTrigger("Init");
            //}
        }
	}


	public void HealOrgan(FailedOrgan fo){
		if(fo.GetType() == typeof(FailedHeart)) {
			transform.FindChild("FailedHeart").GetComponent<CanvasGroup>().alpha = 0;
			transform.FindChild("FailedHeart").GetComponent<OrganVisualTimer>().organ = null;
            //if (fo.onBeat)
            //{
            //    Debug.Log("Checking Animation");
            //    transform.FindChild("FailedHeart").transform.FindChild("Button Mash Counter").GetComponent<Animator>().SetTrigger("Init");
            //}
        } else if (fo.GetType() == typeof(FailedLungs)) {
			transform.FindChild("FailedLungs").GetComponent<CanvasGroup>().alpha = 0;
			transform.FindChild("FailedLungs").GetComponent<OrganVisualTimer>().organ = null;
            //if (fo.onBeat)
            //{
            //    transform.FindChild("FailedLungs").transform.FindChild("Button Mash Counter").GetComponent<Animator>().SetTrigger("Init");
            //}
        } else if (fo.GetType() == typeof(FailedLegs)) {
			transform.FindChild("FailedLegs").GetComponent<CanvasGroup>().alpha = 0;
			transform.FindChild("FailedLegs").GetComponent<OrganVisualTimer>().organ = null;
            //if (fo.onBeat)
            //{
            //    transform.FindChild("FailedLegs").transform.FindChild("Button Mash Counter").GetComponent<Animator>().SetTrigger("Init");
            //}
        }
	}

    public void CleanUI()
    {
        transform.FindChild("FailedHeart").GetComponent<CanvasGroup>().alpha = 0;
        transform.FindChild("FailedHeart").GetComponent<OrganVisualTimer>().organ = null;
        transform.FindChild("FailedLungs").GetComponent<CanvasGroup>().alpha = 0;
        transform.FindChild("FailedLungs").GetComponent<OrganVisualTimer>().organ = null;
        transform.FindChild("FailedLegs").GetComponent<CanvasGroup>().alpha = 0;
        transform.FindChild("FailedLegs").GetComponent<OrganVisualTimer>().organ = null;
    }

}
