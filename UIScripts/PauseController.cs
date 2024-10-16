using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if(!pauseMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape)) Pause();
        else if(Input.GetKeyDown(KeyCode.Escape)) Resume();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
