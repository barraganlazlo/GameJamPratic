using System.Collections.Generic;
using UnityEngine;

public class PlayerHandleWeapon : MonoBehaviour
{
    [HideInInspector] public WhichPlayer multiplayerScript;

    private Animator animator;

    [Header("Pick and Drop")]
    private Weapon weapon;
    private List<Weapon> pickableWeapons;
    [SerializeField] private GameObject weaponPos;

    [Header("Foin")]
    [HideInInspector] public bool hasFoin = false;
    bool canPickFoin = false;
    [SerializeField] private GameObject foinPrefab;
    [HideInInspector] public Foin foin;

    private SpriteRenderer spriteRenderer;
    public static int upOrder = 46;
    public static int downOrder = 50;
    public static float botLimit = -0.6f;

    private bool canShoot;

    public PlayerHandleWeapon otherPlayer;
    //check playersHolding
    public ButtonSprite buttonCochon;
    public ButtonSprite buttonBotteFoin;

    void Awake()
    {
        multiplayerScript = GetComponent<WhichPlayer>();
        animator = GetComponentInChildren<Animator>();
        pickableWeapons = new List<Weapon>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        //PickDrop
        if (Input.GetButtonDown("PickButton" + multiplayerScript.idPlayer))
        {
            PickUp();
        }
        //Shoot
        else if (canShoot && Input.GetButtonDown("InteractButton" + multiplayerScript.idPlayer))
        {
            if (weapon != null && !weapon.coolingDown)
            {
                animator.SetTrigger("angry");
                Shoot();
            }
        }
        if (transform.position.y < botLimit)
        {
            spriteRenderer.sortingOrder = downOrder;
            if (hasFoin)
            {
                foin.spriteRenderer.sortingOrder = downOrder-1;
            }
        }
        else
        {
            spriteRenderer.sortingOrder = upOrder;
            if (hasFoin)
            {
                foin.spriteRenderer.sortingOrder = upOrder-1;
            }
        }
        
    }

    #region pickAndDrop

    void PickUp()
    {
        Drop();
        if (pickableWeapons.Count>0)
        {
            Weapon w = null;
            float maxd = -1;
            float d = -1;
            for (int i=0; i<pickableWeapons.Count;i++)
            {
                d = Vector2.Distance(transform.position,pickableWeapons[i].transform.position);
                if (maxd>d || maxd==-1)
                {
                    w = pickableWeapons[i];
                }
            }
            Equip(w);
        }
        else if (canPickFoin && !hasFoin && !otherPlayer.hasFoin)
        {
            EquipFoin();
        }
    }
    void Drop()
    {
        if (weapon != null)
        {
            weapon.transform.parent = null;
            weapon.isHeld = false;
            weapon.sr.sortingOrder -= 1;
            weapon = null;
            AudioManager.instance.PlayOnEntity("DropWeapon", gameObject);
        }
        if (hasFoin)
        {
            DestroyFoin();
            AudioManager.instance.PlayOnEntity("DropWeapon", gameObject);
        }
    }
    void Equip(Weapon newWeapon)
    {
        weapon = newWeapon;
        weapon.isHeld = true;
        if (Mathf.Sign(weapon.transform.localScale.x) != Mathf.Sign(transform.localScale.x))
        {
            weapon.transform.localScale = new Vector3(weapon.transform.localScale.x * -1, weapon.transform.localScale.y, weapon.transform.localScale.z);
        }
        weapon.transform.parent = transform;
        weapon.transform.position = weaponPos.transform.position;

        pickableWeapons.Remove(weapon);
        AudioManager.instance.PlayOnEntity("PickWeapon", gameObject);
    }
    void EquipFoin()
    {
        hasFoin = true;
        if (!buttonCochon.isActive)
        {
            buttonCochon.isActive = true;
            buttonBotteFoin.isActive = false;
        }
        foin = Instantiate(foinPrefab, weaponPos.transform.position, Quaternion.identity, transform).GetComponent<Foin>();
        AudioManager.instance.PlayOnEntity("PickWeapon", gameObject);
    }
    public void DestroyFoin()
    {
        hasFoin = false;
        if (buttonCochon.isActive)
        {
            buttonCochon.isActive = false;
            buttonBotteFoin.isActive = true;
        }
        Destroy(foin.gameObject);
    }
    #endregion

    void Shoot()
    {
        weapon.Shoot();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.started)
        {
            return;
        }
        //pick and drop
        if (collision.CompareTag("weapon"))
        {
            pickableWeapons.Add(collision.GetComponent<Weapon>());
        }
        else if (collision.CompareTag("foin"))
        {
            canPickFoin = true;
        }
        //shoot
        else if (collision.CompareTag("shootZone"))
        {
            if (weapon != null)
            {
                Epouvantail epou = collision.GetComponentInParent<Epouvantail>();
                weapon.currentSpawnerAim = epou.spawner;
                canShoot = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!GameManager.instance.started)
        {
            return;
        }
        //pick and drop
        if (collision.CompareTag("weapon"))
        {
            pickableWeapons.Remove(collision.GetComponent<Weapon>());
        }
        else if (collision.CompareTag("foin"))
        {
            canPickFoin = false;
        }
        //shoot
        else if (collision.CompareTag("shootZone"))
        {
            canShoot = false;
        }
    }
}
