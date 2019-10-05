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
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are Multiples GameManager in the scene but it can only be one ");
        }
        instance = this;

        life = startLife;
    }

    public void Damage(int val)
    {
        Debug.Log("DamageCastle " + val);
        if (life > val)
        {
            life -= val;
            buffBar.rectTransform.localScale = new Vector2((float) life / (float) startLife, 1);
        }
        else
        {
            life = 0;
            Lose();
            buffBar.rectTransform.localScale = new Vector2(0, 1);

        }
    }
    public void Lose()
    {
        Debug.Log("lost");
    }

}
