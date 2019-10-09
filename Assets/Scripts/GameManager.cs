using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int startLife = 100;
    [HideInInspector]
    public int life;
    public Image buffBar;
    public float StartCd;
    [HideInInspector]
    public bool started = false;
    public Wave[] waves;
    bool lost = false;
    bool won = false;
    float oldLifeRatio;
    public GameObject uiLose;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are Multiples GameManager in the scene but it can only be one ");
            return;
        }
        instance = this;

        life = startLife;
    }
    private void Start()
    {
        Castle.instance.Begin();
        StartGame();
    }
    public void Damage(int val)
    {
        if (life > val)
        {
            life -= val;
            buffBar.fillAmount = life / ((float)startLife);
        }
        else
        {
            life = 0;
            Lose();
            buffBar.fillAmount = 0;
        }
        float lratio = life / ((float)startLife);
        if (oldLifeRatio >= 0.75f && lratio < 0.75f)
        {
            foreach (Epouvantail epou in Castle.instance.epouvantails)
            {
                epou.NextSprite();
            }
        }
        else if (oldLifeRatio >= 0.5f && lratio < 0.5f)
        {
            foreach (Epouvantail epou in Castle.instance.epouvantails)
            {
                epou.NextSprite();
            }
        }
        else if (oldLifeRatio >= 0.25f && lratio < 0.25f)
        {
            foreach (Epouvantail epou in Castle.instance.epouvantails)
            {
                epou.NextSprite();
            }
        }
        oldLifeRatio = lratio;

    }
    public void Lose()
    {
        if (!lost && !won)
        {
            lost = true;
            Instantiate<GameObject>(uiLose);
            Debug.Log("lost");
            buffBar.transform.parent.gameObject.SetActive(false);
            StartCoroutine(GoToLoseScreen());
        }
    }

    public void Win()
    {
        if (!lost && !won)
        {
            won = true;
            buffBar.transform.parent.gameObject.SetActive(false);
            StartCoroutine(GotoWinScreen());
        }
    }

    IEnumerator GotoWinScreen()
    {
        yield return new WaitForSeconds(7.3f);
        SceneManagerScript.Instance.FadeToLevel("WinScreen");
    }

    IEnumerator GoToLoseScreen()
    {
        yield return new WaitForSeconds(3.0f);

        SceneManagerScript.Instance.FadeToLevel("LoseScreen");
    }
    public void StartGame()
    {
        started = true;
        Castle.instance.StartWaves();
    }
}
