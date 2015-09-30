using UnityEngine;
using System.Collections;

namespace LoginUtilities
{
    public class LoginManager : MonoBehaviour
    {
        public static string guestName = "Tester";
		public GameObject cancelMessage; 
		public GameObject loginSuccess; 
		Animator animator;

        // Use this for initialization
        void Start()
        {
			if(isLoggedIn())
			{
				animator = cancelMessage.GetComponent<Animator>();
				GameJolt.UI.Manager.Instance.ShowSignIn((bool success) =>
				                                        {
					if (success)
					{
						loginSuccess.SetActive(true); 
						animator.SetBool("cancelled", true); 
					}
					else
					{
						// cancelMessage; 
						cancelMessage.SetActive(true);
						animator.SetBool("cancelled",true);
					}
				});
			}

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