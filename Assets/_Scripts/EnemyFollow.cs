using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public int speed;
    private Transform target;
	public float rotateSpeed = .3f;


    // Use this for initialization
    void Start()
    {
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


	public void CollateralDMG() {
	
		GameObject.Find("HoldPool").GetComponent<EnemyPool>().hitMe(gameObject, false);
	
	}

    // if a bullet hits us?  1) destory bullet  then call Pool to 2) Reduce our enemy health and 3) possibly enemy dies if health <= 0
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            GameObject.Find("HoldPool").GetComponent<EnemyPool>().hitMe(gameObject);
			int counter = 0;
			foreach (Collider2D CO in Physics2D.OverlapCircleAll(this.transform.position, .6f)) {
			
				if (CO.gameObject.tag == "Enemy" && CO.gameObject != this.gameObject) {
					Debug.DrawLine (this.transform.position, CO.gameObject.transform.position);
					counter++;
					CO.gameObject.GetComponent<EnemyFollow> ().CollateralDMG();
				
				}
			
			}

			//Debug.Log (counter + " Enemies collateral DMG");

        }

    }


}