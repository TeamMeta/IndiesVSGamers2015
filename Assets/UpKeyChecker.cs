using UnityEngine;
using System.Collections;

public class UpKeyChecker : MonoBehaviour {
    Animator animator;

    public bool delayStart = false;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        if (delayStart)
        {
            animator.SetBool("bDelayStart", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        string key = "bUpKeyComplete";
        bool isGlobal = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("bFadeOut",true);
            animator.SetBool("bTutorialComplete", true);
            string value = "complete";
            
            GameJolt.API.DataStore.Set(key, value, isGlobal, (bool success) => { });
        }
        if (LoginUtilities.LoginManager.isLoggedIn() && !animator.GetBool("bTutorialComplete"))
        {
            GameJolt.API.DataStore.Get(key, isGlobal, (string value) =>
            {
                if (value != null)
                {
                    animator.SetBool("bFadeOut", true);
                    animator.SetBool("bTutorialComplete", true);
                }
            });
        }
    }
}
