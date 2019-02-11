using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosmos.UI.Window
{
	public class Screen : MonoBehaviour 
	{
		public Transition.FluxAnimation fluxAnimation;
		public bool isOpen = false;

		public virtual void Open()
		{
			isOpen = true;
			fluxAnimation.StartOpeningAnimation ();
		}

		public virtual void Close()
		{
			isOpen = false;
			fluxAnimation.StartClosingAnimation ();
		}
	}
}

