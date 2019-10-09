using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
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
        Quit();
    }

    public void FadeToLevel (string newLevelName)
    {
        newLevel = newLevelName;
        animator.SetTrigger("fadeOut");
        Debug.Log("ouiouiooui");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(newLevel);
    }
    public void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
