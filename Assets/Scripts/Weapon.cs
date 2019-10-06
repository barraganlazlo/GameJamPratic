using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public bool isHeld;


    private Collider2D trigger;
    private SpriteRenderer sr;
    private SpriteRenderer asr;

    [Header ("Set sorting orders")]
    [SerializeField] private int defaultOrderLayer;
    [SerializeField] private int heldOrderLayer;
    [SerializeField] private GameObject accessory;
    [SerializeField] private int accessory_defaultOrderLayer;
    [SerializeField] private int accessory_heldOrderLayer;

    [SerializeField] private GameObject secondSpriteObject;

    [Header ("Shoot")]
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float coolDown;
    private float currentTime;
    private bool coolingDown = false;
    [SerializeField] private float amplitude;
    [SerializeField] private float duree;

    //UNIT KILL AIM
    public int[] unitKillId;
    [HideInInspector]
    public Spawner currentSpawnerAim;

    //apparence 
    public GameObject[] skins;
    public bool switchSkin = false;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sortingOrder = defaultOrderLayer;
        if (accessory != null)
        {
            asr = accessory.GetComponent<SpriteRenderer>();
            asr.sortingOrder = accessory_defaultOrderLayer;
        }

        currentTime = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld && trigger.enabled)
        {
            trigger.enabled = false;
            sr.sortingOrder = heldOrderLayer;
            if (accessory != null)
            {
                asr.sortingOrder = accessory_heldOrderLayer;
            }
        }
        else if (!isHeld && !trigger.enabled)
        {
            trigger.enabled = true;
            sr.sortingOrder = defaultOrderLayer;
            if (accessory != null)
            {
                asr.sortingOrder = accessory_defaultOrderLayer;
            }
            currentTime = coolDown;
        }

        timer();
    }

    public void Shoot()
    {
        if (!coolingDown)
        {
            BulletScript proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<BulletScript>();
            //if (proj == null)
            //{
            //    proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<BulletScriptArb>();
            //}
            proj.direction = (currentSpawnerAim.transform.position - transform.parent.position).normalized;
            proj.Begin();
            foreach(int i in unitKillId)
            {
                currentSpawnerAim.FleeAllEscouade(i);
            }
            ShakeCamera.instance.ShakeCam(duree, amplitude);
            coolingDown = true;

            if(switchSkin == false) //changer skin
            {
                skins[1].SetActive(false);
            }
            else
            {
                skins[0].SetActive(false);
                skins[1].SetActive(true);
            }
        }
    }

    private void timer()
    {
        if (coolingDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                //canShoot
                currentTime = coolDown;
                coolingDown = false;

                if (switchSkin == false) //changer skin une fois le timer finis
                {
                    skins[1].SetActive(true);
                }
                else
                {
                    skins[0].SetActive(true);
                    skins[1].SetActive(false);
                }
            }
        }
    }
}
