using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kosmos.UI.Transition;

namespace Kosmos.UI
{
    public class Alert : MonoBehaviour
    {
        private static Alert singleton;

        public static Alert Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = FindObjectOfType<Alert>();

                return singleton;
            }
        }

        public GameObject content;
        public FluxAnimation anim;
        public Text message;

        private bool isOpen = false;

        public void Show(string message)
        {
            if (isOpen)
                return;

            isOpen = true;
            content.SetActive(true);
            this.message.text = message;
            anim.StartOpeningAnimation();
        }

        public void Hide()
        {
            if (!isOpen)
                return;

            isOpen = false;
            anim.StartClosingAnimation();
        }

        void Start()
        {
            content.SetActive(false);
        }

        void Update()
        {
            if (Input.GetMouseButton(0) && isOpen)
                Hide();
        }
    }

}

