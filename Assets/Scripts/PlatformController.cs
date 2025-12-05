using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float boundaryX = 8f;

    private CapacityManager capacityManager;

    void Start()
    {
        capacityManager = GetComponent<CapacityManager>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0f, 0f);
        transform.Translate(movement);

        float clampedX = Mathf.Clamp(transform.position.x, -boundaryX, boundaryX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        FallingObject fallingObject = other.GetComponent<FallingObject>();

        if (fallingObject != null && !fallingObject.IsCaught())
        {
            fallingObject.MarkAsCaught();

            if (capacityManager != null)
            {
                capacityManager.AddToCapacity(fallingObject.GetPlatformIncrease());
            }

            Destroy(other.gameObject);
        }
    }
}
