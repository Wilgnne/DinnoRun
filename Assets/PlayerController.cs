using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    Rigidbody2D rb;
    public float playerX;
    public float velocity, jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJompMultiplier = 2f;



    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = this;
        else
            Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (GraundController.controllers != null)
            {
                foreach (GraundController controller in GraundController.controllers)
                {
                    controller.Move((int)playerX);
                }
                yield return new WaitForEndOfFrame();
            }
            else
                yield return new WaitForEndOfFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerX += velocity * Time.deltaTime;

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cactu")
        {
            if(collision.gameObject.GetComponent<SpriteRenderer>().sprite != null)
                Time.timeScale = 0;
        }
    }
}
