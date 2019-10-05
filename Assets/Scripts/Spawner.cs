using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject escouadePrefab;
    [HideInInspector]
    public Epouvantail epouvantail;
    [HideInInspector]
    public int id;

    List<Escouade> escouades;

    public int escouadeId;
    private void Awake()
    {
        escouades = new List<Escouade>();
    }
    public void SpawnEscouade(EscouadeType escouadeType)
    {
        GameObject escouadeGO = Instantiate<GameObject>(escouadePrefab,transform.position,Quaternion.identity);
        Escouade escouade =escouadeGO.GetComponent<Escouade>();
        escouade.SetType(escouadeType);
        escouade.Instantiate(this);
        escouades.Add(escouade);
    }
    public void SpawnEscouade(int i)
    {
        SpawnEscouade(UnitManager.instance.GetEscouadeTypesOfCurrentWave()[i]);
    }
    public void SpawnRandomEscouade()
    {
        SpawnEscouade(Random.Range(0, UnitManager.instance.GetEscouadeTypesOfCurrentWave().Length));
    }
}
