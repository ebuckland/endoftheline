using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


	// constants
	public float PLAYER_FORCE = 2f;
	private Rigidbody2D RB;
	private GameObject bulletGO;


	// lists




	// Use this for initialization
	void Start ()
	{

		RB = this.GetComponent<Rigidbody2D> ();

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
	
	
	
	
	
	
	}




	private class bullet
	{
	
		public int Type0 = 0;

		private GameObject GO;

		float velocity;

		public void init (GameObject gameObject, int type)
		{

			GO = Instantiate (gameObject);

			switch (type) {

			case 0:
				velocity = 2;
				break;

			default:
				velocity = 2;
				break;

			}





		
		
		}



	
	
	
	
	
	}
}
