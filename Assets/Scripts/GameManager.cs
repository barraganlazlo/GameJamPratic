using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int startLife = 100;
    [HideInInspector]
    public int life;
    [HideInInspector]
    public int wave;
    public Image buffBar;
    public float StartCd;
    [HideInInspector]
    public bool started = false;
    public Wave[] waves;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are Multiples GameManager in the scene but it can only be one ");
            return;
        }
        instance = this;

        life = startLife;
        wave = 1;
    }
    private void Start()
    {
        StartGame();
    }
    public void Damage(int val)
    {
        Debug.Log("DamageCastle " + val);
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
        Debug.Log("lost");
    }
    public void StartGame()
    {
        started = true;
        Castle.instance.StartCoroutine(Castle.instance.WavesCoroutine());
    }
}
