using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;
    
    private int currentLane = 1;
    private Vector3 targetPosition;
    private int collectedKeys = 0;
    
    [Header("Input")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    
    void Start()
    {
        targetPosition = transform.position;
    }
    
    void Update()
    {
        HandleInput();
        MoveForward();
        MoveLanes();
    }
    
    void HandleInput()
    {
        if (Input.GetKeyDown(leftKey) && currentLane > 0)
        {
            currentLane--;
            UpdateTargetPosition();
        }
        else if (Input.GetKeyDown(rightKey) && currentLane < 2)
        {
            currentLane++;
            UpdateTargetPosition();
        }
    }
    
    void UpdateTargetPosition()
    {
        float xPosition = (currentLane - 1) * laneDistance;
        targetPosition = new Vector3(xPosition, transform.position.y, transform.position.z);
    }
    
    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }
    
    void MoveLanes()
    {
        float newX = Mathf.Lerp(transform.position.x, targetPosition.x, laneChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    
    public void CollectKey()
    {
        collectedKeys++;
        Debug.Log("Keys collected: " + collectedKeys);
    }
    
    public bool HasKey()
    {
        return collectedKeys > 0;
    }
    
    public void UseKey()
    {
        if (collectedKeys > 0)
        {
            collectedKeys--;
        }
    }
    
    public int GetKeyCount()
    {
        return collectedKeys;
    }
}
