using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool isHeld;
    private Collider2D trigger;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld && trigger.enabled)
        {
            trigger.enabled = false;
        }
        else if (!isHeld && !trigger.enabled)
        {
            trigger.enabled = true;
        }
    }
}
