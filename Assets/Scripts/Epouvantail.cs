using UnityEngine;
using System.Collections;

public class Epouvantail : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite, bool flipX = false)
    {
        if (spriteRenderer==null)
        {
            spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        }
        spriteRenderer.sprite = sprite;
        spriteRenderer.flipX = flipX;
    }
}
