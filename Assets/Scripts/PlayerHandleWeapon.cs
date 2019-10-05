using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandleWeapon : MonoBehaviour
{
    private WhichPlayer multiplayerScript;

    private GameObject currentWeapon = null;
    private bool holdingWeapon = false;

    private GameObject pickableWeapon;
    private Weapon weaponScript;
    private bool canPickWeapon;

    private void Start()
    {
        multiplayerScript = GetComponent<WhichPlayer>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Action"+multiplayerScript.idPlayer)){
            PickUp();
        }
    }

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weapon")
        {
            canPickWeapon = true;
            pickableWeapon = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "weapon")
        {
            canPickWeapon = false;
            pickableWeapon = null;
        }
    }

}
