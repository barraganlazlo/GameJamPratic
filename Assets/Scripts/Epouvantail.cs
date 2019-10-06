using UnityEngine;
using System.Collections;

public class Epouvantail : MonoBehaviour
{
    [HideInInspector]
    public int id;
    public int startLife;
    int life;
    //[HideInInspector]
    public Spawner spawner;
    SpriteRenderer spriteRenderer;
    bool broken;
    void Awake()
    {
        life = startLife;
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }

    public void SetSprite(bool flipX )
    {
        spriteRenderer.flipX = flipX;
    }
    public void Damage(UnitType type)
    {
        if (type.damagesToEpou > 0)
        {
            if ( life> type.damagesToEpou)
            {
                life -= type.damagesToEpou;
            }
            else
            {
                life = 0;
                GameManager.instance.Damage(type.damagesOnEpouDie);
            }
        }
        else
        {
            GameManager.instance.Damage(type.damagesToBluff);
        }
    }
}
