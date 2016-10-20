using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour
{
	public Rigidbody body;

	private bool currentlyInteracting;

	private float velocityFactor = 20000f;
	private Vector3 posDelta;

	private float rotationFactor = 400f;
	private Quaternion rotationDelta;
	private float angle;
	private Vector3 axis;

	private WandController attachedWand;

	private Transform interactionPoint;

	// Use this for initialization
	void Start()
	{
		body = GetComponent<Rigidbody>();
		interactionPoint = new GameObject().transform;
		velocityFactor /= body.mass;
	}

	// Update is called once per frame
	void Update()
	{
		if (attachedWand && currentlyInteracting)
		{
			posDelta = attachedWand.transform.position - interactionPoint.position;
			this.body.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;
		}
	}

	public void BeginInteraction(WandController wand)
	{
		attachedWand = wand;
		interactionPoint.position = wand.transform.position;
		interactionPoint.rotation = wand.transform.rotation;
		interactionPoint.SetParent(transform, true);
		currentlyInteracting = true;
	}

	public void EndInteraction(WandController wand)
	{
		if (wand == attachedWand)
		{
			attachedWand = null;
			currentlyInteracting = false;
		}
	}

	public bool IsInteracting()
	{
		return currentlyInteracting;
	}
}
