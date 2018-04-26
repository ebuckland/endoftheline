using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


	// constants
	public float PLAYER_FORCE = 2f;
	private Rigidbody2D RB;
	public GameObject bulletGO;
	public GameObject bulletOrigin;


	// lists
	private List<Bullet> bulletlist;



	// Use this for initialization
	void Start ()
	{

		RB = this.GetComponent<Rigidbody2D> ();

		bulletlist = new List<Bullet>();

	}
	
	// Update is called once per frame
	void Update ()
	{

		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		Vector2 dir = new Vector2 (
			              mousePosition.x - transform.position.x, 
			              mousePosition.y - transform.position.y
		              );

		transform.up = dir;
		
	}

	void FixedUpdate ()
	{
	

		Vector2 direction = Vector2.zero;

		if (Input.GetKey (KeyCode.W)) {
		
			direction += Vector2.up;
		
		}
		if (Input.GetKey (KeyCode.A)) {

			direction += Vector2.left;

		}
		if (Input.GetKey (KeyCode.S)) {

			direction += Vector2.down;

		}
		if (Input.GetKey (KeyCode.D)) {

			direction += Vector2.right;

		}

		if (direction != Vector2.zero) {
		
			RB.AddForce (direction.normalized * PLAYER_FORCE);
		
		}

		if (Input.GetMouseButtonDown (0)) {
		
			shoot ();
		
		}
	
	}


	void shoot ()
	{
		Bullet bullet = new Bullet ();
		bullet.init (bulletGO, 0, bulletOrigin.transform.position, this.transform.rotation);

		bulletlist.Add (bullet);
	
	}




	private class Bullet
	{
	
		public int Type0 = 0;

		private GameObject GO;

		float velocity;

		public void init (GameObject bullet, int type, Vector3 bulletOrigin, Quaternion rotation)
		{

			GO = Instantiate (bullet);
			/*
			switch (type) {

			case 0:
				velocity = 2;
				break;

			default:
				velocity = 2;
				break;

			}*/

			velocity = 4;

			GO.transform.rotation = rotation;

			GO.transform.position = bulletOrigin;

			GO.GetComponent<Rigidbody2D> ().velocity = velocity * GO.transform.forward;

			GO.SetActive (true);

		
		}
	
	
	}
}
