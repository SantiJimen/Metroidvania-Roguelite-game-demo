using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1f;
        GameObject.Find("DataSave").GetComponent<PlayerSave>().SavePlayer();
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene(0);
    }
}
