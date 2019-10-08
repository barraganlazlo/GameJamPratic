using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScriptArb : MonoBehaviour
{
    BulletScript bull;
    public float speed;
    void Start()
    {
        bull = GetComponentInParent<BulletScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bull.begun)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }
}
