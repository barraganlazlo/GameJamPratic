using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Castle : MonoBehaviour
{
    [Range(0,10)]
    public int nombreDeCote;

    public float ratioX;
    public Vector2 offSet;
    public float ratioY;
    public float radius;
    public float spawnDistance;
    public GameObject prefabEpouvantail;
    public GameObject prefabSpawner;
    public Sprite epouvantailFace;
    public Sprite epouvantailDos;
    public int startLife = 100;
    [HideInInspector]
    public int life;
    List<Epouvantail> epouvantails;
    List<Spawner> spawners;

    GameObject epouvantailsParent;
    GameObject spawnersParent;
    private void Awake()
    {
        CreateSides();
        life = startLife;
    }
    public void CreateSides()
    {
        ResetSides();
        epouvantailsParent = new GameObject("EpouvantailsParent");
        spawnersParent = new GameObject("SpawnersParent");
        for (int i=0; i<nombreDeCote; i++)
        {
            float ang = 360f / nombreDeCote;

            GameObject epou = Instantiate<GameObject>(prefabEpouvantail);
            epou.transform.position = PlaceInCircle(i * ang );
            epou.transform.parent = epouvantailsParent.transform;
            Epouvantail epouScript = epou.GetComponent<Epouvantail>();
            epouScript.id = i;
            epouvantails.Add(epouScript);
            if (i >= nombreDeCote*0.25f && i<nombreDeCote * 0.75f)
            {
                epouScript.SetSprite(epouvantailFace);
                if (i >= nombreDeCote / 2f + 1)
                {
                    epouScript.SetSprite(epouvantailFace, true);

                }

            }
            else
            {
                epouScript.SetSprite(epouvantailDos);
                if (i >= 1 && i<= nombreDeCote*0.75f)
                {
                    epouScript.SetSprite(epouvantailDos, true);
                }
            }

            GameObject spaw = Instantiate<GameObject>(prefabSpawner);
            spaw.transform.position = PlaceInCircle(i * ang,spawnDistance);
            spaw.transform.rotation = Quaternion.Euler(0,0,180 - i * ang);
            spaw.transform.parent = spawnersParent.transform;
            Spawner spawScript = spaw.GetComponent<Spawner>();
            spawScript.id = i;
            spawners.Add(spawScript);
            spawScript.epouvantail = epouScript;
            epouScript.spawner = spawScript;

        }
    }
    void ResetSides()
    {
        if (epouvantails == null)
        {
            epouvantails = new List<Epouvantail>();
        }
        
        if (spawnersParent != null)
        {
            Debug.Log("Destroy ep");
            DestroyImmediate(epouvantailsParent);
            Destroy(epouvantailsParent);
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
        }

        spawners.Clear();
    }
    Vector3 PlaceInCircle(float ang,float distance= 1)
    {
        Vector3 center = transform.position;

        Vector3 pos;
        pos.x =offSet.x + transform.localScale.x *ratioX * (center.x + Mathf.Sin(ang * Mathf.Deg2Rad) + radius * Mathf.Sin( ang * Mathf.Deg2Rad));
        pos.y = offSet.y+ transform.localScale.y * ratioY * (center.y + Mathf.Cos(ang * Mathf.Deg2Rad) + radius * Mathf.Cos(ang * Mathf.Deg2Rad));
        pos.z = center.z;
        pos *= distance;
        return pos;
    }
}
