﻿using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitType", menuName = "UnitType", order = 1)]
public class UnitType : ScriptableObject
{
    public int id;
    public int damages;
    public GameObject prefab;
}