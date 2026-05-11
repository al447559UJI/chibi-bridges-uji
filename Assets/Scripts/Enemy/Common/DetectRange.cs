using UnityEngine;

public class DetectRange : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private IDetectRangeUser user;
    public Vector2 lastTargetPosition { get; private set; }


    void Awake()
    {
        user = GetComponentInParent<IDetectRangeUser>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsTarget(collision, targetTag))
        {
            user?.TargetFound();
        }

        Vector2 collisionPos = collision.transform.position;

        if (collisionPos != lastTargetPosition)
        {
            lastTargetPosition = collisionPos;
        }

    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTarget(collision, targetTag))
        {
            user?.TargetLost();

            // Vector2 collisionPos = collision.transform.position;
            // lastTargetPosition = new Vector2(collisionPos.x + offset.x, collisionPos.y + offset.y);
        }
    }

    private bool IsTarget(Collider2D collision, string targetTag)
    {
        return collision.gameObject.CompareTag(targetTag);
    }
}
