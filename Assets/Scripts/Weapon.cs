using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool isHeld;
 
    private Collider2D trigger;
    private SpriteRenderer sr;

    [Header ("Set sorting orders")]
    [SerializeField] private int defaultOrderLayer;
    [SerializeField] private int heldOrderLayer;

    [Header ("Shoot")]
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float coolDown;
    private float currentTime;
    private bool coolingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sortingOrder = defaultOrderLayer;

        currentTime = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld && trigger.enabled)
        {
            trigger.enabled = false;
            sr.sortingOrder = heldOrderLayer;
        }
        else if (!isHeld && !trigger.enabled)
        {
            trigger.enabled = true;
            sr.sortingOrder = defaultOrderLayer;
            currentTime = coolDown;
        }

        timer();
    }

    public void Shoot()
    {
        if (!coolingDown)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            coolingDown = true;
        }
    }

    private void timer()
    {
        if (coolingDown)
        {
            Debug.Log(currentTime);
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                //canShoot
                currentTime = coolDown;
                coolingDown = false;
            }
        }
    }
}
