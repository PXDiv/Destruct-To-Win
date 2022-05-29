using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMan : MonoBehaviour
{
    public bool toShowLevelNumber;
    public int robotsRemaining;
    public GameObject PlayerPrefab;
    public GameObject PlayerDeadPrefab;
    public TMP_Text robotsRemainingText;
    public TMP_Text timeText;
    public bool useSound;
    [SerializeField] GameObject touchScreenTutorial;

    private void Start()
    {
        //Setting Everything
        robotsRemainingText = GameObject.Find("Robots Remaining").GetComponent<TMP_Text>();
        timeText = GameObject.Find("Time").GetComponent<TMP_Text>();

        //feature Removed for speed runners
        toShowLevelNumber = true;
        if (toShowLevelNumber)
        {
            timeText.text = ("Level " + SceneManager.GetActiveScene().buildIndex).ToString();
        }

        //Setting Sound
        useSound = (PlayerPrefs.GetInt("useSound", 1) != 0);

        if (!useSound)
        { AudioListener.pause = true; }
        else { AudioListener.pause = false; }

        //Tutorial On The First Level

        if (PlayerPrefs.GetInt("useTouch", 0) == 1 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            print("enabling TouchScreenTutorial");
            touchScreenTutorial.SetActive(true);
            GameObject.Find("keyboardTutorial").SetActive(false);
        }
        else { print("using tutorial for keyboard"); }
    }

    private void Update()
    {
        robotsRemainingText.text = robotsRemaining.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<MenuCodes>().Pause();
        }
    }

    public void InstantiatePlayer()
    {
        Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
    }

    public void InstantiateDeadPrefab(float xVal, float yVal)
    {
        Vector3 positionTransform = new Vector3(xVal, yVal);
        Instantiate(PlayerDeadPrefab, positionTransform, Quaternion.identity);
    }
}
