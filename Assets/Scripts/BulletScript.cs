using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 3.0f;
    public float LifeTime = 4;
    protected bool begun;
    [HideInInspector]
    public Vector3 direction;
    // Start is called before the first frame update
    public void Begin()
    {
        Destroy(gameObject, LifeTime);
        begun = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (begun)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }
    public void SetDirection(Vector3 v)
    {
        direction = v;

    }
}
