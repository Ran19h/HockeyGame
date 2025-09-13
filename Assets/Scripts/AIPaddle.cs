using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public Transform puck;
    public Rect playHalf = new Rect(0f, -4f, 7f, 8f);
    public float maxSpeed = 10f;   // top movement speed
    public float reaction = 8f;    // how aggressively we chase the target
    public float deadZone = 0.15f; // don't jitter if already close

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!puck) return;

        // Default: track puck position
        Vector3 target = puck.position;

        // If puck is far on the left side, retreat to a home guard position
        float homeX = playHalf.x + playHalf.width * 0.7f; // near the right goal
        if (puck.position.x < 0.15f)
            target = new Vector3(homeX, 0f, 0f);

        // Clamp target to our half
        target.x = Mathf.Clamp(target.x, playHalf.x + 0.6f, playHalf.x + playHalf.width - 0.6f);
        target.y = Mathf.Clamp(target.y, playHalf.y + 0.6f, playHalf.y + playHalf.height - 0.6f);

        // Move toward target with smoothing and caps
        Vector2 toTarget = (Vector2)(target - transform.position);
        if (toTarget.magnitude < deadZone) toTarget = Vector2.zero;

        Vector2 step = Vector2.ClampMagnitude(toTarget * reaction, maxSpeed);
        rb.MovePosition(rb.position + step * Time.fixedDeltaTime);
    }
}
