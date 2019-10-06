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
        SpawnEscouade(UnitManager.instance.escouadeTypes[i]);
    }
    public void SpawnRandomEscouade()
    {
        EscouadeType[] escouades = UnitManager.instance.GetEscouadeTypesOfCurrentWave();
        int max = 0;
        foreach(EscouadeType et in escouades)
        {
            max += et.tauxApparition;            
        }
        int val = Random.Range(1, max);
        max = 0;
        int id = -1;
        foreach (EscouadeType et in escouades)
        {
            if (max<val) {
                max += et.tauxApparition;
                id += 1;
            }
            else
            {
                break;
            }
        }
        SpawnEscouade(escouades[id]);
    }
}
