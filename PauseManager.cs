using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseUI;
    private bool isPaused = false;

    void Start()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseUI.SetActive(true);

        Time.timeScale = 0f; // berhenti
        isPaused = true;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);

        Time.timeScale = 1f; // lanjut
        isPaused = false;
    }

    public void SaveGame()
    {
        FindObjectOfType<Save>().SavePosition();
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}