using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public UnitType type;
    public float speed;
    Escouade escouade;
    void Update()
    {
        Vector3 v = - speed * transform.parent.position.normalized * Time.deltaTime;
        transform.position +=  v;
        Debug.Log(v);
    }
}
