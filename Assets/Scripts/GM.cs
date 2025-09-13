using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GM : MonoBehaviour
{
    public Puck puck;
    public Transform leftPaddle, rightPaddle;
    public Vector2 leftStart = new Vector2(-5.5f, 0), rightStart = new Vector2(5.5f, 0);

    // Use TMP_Text instead of UnityEngine.UI.Text
    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;
    public TMP_Text centerText;

    public int maxScore = 7;

    int leftScore, rightScore;

    void Start()
    {
        ResetPositions();
        Serve(Random.value > 0.5f ? 1 : -1);
    }

    public void OnGoal(Goal.Side side)
    {
        if (side == Goal.Side.Left) rightScore++;
        else leftScore++;

        UpdateUI();

        if (leftScore >= maxScore || rightScore >= maxScore)
        {
            if (centerText) centerText.text = leftScore > rightScore ? "Left Wins!" : "Right Wins!";
            return;
        }

        ResetPositions();
        Serve(side == Goal.Side.Left ? -1 : 1);
    }

    void ResetPositions()
    {
        leftPaddle.position = leftStart;
        rightPaddle.position = rightStart;
        puck.transform.position = Vector2.zero;
    }

    void Serve(int dir)
    {
        if (centerText) centerText.text = "Serve!";
        puck.ResetAndServe(dir);
        Invoke(nameof(ClearCenterText), 0.5f);
    }

    void ClearCenterText() { if (centerText) centerText.text = ""; }

    void UpdateUI()
    {
        if (leftScoreText) leftScoreText.text = leftScore.ToString();
        if (rightScoreText) rightScoreText.text = rightScore.ToString();
    }

    public void Restart()
    {
        leftScore = rightScore = 0;
        UpdateUI();
        ResetPositions();
        Serve(Random.value > 0.5f ? 1 : -1);
        if (centerText) centerText.text = "";
    }
}

