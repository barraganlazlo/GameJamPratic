using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

    /// <summary>
    /// fait trembler la camera 
    /// celon l'amplitude voulut 
    /// et pendant le temps indiqué.
    /// </summary>

    public bool shake = false;

    private Vector3 startPosition;
    private Transform _Cam;

    public float _Amplitude = 0.2f;
    public float _Duration = 0.5f;


    //__________________________________________________________________________________________________________________
    // Start is called before the first frame update
    void Start()
    {
        _Cam = GameObject.FindObjectOfType<Camera>().transform;
        startPosition = _Cam.position;

    }

    //__________________________________________________________________________________________________________________
    // Update is called once per frame
    void Update()
    {
        
        //condition test;
        if(shake == true)
        {
            StartCoroutine(Shaking(_Duration, _Amplitude));
        }

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
