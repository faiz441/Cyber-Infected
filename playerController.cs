using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    //Movement Variable
    public float runSpeed;
    public float walkSpeed;
    bool running;

    Rigidbody avRB;
    Animator avAnim;

    bool fRight;

    //Jumping

    bool Grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;
    // Start is called before the first frame update
    void Start()
    {
        avRB = GetComponent<Rigidbody>();
        avAnim = GetComponent<Animator>();
        fRight = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadSceneAsync(0);
            Debug.Log("Player Kembali Ke Main Menu");
        }
    }
    void FixedUpdate()
    {

        running = false;

        if (Grounded && Input.GetAxis("Jump") > 0)
        {
            Grounded = false;
            avAnim.SetBool("Grounded", Grounded);
            avRB.AddForce (new Vector3(0,jumpHeight,0));
        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) Grounded = true;
        else Grounded = false;

        avAnim.SetBool("Grounded", Grounded);

        float move = Input.GetAxis("Horizontal");
        avAnim.SetFloat("Speed",Mathf.Abs(move));

        float sneaking = Input.GetAxisRaw("Fire3");
        avAnim.SetFloat("Sneak", sneaking);

        float firing = Input.GetAxis("Fire1");
        avAnim.SetFloat("Shooting", firing);

        if ((sneaking > 0 || firing > 0) && Grounded)
        {
           
            avRB.velocity = new Vector3(move * walkSpeed, avRB.velocity.y, 0);
        }
        else
        {
            avRB.velocity = new Vector3(move * runSpeed, avRB.velocity.y, 0);
            if(Mathf.Abs(move)>0) running = true;
        }


        if (move > 0 && !fRight) Flip();
        else if (move < 0 && fRight) Flip();
    }

    void Flip()
    {
        fRight = !fRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;   
    }

    public float GetFacing ()
    {
        if (fRight) return 1;
        else return -1;
    }


    public bool getRunning()
    {
        return (running);
    }

    
            

}