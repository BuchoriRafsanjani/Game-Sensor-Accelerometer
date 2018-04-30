using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControlScript : MonoBehaviour {

	
	Rigidbody2D rb;

	// Range digunakan untuk batasan maksimal speednya
	[Range(0.2f, 2f)]
	public float moveSpeedModifier = 0.5f;


	float dirX, dirY;


	Animator anim;


	static bool isDead;


	static bool moveAllowed;


	static bool youWin;
    static bool youLose;

	// Bisa ditambahkan game Object Textnya walaupun tidak public karena bersifat SerializeField
	[SerializeField]
	GameObject winText;
    [SerializeField]
    GameObject loseText;

	// Use this for initialization
	void Start () {


		winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

		youWin = false;
        youLose = false;


		moveAllowed = true;

	
		isDead = false;


		rb = GetComponent<Rigidbody2D> ();

		anim = GetComponent<Animator> ();

	
		anim.SetBool ("BallDead", isDead);
	}
	
	// Update is called once per frame
	void Update () {

		// Script Acceleromenter
		dirX = Input.acceleration.x * moveSpeedModifier;
		dirY = Input.acceleration.y * moveSpeedModifier;

		// if isDead is true
		if (isDead) {

			// Ketika bola stop atau posisi berhenti
			rb.velocity = new Vector2 (0, 0);

			// Set animasi BallDead
			anim.SetBool ("BallDead", isDead);

			// Menuju void RestartScene dengan menunggu selama 4f
			Invoke ("RestartScene", 4f);
		}

		// If you win
		if (youWin) {

			// posisi menang
			winText.gameObject.SetActive (true);
            loseText.gameObject.SetActive(false);

			// ball tidak berpindah
			moveAllowed = false;

            // animasi ke Ball Dead
            anim.SetBool("BallDead", true);

            // Restart Scene
            Invoke("RestartScene", 4f);
		}

        if (youLose)
        {

            // posisi kalah
            loseText.gameObject.SetActive(true);
            winText.gameObject.SetActive(false);

            // ball tidak berpindah
            moveAllowed = false;

            // animasi ke Ball Dead
            anim.SetBool("BallDead", true);

            // Restart Scene
            Invoke("RestartScene", 4f);
        }

    }

	void FixedUpdate()
	{
		// Setting a velocity to Rigidbody2D component according to accelerometer data
		if (moveAllowed)
		rb.velocity = new Vector2 (rb.velocity.x + dirX, rb.velocity.y + dirY);
	}

	// Kondisi Kalah
	public static void setIsDeadTrue()
	{
		// Setting isDead to true
		isDead = true;
        youLose = true;
	}

	// Kondisi Menang
	public static void setYouWinToTrue()
	{
		youWin = true;
        youLose = false;
	}

    public void BoostBall()
    {
        dirX = Input.acceleration.x * moveSpeedModifier * 1.5f;
        dirY = Input.acceleration.y * moveSpeedModifier * 1.5f;
    }

	// Menuju RestartScene yaitu Main Menu
	void RestartScene()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
