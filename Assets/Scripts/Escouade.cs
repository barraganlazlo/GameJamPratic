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
        for (int i = 0; i < type.height; i++)
        {
            for (int j = 0; j < type.width; j++)
            {
                int uId = (type.height * type.width - 1) - (i * type.width + j);
                if (type.units[uId]>-1)
                {
                    UnitType unitType = UnitManager.instance.unitTypes[type.units[uId]];
                    GameObject unitGO = Instantiate(unitType.prefab, transform.position, transform.rotation, transform);
                    Vector3 pos = transform.position;
                    float hSpace = verticalMargin ;
                    float vSpace = horizontalMargin ;
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
    }
    public bool Flee(int id)
    {
        bool b = false;
        foreach(Unit u in units)
        {
            if (u.type.id==id)
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
