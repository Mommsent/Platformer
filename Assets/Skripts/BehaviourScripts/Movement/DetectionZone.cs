using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    //public UnityEvent noCollidersRemain;

    public bool IsDetected;
    public Vector2 collisionPos = Vector2.zero;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collisionPos = collision.transform.position;
            IsDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsDetected = false;
    }
}
