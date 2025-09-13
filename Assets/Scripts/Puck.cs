using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Puck : MonoBehaviour
{
    public float serveSpeed = 10f;
    public float maxSpeed = 18f;
    public Vector2 center = Vector2.zero;


    Rigidbody2D rb;


    void Awake() { rb = GetComponent<Rigidbody2D>(); }


    public void ResetAndServe(int dir) // dir: -1 left, +1 right
    {
        rb.position = center;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(new Vector2(dir, Random.Range(-0.3f, 0.3f)).normalized * serveSpeed, ForceMode2D.Impulse);
    }


    void FixedUpdate()
    {
        if (rb.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }
}