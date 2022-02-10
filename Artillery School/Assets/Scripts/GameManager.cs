using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject shopUi;
    [SerializeField] AudioSource bgMusic;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject helpMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] TextMeshProUGUI timerText;
    public float timer = 120f;
    public bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        timer = 120;
        isGameActive = false;
        timerText.SetText("Time: " + Mathf.Round(timer) + "s");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            timer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.P))
            {
                if(Time.timeScale > 0)
                {
                    Time.timeScale = 0;
                    shopUi.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;
                    shopUi.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                helpMenu.SetActive(!helpMenu.activeSelf);
            }

            CheckTimer();
        }        
    }

    public void StartGame()
    {
        menu.SetActive(false);
        isGameActive = true;
    }

    void CheckTimer()
    {
        timerText.SetText("Time: " + Mathf.Round(timer) + "s");

        if(timer <= 0)
        {
            isGameActive = false;
            gameOverMenu.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
