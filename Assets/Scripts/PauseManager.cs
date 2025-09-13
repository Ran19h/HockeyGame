using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool muteAudioWhilePaused = true;
    public AudioListener audioListener;

    bool isPaused;

    void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);
        if (audioListener == null) audioListener = FindObjectOfType<AudioListener>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            TogglePause();
    }

    public void TogglePause() { if (isPaused) Resume(); else Pause(); }

    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;
        Time.timeScale = 0f;
        Physics2D.simulationMode = SimulationMode2D.Script;
        if (pausePanel) pausePanel.SetActive(true);
        if (muteAudioWhilePaused && audioListener) audioListener.enabled = false;
        Cursor.visible = true; Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = 1f;
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        if (pausePanel) pausePanel.SetActive(false);
        if (muteAudioWhilePaused && audioListener) audioListener.enabled = true;
    }

    public void OnResumeButton() => Resume();
    public void OnRestartButton() { Resume(); SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void OnMainMenuButton() { Resume(); SceneManager.LoadScene("MainMenu"); }
    public void OnQuitButton() { Resume(); Application.Quit(); }
}
