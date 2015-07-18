using UnityEngine;
using System.Collections;

public class LoginManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameJolt.UI.Manager.Instance.ShowSignIn((bool success) =>
        {
            if (success)
            {
                Debug.Log("Yaaaa I can code!!!");
            }
            else
            {
                Debug.Log("Yaaa You can not code!!!");
            }
        });
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Logout()
    {
        var isSignedIn = GameJolt.API.Manager.Instance.CurrentUser != null;
        if (isSignedIn)
        {
            GameJolt.API.Manager.Instance.CurrentUser.SignOut();
            Debug.Log("Logged Out");
        }
    }  
}
