using UnityEngine;

public class MovebalePlatform : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    private void FixedUpdate()
    {
        if (transform.position.x > rightBound)
            rb.velocity = new Vector2 (-speed, 0);
        if (transform.position.x < leftBound)
            rb.velocity = new Vector2(speed, 0);   
    }
}
