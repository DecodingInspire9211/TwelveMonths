using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    AsyncOperation loadingScene;
    public Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        progressBar = GameObject.Find("progressLoader").GetComponent<Slider>();
        if(progressBar)
        {
            Debug.Log(progressBar.name);
        }   
        else
        {
            Debug.Log("No object found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(loadingScene == null)
            progressBar.value = Mathf.Clamp01(0f / 0.9f);

        if(loadingScene != null)
            progressBar.value = Mathf.Clamp01(loadingScene.progress / 0.9f);
    }

    public void NewGame()
    {
        try
        {
            loadingScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void LoadGame()
    {
        Debug.Log("Load Game");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
