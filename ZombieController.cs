using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{


    public GameObject flipModel;

    //audio option
    public AudioClip[] idleSound;
    public float idleSoundTime;
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;


    public float detectionTime;
    float startRun;
    bool firstDetection;

    //movement option
    public float runSpeed;
    public float walkSpeed;
    public bool facingRight = true;


    float moveSpeed;
    bool running;

    Rigidbody avRB;
    Animator avAnim;
    Transform detectedPlayer;

    bool Detected;


    // Start is called before the first frame update
    void Start()
    {
        avRB = GetComponentInParent<Rigidbody>();
        avAnim = GetComponentInParent<Animator>();
        enemyMovementAS = GetComponent<AudioSource>();

        running = false;
        Detected = false;
        firstDetection = false;
        moveSpeed = walkSpeed;

        if (Random.Range(0, 10) > 5) Flip();

    }


    void FixedUpdate()
    {
        if (Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
            if (!firstDetection)
            {
                startRun = Time.time + detectionTime;
                firstDetection = true;
            }
        }
        if (Detected && !facingRight) avRB.velocity = new Vector3((moveSpeed * -1), avRB.velocity.y, 0);
        else if (Detected && facingRight) avRB.velocity = new Vector3(moveSpeed, avRB.velocity.y, 0);

        if (!running && Detected)
        {
            if (startRun < Time.time)
            {
                moveSpeed = runSpeed;
                avAnim.SetTrigger("run");
                running = true;

            }
        }

        //idle or walkin sound
        if (!running)
        {
            if (Random.Range(0, 10) > 5 && nextIdleSound < Time.time)
            {
                AudioClip tempClip = idleSound[Random.Range(0, idleSound.Length)];
                enemyMovementAS.clip = tempClip;
                enemyMovementAS.Play();
                nextIdleSound = idleSoundTime + Time.time;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            avAnim.SetBool("detected", Detected);
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            firstDetection = false;
            if (running)
            {
                avAnim.SetTrigger("run");
                moveSpeed = walkSpeed;
                running = false;
            }
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= 1;
        flipModel.transform.localScale = theScale;


    }


}
