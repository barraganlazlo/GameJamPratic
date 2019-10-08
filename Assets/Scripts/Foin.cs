using UnityEngine;

public class Foin : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
