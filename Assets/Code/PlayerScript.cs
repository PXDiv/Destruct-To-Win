using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    GameMan _gameMan;
    MenuCodes _menuCodes;
    Vector3 LastCheckpoint;
    AudioSource audioManager;
    [SerializeField] AudioClip checkPointClip;
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip lostClip;
    [SerializeField] AudioClip destructClip;

    private void Start()
    {
        AudioSource[] audioSources;
        audioSources = FindObjectsOfType<AudioSource>();

        _gameMan = FindObjectOfType<GameMan>();
        _menuCodes = FindObjectOfType<MenuCodes>();

        foreach (AudioSource audio in audioSources)
        {
            if (audio.GetComponent<Player>() == null)
            {
                audioManager = audio;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            LeftControlPressed();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<MenuCodes>().RestartLevel();
        }
    }

    public void LeftControlPressed()
    {
        if (_gameMan.robotsRemaining > 0)
        {
            _gameMan.InstantiateDeadPrefab(transform.position.x, transform.position.y);
            transform.position = LastCheckpoint;
            audioManager.PlayOneShot(destructClip);
            _gameMan.robotsRemaining--;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            print("Checkpoint Set");
            LastCheckpoint = new Vector3(transform.position.x + 0.25f, transform.position.y, 0);
            //audioManager.PlayOneShot(checkPointClip);
        }

        if (other.CompareTag("Death"))
        {
            audioManager.PlayOneShot(lostClip);
            _menuCodes.LevelLost();
        }

        if (other.CompareTag("Win"))
        {
            audioManager.PlayOneShot(winClip);
            _menuCodes.LevelWon();
        }
    }
}