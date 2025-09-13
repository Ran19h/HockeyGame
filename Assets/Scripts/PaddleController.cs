using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public bool isLeft = true;
    public Rect playHalf = new Rect(-7f, -4f, 7f, 8f);
    public float maxSpeed = 20f;
    public float followTightness = 30f;

    Rigidbody2D rb;
    Vector3 target;

    void Awake() { rb = GetComponent<Rigidbody2D>(); target = transform.position; }

    void Update()
    {
        Vector3 wp;
#if UNITY_EDITOR || UNITY_STANDALONE
        wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else
        if (Input.touchCount == 0) return;
        wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        wp.z = 0;
        if (IsInsideHalf(wp)) target = ClampToHalf(wp);
        Vector3 next = Vector3.MoveTowards(transform.position, target, maxSpeed * Time.deltaTime);
        rb.MovePosition(Vector2.Lerp(transform.position, next, followTightness * Time.deltaTime));
    }

    bool IsInsideHalf(Vector3 p) => isLeft ? p.x < 0f : p.x > 0f;

    Vector3 ClampToHalf(Vector3 p)
    {
        var r = playHalf;
        if (!isLeft) r.x = 0f;
        float x = Mathf.Clamp(p.x, r.x + 0.6f, r.x + r.width - 0.6f);
        float y = Mathf.Clamp(p.y, r.y + 0.6f, r.y + r.height - 0.6f);
        return new Vector3(x, y, 0);
    }
}