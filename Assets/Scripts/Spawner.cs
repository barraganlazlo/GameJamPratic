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

    public int escouadeId=7;
    private void Awake()
    {
        escouades = new List<Escouade>();
    }
    public void SpawnEscouade(EscouadeType escouadeType)
    {
        GameObject escouadeGO = Instantiate<GameObject>(escouadePrefab, transform.position, transform.rotation);
        Escouade escouade = escouadeGO.GetComponent<Escouade>();
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
        //Debug.Log(escouades.Length + " Escouades to spawn");
        int max = 0;
        foreach (EscouadeType et in escouades)
        {
            max += et.tauxApparition;
        }
        int val = Random.Range(1, max);
        int currentVal = 0;
        int id = 0;
        foreach (EscouadeType et in escouades)
        {
            if (currentVal < val && val <= currentVal + et.tauxApparition)
            {
                break;
            }
            currentVal += et.tauxApparition;
            id += 1;
        }
        //Debug.Log("id : " + id);
        SpawnEscouade(escouades[id]);
    }
    public void FleeFirstEscouade(int id)
    {
        foreach (Escouade e in escouades)
        {
            if (e.Flee(id))
            {
                return;
            }
        }
    }
    public void FleeAllEscouade(int id)
    {
        foreach (Escouade e in escouades)
        {
            e.Flee(id);
        }
    }
}
