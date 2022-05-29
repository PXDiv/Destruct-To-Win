using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCodes : MonoBehaviour
{
    public GameObject winPanel, lostPanel, PausePanel;
    [SerializeField] Toggle soundToggle, touchToggle;
    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("useTouch", 0);
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            player = FindObjectOfType<Player>();
        }

        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PausePanel = transform.Find("PausePanel").gameObject;
            lostPanel = transform.Find("LostPanel").gameObject;
            winPanel = transform.Find("WinPanel").gameObject;
        }
        else
        {
            soundToggle.isOn = PlayerPrefs.GetInt("useSound", 1) != 0;
            touchToggle.isOn = PlayerPrefs.GetInt("useTouch", 0) != 0;
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        { print("soundToggle: " + soundToggle.isOn + " touchToggle: " + touchToggle.isOn); }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    public void LevelWon()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void LevelLost()
    {
        Time.timeScale = 0f;
        lostPanel.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
    public void LoadLevelbyNumber(int LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void setTouchInput(bool setTo)
    {
        PlayerPrefs.SetInt("useTouch", (setTo ? 1 : 0));
    }

    public void setSoundSetting(bool setTo)
    {
        PlayerPrefs.SetInt("useSound", (setTo ? 1 : 0));
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayerMoveDir(int moveDir)
    {
        player.ChangeMoveDirection(moveDir);
    }

    public void PlayerJump()
    {
        player.Jump();
    }
    public void PlayerDestruct()
    {
        FindObjectOfType<PlayerScript>().LeftControlPressed();
    }

    public void QuitGame()
    {
        print("Game Quit");
        Application.Quit();
    }
}
