using UnityEngine;
using System.Collections;

namespace LoginUtilities
{
    public class LoginManager : MonoBehaviour
    {
        public static string guestName = "Tester";

        // Use this for initialization
        void Start()
        {
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
        void Update()
        {

        }

        public static bool isLoggedIn()
        {
            var isSignedIn = GameJolt.API.Manager.Instance.CurrentUser != null;
            return isSignedIn;
        }

        public void Logout()
        {
            var isSignedIn = isLoggedIn();
            if (isSignedIn)
            {
                GameJolt.API.Manager.Instance.CurrentUser.SignOut();
                Debug.Log("Logged Out");
            }
        }
    }
}