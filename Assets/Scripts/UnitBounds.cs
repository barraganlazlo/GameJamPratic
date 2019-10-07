using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Unit>().OnOutOfBounds();
    }
}
