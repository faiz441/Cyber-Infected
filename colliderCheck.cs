using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class colliderCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
           
            Debug.Log(message: $"Player bersentuhan dengan environment", gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
