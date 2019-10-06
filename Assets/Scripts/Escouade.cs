﻿using System.Collections.Generic;
using UnityEngine;

public class Escouade : MonoBehaviour
{
    EscouadeType type;
    [HideInInspector]
    public Spawner spawner;
    List<Unit> units;
    public float horizontalMargin;
    public float verticalMargin;

    public void SetType(EscouadeType type)
    {
        this.type = type;
    }
    public void Instantiate(Spawner spawner)
    {
        this.spawner = spawner;
        units = new List<Unit>();
        for (int i = 0; i < type.height; i++)
        {
            for (int j = 0; j < type.width; j++)
            {
                int uId = (type.height * type.width - 1) - (i * type.width + j);
                if (type.units[uId]>-1)
                {
                    UnitType unitType = UnitManager.instance.unitTypes[type.units[uId]];
                    GameObject unitGO = Instantiate<GameObject>(unitType.prefab, transform.position, transform.rotation, transform);
                    Vector3 pos = transform.position;
                    float hSpace = verticalMargin + unitGO.transform.localScale.x;
                    float vSpace = horizontalMargin + unitGO.transform.localScale.y;
                    pos.y -= (type.height - 1) * hSpace * 0.5f;
                    pos.x -= (type.width - 1) * vSpace * 0.5f;
                    pos.x += j * hSpace;
                    pos.y += i * vSpace;
                    unitGO.transform.position = pos;
                    Unit u = unitGO.GetComponent<Unit>();
                    units.Add(u);
                    u.escouade = this;
                    u.type = unitType;
                }
               
            }
        }
        transform.rotation = spawner.transform.rotation;
    }
    public void Flee(UnitType type)
    {

    }
}
