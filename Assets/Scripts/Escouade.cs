using System.Collections.Generic;
using UnityEngine;

public class Escouade : MonoBehaviour
{
    EscouadeType type;
    [HideInInspector]
    public Spawner spawner;
    List<Unit> units;
    List<Unit> toRemove;
    public float horizontalMargin;
    public float verticalMargin;

    public void SetType(EscouadeType type)
    {
        this.type = type;
    }
    public void Instantiate(Spawner spawner)
    {
        toRemove = new List<Unit>();
        this.spawner = spawner;
        units = new List<Unit>();
        int startOrder;
        int growingorder;
        if (transform.position.y>-1)
        {
            startOrder = -25;
        }
        else
        {
            startOrder = 25;
        }
        if (transform.position.x > 0)
        {
            growingorder = -1;
        }
        else
        {
            growingorder = 1;
        }
        for (int i = 0; i < type.height; i++)
        {
            for (int j = 0; j < type.width; j++)
            {
                int uId = (type.height * type.width - 1) - (i * type.width + j);
                if (type.units[uId]>-1)
                {
                    UnitType unitType = UnitManager.instance.unitTypes[type.units[uId]];
                    GameObject unitGO = Instantiate(unitType.prefab, transform.position, transform.rotation, transform);
                    Vector3 pos = Vector3.zero;
                    float hSpace = verticalMargin ;
                    float vSpace = horizontalMargin ;
                    pos.y += Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * (-(type.height - 1) * hSpace * 0.5f) + i * vSpace;
                    pos.x += Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * (-(type.width - 1) * vSpace * 0.5f) + j * hSpace;
                    unitGO.transform.localPosition = pos;
                    Unit u = unitGO.GetComponent<Unit>();
                    u.transform.rotation = Quaternion.identity;
                    units.Add(u);
                    u.escouade = this;
                    u.type = unitType;
                    u.SetOrder(startOrder + j * growingorder - i);
                }               
            }
        }
        foreach(Unit u in units)
        {
            u.Begin();
        }

        if ( transform.position.x<0)
        {
            Flip();
        }
    }
    public bool Flee(int id)
    {
        bool b = false;
        foreach(Unit u in units)
        {
            if (u.type.id==id || id==-3)
            {
                u.StartFleeing();
                b = true;
            }
        }
        RemoveUnits();
        return b;
    }
    public void RemoveUnit(Unit u)
    {
        toRemove.Add(u);
    }
    void RemoveUnits()
    {
        while (toRemove.Count > 0)
        {
            units.Remove(toRemove[0]);
            toRemove.RemoveAt(0);
        }
    }
    public void Flip()
    {
        foreach(Unit u in units)
        {
            u.Flip();
        }
    }
}
