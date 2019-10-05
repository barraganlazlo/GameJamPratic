using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitType", menuName = "UnitType", order = 1)]
public class UnitType : ScriptableObject
{
    public int id;
    public int damagesToEpou;
    public int damagesOnEpouDie;
    public int damagesToBluff;
    public GameObject prefab;
}