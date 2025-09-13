using UnityEngine;


public class Goal : MonoBehaviour
{
    public enum Side { Left, Right }
    public Side side;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Puck"))
            FindObjectOfType<GM>()?.OnGoal(side);
    }
}