using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;

    void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0 && GameObject.Find("ExitDoor").GetComponent<Finish>().finished)
        {
            StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadNextScene(index));
    }

    IEnumerator LoadNextScene(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
