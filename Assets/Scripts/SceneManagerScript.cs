﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public bool inMenu = false;
    public string defaultNextScene;
    private Animator animator;
    private string newLevel;

    public static SceneManagerScript Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        animator = GetComponent<Animator>();
        transform.GetChild(0).gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        if (inMenu)
        {
            if (Input.GetButtonDown("InteractButton1") || Input.GetButtonDown("InteractButton2"))
            {
                if (newLevel == null)
                {
                    
                    FadeToLevel(defaultNextScene);
                }
                else
                {
                    FadeToLevel(newLevel);
                }
            }
            else if (Input.GetButtonDown("PickButton1") || Input.GetButtonDown("PickButton2"))
            {
                Quit();
            }
        }
    }

    public void FadeToLevel(string newLevelName)
    {
        newLevel = newLevelName;
        animator.SetTrigger("fadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(newLevel);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PlayConfirmSound()
    {
        AudioManager.instance.PlayOnEntity("ui_confirm", AudioManager.instance.gameObject);
    }

    public void PlayCancelSound()
    {
        AudioManager.instance.PlayOnEntity("ui_cancel", AudioManager.instance.gameObject);
    }
}
