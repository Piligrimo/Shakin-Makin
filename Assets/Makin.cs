using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Makin : MonoBehaviour {
    Rigidbody2D rb;
    Animator an;
    AudioSource au;
    public Text ScoreUI;
    public GameObject Res,bullet,bonus;
    int score,highscore,bulletCount;
    public int XtraScore;
    public AudioClip shotSound, jumpSound,takeSound;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
    //    PlayerPrefs.SetInt("high", 0);
        an = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
        bonus = GameObject.FindGameObjectWithTag("Bonus");
        highscore = PlayerPrefs.GetInt("high", 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (score > highscore)
            highscore = score;
        if (transform.position.y * 10 + XtraScore > score)
            score = Mathf.RoundToInt(transform.position.y * 10)+XtraScore;
        ScoreUI.text = "Счет:" + score.ToString()+"\nРекорд:"+highscore.ToString()+"\nСнаряды:"+bulletCount.ToString();
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*5, rb.velocity.y);
        if ((score - XtraScore) / 10f - transform.position.y > 20)
        {
            Res.SetActive(true);
            Cursor.visible = true;
            PlayerPrefs.SetInt("high", highscore);
        }
        if (transform.position.y-bonus.transform.position.y>10)
            bonus.transform.position = transform.position + new Vector3(Random.value * 10-5, 25 + transform.position.y / 100, 0);
        if (Input.GetButtonDown("Jump")&&bulletCount>0)
        {
            GameObject b;
            b = Instantiate(bullet, transform.position + transform.up, transform.rotation) as GameObject;
            b.GetComponent<Rigidbody2D>().velocity =new Vector2 (0,30);
            au.clip = shotSound;
            au.Play();
            an.SetTrigger("shoot");
            bulletCount--;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!Res.active)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                Res.SetActive(true);
            }
            else
            {
                if (Time.timeScale == 0)
                {
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    Res.SetActive(false);
                }
            }
	}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = new Vector2(rb.velocity.x, 12+transform.position.y/100);
        au.clip = jumpSound;
        au.Play();
        an.SetTrigger("jmp");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bonus")
        {
            bulletCount++;
            collision.gameObject.transform.position = transform.position + new Vector3(Random.value * 10 - 5,25  + transform.position.y / 100, 0);
            au.clip = takeSound;
            au.Play();
        }

    }
    public void Restart()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("high", highscore);
        Application.LoadLevel(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
