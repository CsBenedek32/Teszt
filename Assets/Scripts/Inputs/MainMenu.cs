using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    public Button PlayButton;
    public Button QuiteButton;


    private void Start()
    {
        PlayButton.onClick.AddListener(PlayGame);
        QuiteButton.onClick.AddListener(QuiteGame);
    }

    public void PlayGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
     
    }

    IEnumerator LoadLevel(int v)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(v);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}
