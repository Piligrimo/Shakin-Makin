using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}

    // Update is called once per frame
    void Update() {
        if (transform.localPosition.y == 0 || Input.anyKeyDown)
            Application.LoadLevel(1);
	}
}
