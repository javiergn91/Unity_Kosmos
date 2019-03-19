using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosmos.Networking
{
    public class NetworkUtils : MonoBehaviour
    {
        //Singleton
        private static NetworkUtils singleton;

        public static NetworkUtils Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = FindObjectOfType<NetworkUtils>();

                return singleton;
            }
        }

        //Delegate definitions
        public delegate void F(bool success, string response);

        /*
        public string GetIPAddress()
        {
            return Network.player.ipAddress;
        }
        */

        public void Request(string url, F onEnd)
        {
            Debug.Log(url);
            StartCoroutine(RequestCoroutine(url, onEnd));
        }

        private IEnumerator RequestCoroutine(string url, F onEnd)
        {
            WWW www = new WWW(url);
            yield return www;

            if(www.error != null && www.error != "")
            {
                onEnd(false, www.error);
            } 
            else
            {
                onEnd(true, www.text);
            }
        }
    }
}


