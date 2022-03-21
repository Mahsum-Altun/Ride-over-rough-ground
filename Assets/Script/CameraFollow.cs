using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // camera will follow this object
    public Transform Target;
    //camera transform
    public Transform camTransform;
    // offset between camera and target
    public Vector3 Offset;
    
    
     
    
    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;
 
    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;
 
    private void Start()
    {
        Offset.x = 0;
        Offset.y = -3.48f;
        Offset.z = 6.92f;
    }

    private void LateUpdate()
    {
        // update position
        Vector3 targetPosition = Target.transform.position + -1 * Offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        
 
        // update rotation
        transform.LookAt(Target);
    }
}
