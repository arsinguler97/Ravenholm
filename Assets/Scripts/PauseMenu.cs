using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource pauseMusic;

    private bool _isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(QuitToMainMenu);

        gameMusic.Play();
        pauseMusic.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;

        gameMusic.Play();
        pauseMusic.Stop();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;

        gameMusic.Pause();
        pauseMusic.Play();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}