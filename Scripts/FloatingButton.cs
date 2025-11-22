using UnityEngine;

public class FloatingButton : MonoBehaviour
{
    [SerializeField] private Vector2 FirstPoint;
    [SerializeField] private Vector2 SecondPoint;
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool useCosine = false;
    [SerializeField] private bool reverseDirection = false;
    private float timeElapsed;
    private bool isUIElement;
    private RectTransform rectTransform;
    [SerializeField] private bool isPaused = false;

    void Start()
    {
        InitializeFloating();
    }

    void Update()
    {
        UpdateFloatingPosition();
    }

    private void InitializeFloating()
    {
        rectTransform = GetComponent<RectTransform>();
        isUIElement = rectTransform != null;
        timeElapsed = 0f;
    }

    private void UpdateFloatingPosition()
    {
        if (isPaused)
        {
            Debug.Log("Movement is paused due to isPaused = true");
            return;
        }

        timeElapsed += (reverseDirection ? -1 : 1) * Time.deltaTime * speed;
        Vector2 offset = CalculateFloatingVector();

        if (isUIElement)
        {
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = currentPosition + offset;
        }
        else
        {
            Vector2 currentPosition = transform.position;
            transform.position = currentPosition + offset;
        }
    }

    private Vector2 CalculateFloatingVector()
    {
        float t = useCosine ? Mathf.Cos(timeElapsed) : Mathf.Sin(timeElapsed);
        t = (t + 1) / 2;
        return Vector2.Lerp(FirstPoint, SecondPoint, t);
    }

    public void SetPaused(bool pause)
    {
        isPaused = pause;
        Debug.Log("Pause state changed to: " + isPaused);
    }
}

public enum Direction
{
    Right = 1,
    Left = -1,
    UP = 1,
    Down = -1
}
