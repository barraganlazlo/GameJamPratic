﻿using UnityEngine;

public class Escouade : MonoBehaviour
{
    EscouadeType type;
    Spawner spawner;
    Unit[,] units;

    public float horizontalMargin;
    public float verticalMargin;
    public void SetType(EscouadeType type)
    {
        this.type = type;
    }
    public void Instantiate(Spawner spawner)
    {
        this.spawner = spawner;
        units = new Unit[type.height, type.width];
        for (int i = 0; i < type.height; i++)
        {
            for (int j = 0; j < type.width; j++)
            {
                int uId = (type.height * type.width - 1) - (i * type.width + j);
                UnitType unitType = UnitManager.instance.unitTypes[type.units[uId]];
                GameObject unitGO = Instantiate<GameObject>(unitType.prefab, transform.position, transform.rotation, transform);
                Vector3 pos = transform.position;
                float hSpace = verticalMargin + unitType.verticalSize;
                float vSpace = horizontalMargin + unitType.horizontalSize;
                pos.y -= (type.height - 1) * hSpace * 0.5f;
                pos.x -= (type.width - 1) * vSpace * 0.5f;
                pos.x += j * hSpace;
                pos.y += i * vSpace;
                unitGO.transform.position = pos;
                units[i, j] = unitGO.GetComponent<Unit>();
            }
        }
        transform.rotation = spawner.transform.rotation;
    }
    public void Flee(UnitType type)
    {

    }
}
