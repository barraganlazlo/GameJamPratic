using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool isHeld;

    [SerializeField] private bool isFoin = false;

    private Collider2D trigger;
    private SpriteRenderer sr;
    private SpriteRenderer asr;

    [Header ("Shoot")]
    [SerializeField]
    private GameObject projectile;
    [SerializeField] private float coolDown;
    private float currentTime;

    [SerializeField]
    private float amplitude;
    [SerializeField] private float duree;

    [HideInInspector]
    public bool coolingDown = false;

    //UNIT KILL AIM
    public int[] unitKillId;
    [HideInInspector]
    public Spawner currentSpawnerAim;

    //apparence 
    public Sprite defaultSprite;
    public Sprite emptySprite;

    void Awake()
    {
        trigger = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        currentTime = coolDown;
    }

    void Update()
    {
        if (!isFoin)
        {
            if (isHeld && trigger.enabled)
            {
                trigger.enabled = false;
            }
            else if (!isHeld && !trigger.enabled)
            {
                trigger.enabled = true;
                currentTime = coolDown;
            }
        }
        else if (isHeld && trigger.enabled)
        {
            trigger.enabled = false;
        }
        else if (!isHeld && !trigger.enabled)
        {
            trigger.enabled = true;
        }

        timer();
    }

    public void Shoot()
    {
            switch(gameObject.name)
            {
                case "cassouletGun":
                    AudioManager.instance.PlayOnEntity("Cassoulet_fire", gameObject);
                    break;
                case "canonGun":
                    AudioManager.instance.PlayOnEntity("Canon_fire", gameObject);
                    break;
                default:
                    AudioManager.instance.PlayOnEntity("Arbaliste_fire", gameObject);
                    break;
            }

            BulletScript proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<BulletScript>();
            proj.direction = (currentSpawnerAim.transform.position - transform.parent.position).normalized;
            proj.Begin();
            foreach(int i in unitKillId)
            {
                currentSpawnerAim.FleeAllEscouade(i);
            }
            ShakeCamera.instance.ShakeCam(duree, amplitude);
            coolingDown = true;

        //changer skin
        sr.sprite = emptySprite;
         
    }

    void timer()
    {
        if (coolingDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                //canShoot
                currentTime = coolDown;
                coolingDown = false;

                sr.sprite = defaultSprite;
            }
        }
    }
}
