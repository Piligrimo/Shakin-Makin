using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    GameObject player;
    Collider2D col;
	// Use this for initialization
	void Start () {
        col = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (col.isTrigger && player.transform.position.y > transform.position.y)
            col.isTrigger = false;
        if (player.transform.position.y - transform.position.y > 10)
        {
            transform.position = player.transform.position + new Vector3(Random.value * 14 - 7,Random.value + 7 + player.transform.position.y / 100, 0);
            col.isTrigger = true;
            
        }
	}
}
