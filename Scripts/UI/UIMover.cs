using UnityEngine;

public class UIMover : MonoBehaviour
{
    public enum Direction { Up, Down, Right, Left }

    [Header("General Settings")]
    [SerializeField] private RectTransform currentUI;
    [SerializeField] private RectTransform targetUI;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Direction moveDirection;

    private Canvas canvas;
    private Vector2 centerPosition;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Canvas not found in the parent object!");
        }

        centerPosition = Vector2.zero;
    }

    public void SwitchUI()
    {
        if (canvas == null || currentUI == null || targetUI == null)
        {
            Debug.LogError("Make sure all RectTransforms and Canvas are assigned in the inspector!");
            return;
        }

        float moveDistance = 1500f;

        Vector2 outPosition = currentUI.anchoredPosition;
        Vector2 inStartPosition = centerPosition;

        switch (moveDirection)
        {
            case Direction.Up:
                outPosition.y += moveDistance;
                inStartPosition.y -= moveDistance;
                break;
            case Direction.Down:
                outPosition.y -= moveDistance;
                inStartPosition.y += moveDistance;
                break;
            case Direction.Right:
                outPosition.x += moveDistance;
                inStartPosition.x -= moveDistance;
                break;
            case Direction.Left:
                outPosition.x -= moveDistance;
                inStartPosition.x += moveDistance;
                break;
        }

        targetUI.anchoredPosition = inStartPosition;

        LeanTween.move(currentUI, outPosition, moveSpeed)
            .setEase(LeanTweenType.easeInOutQuad);

        LeanTween.move(targetUI, centerPosition, moveSpeed)
            .setEase(LeanTweenType.easeInOutQuad);
    }
}
