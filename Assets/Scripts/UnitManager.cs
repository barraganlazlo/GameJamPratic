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

    [HideInInspector]
    public Sprite[] epouvantailsSprites;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are Multiples UnitManager in the scene but it can only be one ");
        }
        instance = this;
        LoadEscouadeTypes();
        LoadUnitTypes();
        LoadEpouvantailSprites();

    }

    private void LoadUnitTypes()
    {
        unitTypes = Resources.LoadAll<UnitType>("UnitTypes");
        Debug.Log("Loaded " + unitTypes.Length + " UnitTypes");

    }
    private void LoadEscouadeTypes()
    {
        escouadeTypes = Resources.LoadAll<EscouadeType>("EscouadeTypes");
        Debug.Log("Loaded " + escouadeTypes.Length + " EscouadeTypes");
    }
    private void LoadEpouvantailSprites()
    {

        epouvantailsSprites = Resources.LoadAll<Sprite>("Epouvantails");
        Debug.Log("Loaded " + epouvantailsSprites.Length + " Epouvantail Sprites");
    }

}
