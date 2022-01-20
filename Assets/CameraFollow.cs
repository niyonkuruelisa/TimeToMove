using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    private void FixedUpdate()
    {

        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        //Verify if the targetPosition is out of bound or not
 

        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

}

