using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public Vector3 targetPosition;
    private Vector3 correctPosition;
    private SpriteRenderer sPrite;
    public int number;
    public bool inRightPlace;

    // Start is called before the first frame update
    void Awake()
    {
        targetPosition = transform.position;
        correctPosition = transform.position;
        sPrite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
        if (targetPosition == correctPosition)
        {
            sPrite.color = Color.green;
            inRightPlace = true;
        }
        else
        {
            sPrite.color = Color.white;
            inRightPlace = false;
        }
    }
}
