using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    float damage;
    float damageRate;
    public float pushBackF;

    float nextDamage;

    bool playerInRange = false;

    GameObject thePlayer;
    playerHealth thePlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        nextDamage = Time.time;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerHealth = thePlayer.GetComponent<playerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange) Attack();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
    void Attack()
    {
        if (nextDamage <= Time.time)
        {
            //thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;

            pushBack(thePlayer.transform);
        }
    }
    void pushBack(Transform pushedObject)
    {
        Vector3 pushDirection = new Vector3(0, (pushedObject.position.y - transform.position.y), 0).normalized;
        pushDirection *= pushBackF;

        Rigidbody pushedRB = pushedObject.GetComponent<Rigidbody>();
        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDirection, ForceMode.Impulse);
    }

}
