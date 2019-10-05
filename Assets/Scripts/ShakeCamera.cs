using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera instance;
    /// <summary>
    /// fait trembler la camera 
    /// celon l'amplitude voulut 
    /// et pendant le temps indiqué.
    /// </summary>

    [HideInInspector] public bool shake = false;

    private Vector3 startPosition;
    private Transform _Cam;

    private float _Amplitude = 0.2f;
    private float _Duration = 0.5f;

    private void Awake()
    {
        if (instance != null){
            Debug.LogError("There are Multiples intances of ShakeCamera but it can only be one");
            return;
        }
        instance = this;
    }
    //__________________________________________________________________________________________________________________
    // Start is called before the first frame update
    void Start()
    {
        _Cam = Camera.main.transform;
        startPosition = _Cam.position;

    }

    private void Update()
    {
        if (shake)
        {
            StartCoroutine(Shaking(_Duration, _Amplitude));
        }
    }


    public void ShakeCam(float duree, float amplitude)
    {
        _Amplitude = amplitude;
        _Duration = duree;
        shake = true;
    }

    IEnumerator Shaking (float dura, float ampli)
    {
        _Cam.position = startPosition + new Vector3(Random.Range(-ampli, ampli),
                                                    Random.Range(-ampli, ampli),0);

        yield return new WaitForSeconds(dura);
        _Cam.position = startPosition;
        shake = false;
    }
}
