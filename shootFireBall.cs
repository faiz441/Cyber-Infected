using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootFireBall : MonoBehaviour
{

    public float damage;
    public float speed;

    Rigidbody avRB;

    // Start is called before the first frame update
    void Start()
    {
        avRB = GetComponentInParent<Rigidbody>();

        if (transform.rotation.y > 0) avRB.AddForce(Vector3.right * speed, ForceMode.Impulse);
        else avRB.AddForce(Vector3.right * -speed, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            avRB.velocity = Vector3.zero;
            Destroy(gameObject);
        }
    }

}
