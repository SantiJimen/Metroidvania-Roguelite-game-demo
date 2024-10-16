using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject loadMenu;
    [SerializeField] private GameObject volumeMenu;
    private SceneLoader sceneLoad;

    void Start()
    {
        mainMenu.SetActive(true);
        loadMenu.SetActive(false);
        volumeMenu.SetActive(false);
        sceneLoad = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteKey("dash");
        PlayerPrefs.DeleteKey("claws");
        PlayerPrefs.DeleteKey("boots");
        PlayerPrefs.DeleteKey("health");
        sceneLoad.LoadScene(1);
    }

    public void GoLoadMenu()
    {
        loadMenu.SetActive(true);
        mainMenu.SetActive(false);

        if(PlayerPrefs.HasKey("dash"))
            loadMenu.transform.GetChild(1).GetComponent<Button>().interactable = true;
        else
            loadMenu.transform.GetChild(1).GetComponent<Button>().interactable = false;
    }

    public void GoSettingsMenu()
    {
        volumeMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void GoMainMenu()
    {
        mainMenu.SetActive(true);
        volumeMenu.SetActive(false);
        loadMenu.SetActive(false);
    }

    public void LoadGame()
    {
        PlayerPrefs.DeleteKey("health");
        sceneLoad.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
