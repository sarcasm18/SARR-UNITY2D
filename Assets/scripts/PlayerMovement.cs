using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    public float playerspeed = 2;
    public float jumpforce = 2;
    public bool isGrounded;
    public LayerMask groundLayerMask;
    public float raycastlength = 4;
    private Animator anim;
    private SpriteRenderer spriterenderer;
    public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        respawnPoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * playerspeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }
        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (horizontal < 0)
        {
            spriterenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriterenderer.flipX = false;
        }
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastlength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * raycastlength, Color.green);
        anim.SetBool("isGrounded", isGrounded);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "coin")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Respawn")
        {
            Respawn();
        }
        void Respawn()
        {
            transform.position = respawnPoint.position;
            Debug.Log("re");
        }

    }
   
}
                                                             