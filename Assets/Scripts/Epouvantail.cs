using UnityEngine;
using System.Collections;

public class Epouvantail : MonoBehaviour
{
    [HideInInspector]
    public int id;
    public int startLife;
    int life;

    public Spawner spawner;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool broken;
    void Awake()
    {
        life = startLife;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }
    public void SetOrder(int i)
    {
        spriteRenderer.sortingOrder = i;
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
        animator.SetTrigger("attack");
    }
}
