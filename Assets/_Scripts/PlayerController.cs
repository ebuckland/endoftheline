using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	public int DAMAGE_WAIT;
	private int damageWait = 0;
	public int REGEN_TIME;
	private int regenTime = 0;
	public int REGEN_WAIT;
	private int regenWait = 0;
	public int GAME_OVER;
	private int gameOver = 0;
	private bool isGameOver = false;

	private AudioSource oofSound;




	// Use this for initialization
	void Start ()
	{

		AudioSource[] aSources = this.GetComponents<AudioSource> ();

		oofSound = aSources [0];
		gunSound = aSources [1];

		RB = this.GetComponent<Rigidbody2D> ();

		bulletDelayTime = Time.time;


	}
	
	// Update is called once per frame
	void Update ()
	{

		if (isGameOver) {

			Debug.Log (gameOver);
		
			gameOver++;
			if (gameOver > GAME_OVER) {
			
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
			
			}
		
		}


		Debug.Log ("regen wait: " + regenWait + "  regen time: " + regenTime);

		if (regenWait > REGEN_WAIT) {
			if (health < 6) {
				regenTime++;
			
				if (regenTime > REGEN_TIME) {
					health++;
					updateHealth ();

					regenTime = 0;
				
				}
			
			}
		} else {
			regenWait++;
		}

		if (damageWait < DAMAGE_WAIT) {
		
			damageWait++;
		
		}
			





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

	void OnCollisionStay2D (Collision2D coll) {
	
		if (coll.gameObject.tag == "Enemy") {
			if (damageWait == DAMAGE_WAIT) {
				regenWait = 0;
				regenTime = 0;
				damageWait = 0;

				if (health == 0) {
				
					isGameOver = true;
				

				} else {
					oofSound.Play ();
					health--;
					updateHealth ();
				}
			

			}
		}
	
	
	}


	void updateHealth() {

		HealthText.GetComponent<Text> ().text = new string ('+', health);
	
	
	}



}
