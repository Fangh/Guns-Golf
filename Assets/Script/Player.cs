using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[Header("References")]
	public OVRGrabber rightHandGrabber;
	public OvrAvatar ovrAvatar;
	public Pistol pistol;

	[Header("Balancing")]
	public float moveSpeed = 10f;


	private GameObject ball;
	private Vector3 ballOriginalPos;
	private bool hasGrabbedWorld = false;
	private Vector3 grabPoint = Vector3.zero;
	private Vector3 lastGrab = Vector3.zero;
	

	// Use this for initialization
	void Start () 
	{
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballOriginalPos = ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( rightHandGrabber.grabbedObject != null )
		{
			ovrAvatar.HandRight.gameObject.SetActive(false);
			if ( OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f )
			{
				pistol.Shoot();
			}
		}
		else
		{
			ovrAvatar.HandRight.gameObject.SetActive(true);
		}

		if ( OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.8f)
		{
			if ( !hasGrabbedWorld )
			{
				hasGrabbedWorld = true;
				grabPoint = ovrAvatar.HandLeft.transform.position;
				lastGrab = grabPoint;
			}
			Vector3 direction = grabPoint - lastGrab;
			// Debug.Log( "grabPoint = " + grabPoint );
			// Debug.Log( "lastGrab = " + lastGrab );
			// Debug.Log( "direction.magnitude = "+ direction.magnitude);

			if ( direction.magnitude > 0f )
			{
				transform.position += direction * 50 * Time.deltaTime;
			}

			lastGrab = ovrAvatar.HandLeft.transform.position;
		}
		else
		{
			if ( hasGrabbedWorld )
				hasGrabbedWorld = false;
		}
		float horizontal = OVRInput.Get( OVRInput.Axis2D.PrimaryThumbstick ).x;
		float vertical = OVRInput.Get( OVRInput.Axis2D.PrimaryThumbstick ).y;
		if ( Mathf.Abs( horizontal ) > 0.1f || Mathf.Abs( vertical ) > 0.1f)
		{
			transform.position -= new Vector3( horizontal, 0, vertical ) * Time.deltaTime * moveSpeed;
		}

		if ( OVRInput.GetDown(OVRInput.Button.One) )
		{
			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
			ball.transform.position = ballOriginalPos;
		}
		
	}
}
