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
            Debug.Log("lost");
        }
    }
    public void StartGame()
    {
        Castle.instance.CreateSides();
        Castle.instance.StartCoroutine(Castle.instance.WavesCoroutine());
        started = true;
    }
}
