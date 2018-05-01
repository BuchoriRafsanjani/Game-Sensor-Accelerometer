using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallControlBaruClean : MonoBehaviour {

    Rigidbody2D rb;

    // Range digunakan untuk batasan maksimal speednya
    [Range(0.2f, 2f)]
    public float moveSpeedModifier = 0.5f;

    float dirX, dirY;

    Animator anim;

    static bool isDead;

    static bool moveAllowed;

    // Bisa ditambahkan game Object Textnya walaupun tidak public karena bersifat SerializeField
    [SerializeField]
    GameObject winText;
    [SerializeField]
    GameObject loseText;

    // Use this for initialization
    void Start () {

        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

        moveAllowed = true;


        isDead = false;


        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();


        anim.SetBool("BallDead", isDead);

    }

    // Update is called once per frame
    void Update()
    {

        // Script Acceleromenter
        dirX = Input.acceleration.x * moveSpeedModifier;
        dirY = Input.acceleration.y * moveSpeedModifier;
    }

    void FixedUpdate()
    {
        // Setting a velocity to Rigidbody2D component according to accelerometer data
        if (moveAllowed)
            rb.velocity = new Vector2(rb.velocity.x + dirX, rb.velocity.y + dirY);
    }


    void YouWin()
    {
        winText.gameObject.SetActive(true);
        loseText.gameObject.SetActive(false);

        // ball tidak berpindah
        moveAllowed = false;

        // animasi ke Ball Dead
        anim.SetBool("BallDead", true);

        // Restart Scene
        Invoke("RestartScene", 4f);
    }

    void YouLose()
    {
        // posisi kalah
        loseText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);

        // Ketika bola stop atau posisi berhenti
        rb.velocity = new Vector2(0, 0);

        // Set animasi BallDead
        anim.SetBool("BallDead", isDead);

        // ball tidak berpindah
        moveAllowed = false;

        // animasi ke Ball Dead
        anim.SetBool("BallDead", true);

        // Restart Scene
        Invoke("RestartScene", 4f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DeathHoles")
        {
            YouLose();
        }
        else if (collision.gameObject.tag == "ExitHole")
        {
            YouWin();
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
