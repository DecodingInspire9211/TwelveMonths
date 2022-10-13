using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Preloader : MonoBehaviour
{
    AsyncOperation loadingScene;
    Slider progressBar;
    TMP_Text progressText;

    // Start is called before the first frame update
    void Start()
    {   
        progressBar = GameObject.Find("progressLoader").GetComponent<Slider>();
        if(progressBar)
            Debug.Log(progressBar.name);
        if(!progressBar)
            Debug.Log("No object found");

        progressText = GameObject.Find("progressPercentage").GetComponent<TMP_Text>();
        if(progressText)
            Debug.Log(progressText.name);
        if(!progressText)
            Debug.Log("No object found");

        if(PlayerPrefs.GetInt("FIRSTTIMEOPEN", 1) == 1)
        {
            Debug.Log("Game starts for the first time");
            PlayerPrefs.SetInt("FIRSTTIMEOPEN", 0);

            StartCoroutine(Preload());

            //TODO: Set quality of game for the user
        }
        else
        {
            Debug.Log("Game has been started once already, welcome back!");
            StartCoroutine(Preload());

            //TODO: Load into MainMenu without any additional steps
        }
    }

    IEnumerator Preload()
    {
        loadingScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while(!loadingScene.isDone)
        {
            float progress;

            progress = Mathf.Clamp01(loadingScene.progress / 0.9f);
            float progressString = progress * 100;

            progressBar.value = progress;
            progressText.SetText($"{progressString.ToString("0.00")} %");

            yield return null;
        }
    }
}
