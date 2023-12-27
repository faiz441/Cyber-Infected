using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionController : MonoBehaviour
{
    public Light explosionlight;
    public float power;
    public float radius;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius); 
        foreach (Collider hit in colliders) {
           Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null) rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Impulse);
            if (hit.tag == "Player")
            {
                playerHealth thePlayerHealth = hit.gameObject.GetComponent<playerHealth>();
                thePlayerHealth.addDamage(damage);

            }
            else if(hit.tag == "Enemy")
            {
                //enemyHealth theEnemyHealth = hit.gameObject.GetComponent<enemyHealth>();
                //theEnemyHealth.addDamage(damage);
            }
            

        }

    }

    // Update is called once per frame
    void Update()
    {

        //explosionlight.intensity = Mathf.Lerp(explosionlight.intensity, 0f, 5 * Time.time);
       

    }
}
