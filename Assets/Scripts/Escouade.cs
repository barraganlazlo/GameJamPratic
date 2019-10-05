using UnityEngine;
using System.Collections;

public class Escouade : MonoBehaviour
{
    EscouadeType type;

    Unit[,] units;

    public void SetType(EscouadeType type)
    {
        this.type = type;
    }
    public void Instantiate()
    {
        units = new Unit[type.units.GetLength(0), type.units.GetLength(1)];
        for (int i = 0; i < units.GetLength(0); i++)
        {
            for (int j = 0; j < units.GetLength(1); j++)
            {
                GameObject unitGO = Instantiate<GameObject>(UnitManager.instance.unitTypes[type.units[i, j]].prefab);
                units[i, j] = unitGO.GetComponent<Unit>();
            }
        }
    }
}
