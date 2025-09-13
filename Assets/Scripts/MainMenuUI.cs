using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartTwoPlayers()
    {
        GameModeConfig.Selected = GameModeConfig.Mode.TwoPlayers;
        SceneManager.LoadScene("Game");
    }

    public void StartVsCPU()
    {
        GameModeConfig.Selected = GameModeConfig.Mode.VsCPU;
        SceneManager.LoadScene("Game");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
