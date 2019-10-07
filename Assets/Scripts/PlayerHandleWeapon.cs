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
    [HideInInspector] public GameObject foin;

    private bool canShoot;

    void Awake()
    {
        multiplayerScript = GetComponent<WhichPlayer>();
        animator = GetComponentInChildren<Animator>();
        pickableWeapons = new List<Weapon>();
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
            if (!weapon.coolingDown)
            {
                animator.SetTrigger("angry");
                Shoot();
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
        else if (canPickFoin && !hasFoin)
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
        Debug.Log("foin");
        foin = Instantiate(foinPrefab, weaponPos.transform.position, Quaternion.identity, transform);
        AudioManager.instance.PlayOnEntity("PickWeapon", gameObject);
    }
    public void DestroyFoin()
    {
        hasFoin = false;
        Destroy(foin);
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
