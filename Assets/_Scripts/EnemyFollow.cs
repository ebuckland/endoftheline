using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public int speed;
    private Transform target;


    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // if a bullet hits us?  1) destory bullet  then call Pool to 2) Reduce our enemy health and 3) possibly enemy dies if health <= 0
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
        }

        // GameObject.Find("HoldPool").GetComponent<EnemyPool>().hitMe(gameObject);


    }


}
