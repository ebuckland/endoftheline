﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public int speed;
    private Transform target;
	private Rigidbody2D RB;
	public float rotateSpeed = .3f;


    // Use this for initialization
    void Start()
    {
		RB = this.GetComponent<Rigidbody2D> ();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


		// rotate the zombie to the player
		Vector2 direction = target.position - this.transform.position;
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Lerp (transform.rotation, rotation, rotateSpeed);


    }

    // if a bullet hits us?  1) destory bullet  then call Pool to 2) Reduce our enemy health and 3) possibly enemy dies if health <= 0
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            GameObject.Find("HoldPool").GetComponent<EnemyPool>().hitMe(gameObject);

        }

    }


}