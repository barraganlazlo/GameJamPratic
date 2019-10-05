using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Castle : MonoBehaviour
{
    [Range(0,10)]
    public int nombreDeCote;

    public float ratioX;
    public float ratioY;
    public float radius;
    public float spawnDistance;
    public GameObject prefabEpouvantail;
    public GameObject prefabSpawner;

    List<GameObject> epouvantails;
    List<Spawner> spawners;

    GameObject epouvantailsParent;
    GameObject spawnersParent;
    private void Awake()
    {
        CreateSides();
    }
    private void OnDestroy()
    {
        ResetSides();
    }
    public void CreateSides()
    {
        ResetSides();
        epouvantailsParent = new GameObject("EpouvantailsParent");
        spawnersParent = new GameObject("SpawnersParent");
        for (int i=0; i<nombreDeCote; i++)
        {
            float ang = 360f / nombreDeCote;

            GameObject Epouvantail = Instantiate<GameObject>(prefabEpouvantail);
            Epouvantail.transform.position = PlaceInCircle(i * ang );
            Epouvantail.transform.parent = epouvantailsParent.transform;
            epouvantails.Add(Epouvantail);

            GameObject Spawner = Instantiate<GameObject>(prefabSpawner);
            Spawner.transform.position = PlaceInCircle(i * ang,spawnDistance);
            Spawner.transform.parent = spawnersParent.transform;
            spawners.Add(Spawner.GetComponent<Spawner>());
        }
    }
    void ResetSides()
    {
        if (epouvantails == null)
        {
            epouvantails = new List<GameObject>();
        }
        
        if (spawnersParent != null)
        {
            Debug.Log("Destroy ep");
            //StartCoroutine(Destroy(epouvantailsParent));//allow destroying in edit Mode
            DestroyImmediate(epouvantailsParent);
        }

        epouvantails.Clear();

        if (spawners == null)
        {
            spawners = new List<Spawner>();
        }
        
        if (spawnersParent != null)
        {
            Debug.Log("Destroy sp");
            DestroyImmediate(spawnersParent);
            //StartCoroutine(Destroy(spawnersParent));//allow destroying in edit Mode
        }

        spawners.Clear();
    }
    IEnumerator Destroy(GameObject go) //destroy go in edit mode
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }
    Vector3 PlaceInCircle(float ang,float distance= 1)
    {
        Vector3 center = transform.position;

        Vector3 pos;
        pos.x = ratioX * (center.x + Mathf.Sin(ang * Mathf.Deg2Rad) + radius * Mathf.Sin( ang * Mathf.Deg2Rad));
        pos.y = ratioY * (center.y + Mathf.Cos(ang * Mathf.Deg2Rad) + radius * Mathf.Cos(ang * Mathf.Deg2Rad));
        pos.z = center.z;
        pos *= distance;
        return pos;
    }
}
