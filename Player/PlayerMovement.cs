using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private bool facingRight = true;
    private float jumpingPower = 16;
    public bool wallClimbing = false;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingSpeed = 14f;
    private float dashingTime = 0.3f;
    private float dashingCoolDown = 1f;
    
    public bool dashActive = false;
    public bool bootsOn = false;
    public bool climbClaws = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private AudioSource jumpSound; 
    public Animator anim;

    void Start()
    {
        GameObject.Find("DataSave").GetComponent<PlayerSave>().LoadPlayer();
    }
    

    // Update is called once per frame
    void Update()
    {

        if(isDashing) return;
        else if(GameObject.Find("ExitDoor").GetComponent<Finish>().finished)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetTrigger("Jump");
        }
        else if(facingRight && horizontal < -0.1f || !facingRight && horizontal > 0.1f)
        {
            Flip();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashActive && canDash && horizontal != 0)
        {
            anim.SetTrigger("Dash");
            StartCoroutine(Dash());
        } 

        
    }

    private void FixedUpdate()
    {
        if(isDashing) return;

        if(transform.GetComponent<HealthController>().KBCounter <= 0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }  
        anim.SetFloat("AirSpeed", rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        anim.SetBool("Grounded", isGrounded());
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag.Equals("ClimbWall") && climbClaws) wallClimbing = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag.Equals("ClimbWall")) wallClimbing = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 10, true);
        rb.velocity = new Vector2(horizontal * dashingSpeed, 0);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 10, false);
        tr.emitting = false;
        isDashing = false;
        rb.gravityScale = originalGravity;

        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
}
