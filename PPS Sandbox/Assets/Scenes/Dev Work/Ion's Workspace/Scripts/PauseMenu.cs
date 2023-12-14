using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public bool GamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    MusicTransition musicTransition;
    AudioSource audioSource;
    public AudioClip[] audio;

    PlayerControls playerControls;

    // Update is called once per frame

    private void Start()
    {
        musicTransition = FindObjectOfType<MusicTransition>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    void Update()
    {
        playerControls.Actions.Pause.performed += x => HandlePause();
    }

    void HandlePause()
    {
        print("PAUSE");
        if (GamePaused)
        {
            Resume();
        }

        else
        {
            Pause();
            audioSource.clip = audio[0];
            audioSource.Play();
        }
    }

    public void Transition()
    {
        audioSource.clip = audio[1];
        audioSource.Play();
    }

    public void Resume ()
    {
        musicTransition.songStopped();
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause ()
    {
        musicTransition.songStopped();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResetScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(WaitThenReset(0.1f));
    }

    IEnumerator WaitThenReset(float time)
    {
        yield return new WaitForSeconds(time);
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
