using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour 
{
	[Header("References")]
	public AudioClip SFX_Win;
	public GameObject FX_Win;

	bool hasWon = false;

	void OnTriggerEnter(Collider other)
	{
		if ( other.CompareTag("Ball") && !hasWon )
		{
			hasWon = true;
			GetComponent<AudioSource>().PlayOneShot( SFX_Win );
			FX_Win.SetActive( true );
		}
	}
}
