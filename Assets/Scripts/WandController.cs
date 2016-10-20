using UnityEngine;
using System.Collections;

public class WandController : MonoBehaviour
{
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	private GameObject pickup;

	// Use this for initialization
	void Start()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update()
	{
		if (controller == null)
		{
			return;
		}
		if (controller.GetPressDown(gripButton) && pickup != null)
		{
			pickup.transform.parent = this.transform;
			pickup.GetComponent<Rigidbody>().isKinematic = true;
		}
		if (controller.GetPressUp(gripButton))
		{
			pickup.transform.parent = null;
			pickup.GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		pickup = collider.gameObject;
	}

	private void OnTriggerExit(Collider collider)
	{
		pickup = null; 
	}
}
