using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public bool inMenu = false;
    public string defaultNextScene;
    private Animator animator;
    private string newLevel;
    [HideInInspector] public bool isPaused;
    public GameObject AmenuOption_obj;
    public GameObject BmenuOption_obj;
    private SpriteSwap AmenuOption;
    private SpriteSwap BmenuOption;
    [SerializeField] private GameObject pauseMenu;

    public static SceneManagerScript Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        animator = GetComponent<Animator>();
        transform.GetChild(0).gameObject.SetActive(true);
        if (AmenuOption_obj != null)
        {
            AmenuOption = AmenuOption_obj.GetComponent<SpriteSwap>();
            if (BmenuOption_obj != null)
            {
                BmenuOption = BmenuOption_obj.GetComponent<SpriteSwap>();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inMenu)
            {
                Quit();
            }
            else
            {
                PauseGame();
            }
        }
        else if (Input.GetButtonDown("C_Btn_Start"))
        {
            if (!inMenu)
            {
                PauseGame();
            }
        }



        if (inMenu)
        {
            if (AmenuOption_obj != null && (Input.GetButtonDown("C_Btn_Interact1") || Input.GetButtonDown("C_Btn_Interact2")))
            {
                PlayConfirmSound();
                AmenuOption.Swap();
                if (newLevel == null)
                {
                    
                    FadeToLevel(defaultNextScene);
                }
                else
                {
                    FadeToLevel(newLevel);

                }
            }
            else if (BmenuOption_obj != null && (Input.GetButtonDown("C_Btn_Pick1") || Input.GetButtonDown("C_Btn_Pick2")))
            {
                PlayCancelSound();
                BmenuOption.Swap();
                PlayCancelSound();
                Quit();
            }
        }
    }

    public void PauseGame()
    {
        PlayConfirmSound();
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
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

    //private void StopWorldSounds()
    //{
    //    foreach (Sound sound in AudioManager.instance.sounds)
    //    {
    //        if (sound.name != "musique")
    //        {
    //            AudioManager.instance.FadeOutOnEntity(sound.name, AudioManager.instance.gameObject);
    //        }
    //    }
    //}

    //private void StartWorldSounds()
    //{
    //    foreach (Sound sound in AudioManager.instance.sounds)
    //    {
    //        if (sound.name != "musique")
    //        {
    //            AudioManager.instance.FadeOutOnEntity(sound.name, AudioManager.instance.gameObject);
    //        }
    //    }
    //}
}
