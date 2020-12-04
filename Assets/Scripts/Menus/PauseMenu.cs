using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    public GameObject pauseMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
        {
            ShowMenu();
        }
        else
        {
            HideMenu();
        }


    }

    private void ShowMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    private void HideMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

}
