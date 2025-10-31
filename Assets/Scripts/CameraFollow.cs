using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    
    [Header("Camera Position")]
    public Vector3 offset = new Vector3(0f, 5f, -8f);
    public float smoothSpeed = 10f;
    
    [Header("Camera Rotation")]
    public float lookAheadDistance = 2f;
    
    void LateUpdate()
    {
        if (target == null)
            return;
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        
        Vector3 lookAtPosition = target.position + Vector3.forward * lookAheadDistance;
        transform.LookAt(lookAtPosition);
    }
}
