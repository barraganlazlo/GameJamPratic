using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    [HideInInspector]
    public EscouadeType[] escouadeTypes;
    [HideInInspector]
    public UnitType[] unitTypes;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are Multiples UnitManager in the scene but it can only be one ");
        }
        instance = this;
    }

    private void LoadUnitTypes()
    {
        unitTypes = Resources.LoadAll<UnitType>("UnitTypes");
    }
    private void LoadEscouadeTypes()
    {
        unitTypes = Resources.LoadAll<UnitType>("UnitTypes");
    }

}
