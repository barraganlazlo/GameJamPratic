using System.Collections;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public static Castle instance;

    public bool spawning = true;
    [Range(0, 10)]
    public int nombreDeCote;
    public float Decallage;
    public Vector2 offSet;
    public float ratioX;
    public float ratioY;
    public float radius;
    public float spawnDistance;
    public GameObject prefabEpouvantail;
    public GameObject prefabSpawner;

    Epouvantail[] epouvantails;
    Spawner[] spawners;

    GameObject epouvantailsParent;
    GameObject spawnersParent;


    public int waveId;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are Multiples Castles but it can be only one");
            return;
        }
        instance = this;
    }
    public void Begin()
    {
        CreateSides();
        foreach (Epouvantail e in epouvantails)
        {
            e.Begin();
        }
    }
    public void StartWaves()
    {
        StartCoroutine(WavesCoroutine());
    }
    IEnumerator WavesCoroutine()
    {
        if (spawning)
        {
            waveId = 1;
            for (int j = 0; j < GameManager.instance.waves.Length; j++)
            {
                Wave wave = GameManager.instance.waves[j];
                Debug.Log("Wave : " + waveId);
                int nbEscouades = Random.Range(wave.nbEscouadeMin, wave.nbEscouadeMax);
                for (int i = 1; i < nbEscouades + 1; i++)
                {
                    Spawner sp = spawners[Random.Range(0, spawners.Length)];
                    sp.SpawnRandomEscouade();
                    float cdNextEscouade = Random.Range(wave.cdNextEscouadeMin, wave.cdNextEscouadeMax);
                    yield return new WaitForSeconds(cdNextEscouade);
                }
                waveId += 1;
                yield return new WaitForSeconds(wave.cdNextWave);
            }
        }
    }
    public void CreateSides()
    {
        ResetSides();
        epouvantailsParent = new GameObject("EpouvantailsParent");
        spawnersParent = new GameObject("SpawnersParent");
        for (int i = 0; i < nombreDeCote; i++)
        {
            float ang = 360f / nombreDeCote;

            GameObject epou = Instantiate<GameObject>(prefabEpouvantail);
            epou.transform.position = PlaceInCircle(i * ang + Decallage);
            epou.transform.parent = epouvantailsParent.transform;
            Epouvantail epouScript = epou.GetComponent<Epouvantail>();
            epouvantails[i] = epouScript;

            GameObject spaw = Instantiate<GameObject>(prefabSpawner);
            spaw.transform.position = PlaceInCircle(i * ang + Decallage, spawnDistance);
            spaw.transform.rotation = Quaternion.Euler(0, 0, 180 - (i * ang+Decallage));
            spaw.transform.parent = spawnersParent.transform;
            Spawner spawScript = spaw.GetComponent<Spawner>();
            spawners[i] = spawScript;
            spawScript.epouvantail = epouScript;
            epouScript.spawner = spawScript;
        }
    }
    void ResetSides()
    {

        epouvantails = new Epouvantail[nombreDeCote];

        spawners = new Spawner[nombreDeCote];
    }
    Vector3 PlaceInCircle(float ang, float distance = 1)
    {
        Vector3 center = transform.position;

        Vector3 pos;
        pos.x = offSet.x + transform.localScale.x * ratioX * (center.x + Mathf.Sin(ang * Mathf.Deg2Rad) + radius * Mathf.Sin(ang * Mathf.Deg2Rad));
        pos.y = offSet.y + transform.localScale.y * ratioY * (center.y + Mathf.Cos(ang * Mathf.Deg2Rad) + radius * Mathf.Cos(ang * Mathf.Deg2Rad));
        pos.z = center.z;
        pos *= distance;
        return pos;
    }
}
