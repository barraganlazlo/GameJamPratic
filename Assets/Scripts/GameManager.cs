using UnityEngine;
using System.Collections;
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
    bool lost;

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
            buffBar.fillAmount =((float) life )/ ((float) startLife);
        }
        else
        {
            life = 0;
            Lose();
            buffBar.fillAmount = 0;
        }
    }
    public void Lose()
    {
        if (!lost)
        {
            lost = true;
            Instantiate<GameObject>(uiLose);
            Debug.Log("lost");
        }
    }
    public void StartGame()
    {
        started = true;
        Castle.instance.StartWaves();
    }
}
