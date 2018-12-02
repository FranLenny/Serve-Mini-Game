using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{

    public float speed;
    public Text scoreText;
    public Text winText;
    public Text endText;
    private int count;
    private float timer;
    private bool facingRight = true;

    private Rigidbody2D rb2d;
    private AudioSource source;
    private Animator anim;
    private int score;
    public AudioClip clapClip;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score = 0;
        winText.text = "";
        endText.text = "";

        anim = GetComponent<Animator>();

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Customer_1"))
        {
            score = score + 3;
            source.PlayOneShot(clapClip);
        }

        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        if (facingRight == false && moveHorizontal < 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal > 0)
        {
            Flip();
        }
        timer = timer + Time.deltaTime;

        if (timer >= 10)
        {
            endText.text = "You Lose! :(";
         
        }
        else if (score >= 9)
        {
            winText.text = "You win 10 points";
        }
    }

}

        