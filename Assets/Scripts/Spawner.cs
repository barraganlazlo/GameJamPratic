using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject escouadePrefab;

    List<Escouade> escouades;
    private void Awake()
    {
        escouades = new List<Escouade>();
    }
    public void SpawnEscouade()
    {
        GameObject escouadeGO = Instantiate<GameObject>(escouadePrefab,transform.position,Quaternion.identity);
        Escouade escouade =escouadeGO.GetComponent<Escouade>();
        escouade.SetType(UnitManager.instance.escouadeTypes[Random.Range(0, UnitManager.instance.escouadeTypes.Length)]);
        escouade.Instantiate(this);
        escouades.Add(escouade);
    }
}
