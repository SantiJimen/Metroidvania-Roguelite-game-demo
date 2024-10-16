using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Finish : MonoBehaviour
{
    public TextMeshPro text;
    public bool finished;

    void Start()
    {
        text.text = "";
        finished = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            text.text = "[E]";
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            text.text = "";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        { 
            GameObject.Find("DataSave").GetComponent<PlayerSave>().SavePlayer();
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            if(PlayerPrefs.HasKey("health"))PlayerPrefs.DeleteKey("health");
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene(0);
        }
        else
            finished = true;
    }
}
