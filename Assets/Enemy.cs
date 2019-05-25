using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    Vector3 startPoint;
    GameObject player;
    Rigidbody2D rb;
    AudioSource au;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        startPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-8, 0);
        au = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        if (startPoint.x - transform.position.x > 5 && rb.velocity.x < 0 || startPoint.x - transform.position.x < -5 && rb.velocity.x > 0)
            rb.velocity = -rb.velocity;
        if (player.transform.position.y - transform.position.y > 10)
        {
            transform.position = player.transform.position + new Vector3(Random.value * 6 - 3, 50 + player.transform.position.y / 100, 0);
            startPoint = transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.transform.position.y < transform.position.y)
            {
                player.GetComponent<Collider2D>().enabled = false;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                au.Play();
            }
            else
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20);
                transform.position = player.transform.position + new Vector3(Random.value * 6 - 3, 50 + player.transform.position.y / 100, 0);
                player.GetComponent<Makin>().XtraScore += 200 + Mathf.RoundToInt(Random.value * 100);
                startPoint = transform.position;
            }
        }
        if (collision.gameObject.tag=="Bullet")
        {
            transform.position = player.transform.position + new Vector3(Random.value * 6 - 3, 50 + player.transform.position.y / 100, 0);
            player.GetComponent<Makin>().XtraScore += 200 + Mathf.RoundToInt(Random.value * 100);
            startPoint = transform.position;
        }
    }
}
