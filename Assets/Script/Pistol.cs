using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour 
{
	[ Header("References") ]
	public AudioClip SFX_Shoot;
	public GameObject muzzleFlash;
	public Transform laser;
	public GameObject bulletPrefab;

	[Header("Balancing")]
	public float cooldown = 2f;
	public float force = 10f;


	private float currentCooldown;
	private bool canShoot = false;

	// Use this for initialization
	void Start () 
	{
		currentCooldown = cooldown;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( currentCooldown < 0 && !canShoot )
		{
			canShoot = true;
		}
		else
		{
			currentCooldown -= Time.deltaTime;
		}
		
	}

	public void Shoot()
	{
		if ( !canShoot )
			return;

		currentCooldown = cooldown;
		canShoot = false;

		muzzleFlash.SetActive(true);
		GetComponent<Animator>().SetTrigger("Shoot");
		Invoke("DisableMuzzle", 0.5f);
		GetComponent<AudioSource>().PlayOneShot(SFX_Shoot);
		Quaternion rotation = transform.localRotation * Quaternion.Euler( new Vector3(90,0,0));
		GameObject bullet = GameObject.Instantiate( bulletPrefab, laser.position, rotation ) as GameObject;

		// RaycastHit hit;
		// if( Physics.Raycast(laser.position, laser.forward, out hit,  50f) )
		// {
		// 	if( hit.collider.CompareTag("Ball") )
		// 	{
		// 		Vector3 direction = hit.collider.transform.position - laser.position;
		// 		hit.collider.GetComponent<Rigidbody>().AddForce( direction * force, ForceMode.Impulse );
		// 	}
		// }

	}

	void DisableMuzzle()
	{
		muzzleFlash.SetActive(false);
	}
}
