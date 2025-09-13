using UnityEngine;

public class DualTouchManager2D : MonoBehaviour
{
    [Header("Refs")]
    public Rigidbody2D leftPaddle;
    public Rigidbody2D rightPaddle;

    [Header("World Bounds (per half)")]
    public Rect leftHalf = new Rect(-7f, -4f, 7f, 8f);
    public Rect rightHalf = new Rect(0f, -4f, 7f, 8f);

    [Header("Tuning")]
    public float maxSpeed = 22f;
    public float followTightness = 35f;
    public float paddleRadius = 0.6f;

    int leftFingerId = -1;
    int rightFingerId = -1;

    Vector2 leftTarget;
    Vector2 rightTarget;

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
        if (leftPaddle) leftTarget = leftPaddle.position;
        if (rightPaddle) rightTarget = rightPaddle.position;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseFallback();
#endif
        HandleTouches();
    }

    void FixedUpdate()
    {
        MovePaddle(leftPaddle, leftTarget);
        MovePaddle(rightPaddle, rightTarget);
    }

    void HandleTouches()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            var t = Input.GetTouch(i);
            Vector3 wp = cam.ScreenToWorldPoint(t.position); wp.z = 0;

            bool onLeftSide = t.position.x < (Screen.width * 0.5f);
            bool onRightSide = !onLeftSide;

            if (t.phase == TouchPhase.Began)
            {
                if (onLeftSide && leftFingerId == -1) leftFingerId = t.fingerId;
                else if (onRightSide && rightFingerId == -1) rightFingerId = t.fingerId;
            }

            if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                if (t.fingerId == leftFingerId)
                    leftTarget = ClampToRect(wp, leftHalf, paddleRadius);
                if (t.fingerId == rightFingerId)
                    rightTarget = ClampToRect(wp, rightHalf, paddleRadius);
            }

            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                if (t.fingerId == leftFingerId) leftFingerId = -1;
                if (t.fingerId == rightFingerId) rightFingerId = -1;
            }
        }
    }

    void HandleMouseFallback()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition); wp.z = 0;
            if (Input.mousePosition.x < Screen.width * 0.5f)
                leftTarget = ClampToRect(wp, leftHalf, paddleRadius);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition); wp.z = 0;
            if (Input.mousePosition.x >= Screen.width * 0.5f)
                rightTarget = ClampToRect(wp, rightHalf, paddleRadius);
        }
    }

    void MovePaddle(Rigidbody2D rb, Vector2 target)
    {
        if (!rb) return;
        Vector2 pos = rb.position;
        Vector2 next = Vector2.MoveTowards(pos, target, maxSpeed * Time.fixedDeltaTime);
        Vector2 blended = Vector2.Lerp(pos, next, followTightness * Time.fixedDeltaTime);
        Vector2 delta = blended - pos;
        if (delta.magnitude / Time.fixedDeltaTime > maxSpeed)
            delta = delta.normalized * maxSpeed * Time.fixedDeltaTime;
        rb.MovePosition(pos + delta);
    }

    Vector2 ClampToRect(Vector3 p, Rect r, float margin)
    {
        float x = Mathf.Clamp(p.x, r.x + margin, r.x + r.width - margin);
        float y = Mathf.Clamp(p.y, r.y + margin, r.y + r.height - margin);
        return new Vector2(x, y);
    }
}