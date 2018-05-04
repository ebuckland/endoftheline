using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


	// constants
	public float PLAYER_FORCE = 2f;
	private Rigidbody2D RB;
	public GameObject bulletGO;
	public GameObject bulletOrigin;
	public float BULLET_DELAY = .1f;
	private float bulletDelayTime;
	private AudioSource gunSound;

	public GameObject HealthText;

	private int health = 6;
	public int DAMAGE_WAIT = 80;
	private int damageWait = 0;
	public int REGEN_TIME = 200;
	private int regenTime = 0;
	public int REGEN_WAIT = 60;
	private int regenWait = 0;




	// Use this for initialization
	void Start ()
	{

		RB = this.GetComponent<Rigidbody2D> ();

		bulletDelayTime = Time.time;

		gunSound = this.GetComponent<AudioSource> ();

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


		if (Input.GetMouseButton(0) && Time.time - bulletDelayTime > BULLET_DELAY) {
		
			gunSound.Play ();
			bulletDelayTime = Time.time;
			shoot ();

		
		}
	
	}


	void shoot ()
	{
		GameObject GO = Instantiate (bulletGO);
		/*
			switch (type) {

			case 0:
				velocity = 2;
				break;

			default:
				velocity = 2;
				break;

			}*/

		float velocity = 4.5f;

		GO.transform.rotation = bulletOrigin.transform.rotation;

		GO.transform.position = bulletOrigin.transform.position;

		GO.SetActive (true);

		GO.GetComponent<Rigidbody2D> ().velocity = velocity * GO.transform.up;

		Destroy (GO, 2f);
	
	}

	void OnCollisionEnter (Collider2D collider) {
	
		if (collider.gameObject.tag == "Enemy") {
			if (damageWait == 0) {
				health++;
				HealthText.GetComponent<Text> ().text = new string ("+", health);
			
			
			}
		}
	
	
	}
}
