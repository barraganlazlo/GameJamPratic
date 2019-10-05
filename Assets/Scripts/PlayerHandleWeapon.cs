using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandleWeapon : MonoBehaviour
{
    private WhichPlayer multiplayerScript;

    [Header ("Pick and Drop")]
    private GameObject currentWeapon = null;
    private bool holdingWeapon = false;
    private GameObject pickableWeapon;
    private Weapon weaponScript;
    private bool canPickWeapon;

    [Header("Shoot")]
    private bool canShoot;

    private void Start()
    {
        multiplayerScript = GetComponent<WhichPlayer>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("PickButton"+multiplayerScript.idPlayer)){
            PickUp();
        }
        else if (canShoot && Input.GetButtonDown("InteractButton"+multiplayerScript.idPlayer))
        {
            Shoot();
        }
        
    }

    #region pickAndDrop

    void PickUp()
    {
        if (holdingWeapon)
        {
            UpdateWeapon(null);
        }
        if (pickableWeapon != null)
        {
            UpdateWeapon(pickableWeapon);
        }
    }

    void UpdateWeapon(GameObject newWeapon)
    {
        if (newWeapon != null)
        {
            
            weaponScript = newWeapon.GetComponent<Weapon>();
            holdingWeapon = true;
            weaponScript.isHeld = true;
            canPickWeapon = false;
            newWeapon.transform.parent = this.transform;
            newWeapon.transform.position = this.transform.position;
            pickableWeapon = null;
        }
        else
        {
            if (currentWeapon != null)
            {
                currentWeapon.transform.parent = null;
                weaponScript.isHeld = false;
                holdingWeapon = false;
            }
        }

        currentWeapon = newWeapon;
    }
    #endregion

    void Shoot()
    {
        weaponScript.Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //pick and drop
        if (collision.tag == "weapon")
        {
            canPickWeapon = true;
            pickableWeapon = collision.gameObject;
        }
    }

    //shoot
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "shootZone")
        {
            if (currentWeapon != null)
            {
                canShoot = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //pick and drop
        if (collision.tag == "weapon")
        {
            canPickWeapon = false;
            pickableWeapon = null;
        }

        //shoot
        else if (collision.gameObject.tag == "shootZone")
        {
            canShoot = false;
        }
    }

}
