using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform targetToFollow;
    public Vector3 worldOffset = new Vector3(0f, 1f, 0f);

    [Header("References")]
    public Camera mainCamera;
    public RectTransform canvasRectTransform;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (canvasRectTransform == null)
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                canvasRectTransform = canvas.GetComponent<RectTransform>();
            }
        }
    }

    void LateUpdate()
    {
        if (targetToFollow == null || mainCamera == null || canvasRectTransform == null)
        {
            return;
        }

        Vector3 worldPosition = targetToFollow.position + worldOffset;
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            screenPosition,
            canvasRectTransform.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera,
            out localPoint
        );

        rectTransform.localPosition = localPoint;
    }
}
