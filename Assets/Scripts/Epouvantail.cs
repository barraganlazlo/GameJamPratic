using UnityEngine;
using System.Collections.Generic;
public class Epouvantail : MonoBehaviour
{
    [HideInInspector]
    public int id;
    public int startLife;
    int life;
    public GameObject posToAim;

    [HideInInspector]
    public Spawner spawner;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool broken;
    int spriteId;
    ButtonSprite button;
    List<PlayerHandleWeapon> players;
    public void Begin()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        button = GetComponentInChildren<ButtonSprite>();
        players = new List<PlayerHandleWeapon>();
        life = startLife;
        if (transform.position.y > 0)
        {
            spriteId = 4;
            spriteRenderer.sortingOrder = 1;
            if (transform.position.x > 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            spriteId = 0;
            spriteRenderer.sortingOrder = 51;
            if (transform.position.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        if (transform.position.x < 0)
        {
            animator.SetBool("left", true);
        }
        spriteRenderer.sprite = UnitManager.instance.epouvantailsSprites[spriteId];
    }
    public void SetOrder(int i)
    {
        spriteRenderer.sortingOrder = i;
    }
    public void Damage(UnitType type)
    {
        if (type.damagesToEpou > 0)
        {
            if (life > type.damagesToEpou)
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
    public void TurnShootZone(Quaternion q)
    {
        GetComponentInChildren<Collider2D>().transform.parent.rotation = q;
    }
    public void NextSprite()
    {
        spriteId += 1;
        spriteRenderer.sprite = UnitManager.instance.epouvantailsSprites[spriteId];
    }
    public void ActivateButton(bool b, PlayerHandleWeapon player)
    {
        if (b)
        {
            if (!players.Contains(player))
            {
                players.Add(player);
            }
        }
        else
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }
        }
        if (players.Count>0)
        {
            button.isActive = true;
        }
        else
        {
            button.isActive = false;
        }
    }
}
