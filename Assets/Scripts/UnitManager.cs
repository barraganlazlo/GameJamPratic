using UnityEngine;
using System;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    [HideInInspector]
    public EscouadeType[] escouadeTypes;
    [HideInInspector]
    public UnitType[] unitTypes;

    [HideInInspector]
    public Sprite[] epouvantailsSprites;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are Multiples UnitManager in the scene but it can only be one ");
            return;
        }
        instance = this;
        LoadEscouadeTypes();
        LoadUnitTypes();
    }

    private void LoadUnitTypes()
    {
        unitTypes = Resources.LoadAll<UnitType>("UnitTypes");
        Debug.Log("Loaded " + unitTypes.Length + " UnitTypes");
        Array.Sort(unitTypes,new UnitTypeComparer());

    }
    private void LoadEscouadeTypes()
    {
        escouadeTypes = Resources.LoadAll<EscouadeType>("EscouadeTypes");
        Debug.Log("Loaded " + escouadeTypes.Length + " EscouadeTypes");
        Array.Sort(escouadeTypes,new EscouadeTypeComparer());
    }

    public EscouadeType[] GetEscouadeTypesOfCurrentWave()
    {
        List<EscouadeType> list = new List<EscouadeType>();
        foreach (EscouadeType etype in escouadeTypes)
        {
            if (etype.waveBegin<=GameManager.instance.wave && (GameManager.instance.wave < etype.waveEnd || etype.waveEnd<1))
            {
                list.Add(etype);
            }
        }
        return list.ToArray();
    }
}
