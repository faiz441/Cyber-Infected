using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchScript : MonoBehaviour
{

    public float damage;
    public float knockBack;
    public float knockBackRadius;
    public float meleeRate;


    float nexMelee;

    int shootableMask;

    Animator avAnim;
    playerController myPC;


    // Start is called before the first frame update
    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        avAnim = transform.root.GetComponent<Animator>();
        myPC = transform.root.GetComponent<playerController>();
        nexMelee = 0f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float melee = Input.GetAxis("Fire2");
        if (melee > 0 && nexMelee < Time.time && !(myPC.getRunning())) 
        {
            avAnim.SetTrigger("Punch");
            nexMelee = Time.time + meleeRate;

            //damage

            Collider[] attacked = Physics.OverlapSphere(transform.position, knockBackRadius, shootableMask);

        
        }
    }
}
