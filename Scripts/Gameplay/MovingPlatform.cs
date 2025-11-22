using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 2f;
    public bool canFall = false;

    private Vector3 targetPosition;
    private bool movingToEnd = true;

    void Start()
    {
        transform.position = startPoint.position;
        targetPosition = endPoint.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            if (movingToEnd)
            {
                targetPosition = startPoint.position;
            }
            else
            {
                targetPosition = endPoint.position;
            }
            movingToEnd = !movingToEnd;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);

            if (canFall)
            {
                StartCoroutine(CheckFall(collision.transform));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private System.Collections.IEnumerator CheckFall(Transform player)
    {
        while (true)
        {
            if (!IsPlayerOnPlatform(player))
            {
                player.SetParent(null);
                yield break;
            }
            yield return null;
        }
    }

    private bool IsPlayerOnPlatform(Transform player)
    {
        float platformHeight = GetComponent<Collider2D>().bounds.size.y;
        float playerBottom = player.position.y - player.GetComponent<Collider2D>().bounds.extents.y;
        float platformTop = transform.position.y + platformHeight / 2;

        return playerBottom <= platformTop;
    }
}
