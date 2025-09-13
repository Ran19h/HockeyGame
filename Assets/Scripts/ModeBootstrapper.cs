using UnityEngine;

public class ModeBootstrapper : MonoBehaviour
{
    public DualTouchManager2D dualTouch2P;
    public PaddleController playerLeft;
    public AIPaddle aiRight;

    public GameObject twoPHint;
    public GameObject cpuHint;

    void Start()
    {
        var twoPlayers = GameModeConfig.Selected == GameModeConfig.Mode.TwoPlayers;
        if (dualTouch2P) dualTouch2P.enabled = twoPlayers;
        if (playerLeft) playerLeft.enabled = !twoPlayers;
        if (aiRight) aiRight.enabled = !twoPlayers;
        if (twoPHint) twoPHint.SetActive(twoPlayers);
        if (cpuHint) cpuHint.SetActive(!twoPlayers);
    }
}
