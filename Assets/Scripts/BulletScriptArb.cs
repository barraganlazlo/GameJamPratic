using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScriptArb : BulletScript
{
    BulletScript[] childs;

    void Awake()
    {
        childs = transform.GetComponentsInChildren<BulletScript>();
    }
    // Start is called before the first frame update
    public new void Begin()
    {
        Destroy(gameObject, LifeTime);
        begun = true;
        foreach (BulletScript b in childs)
        {
            b.Begin();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (begun)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }
    public new void SetDirection(Vector3 v)
    {
        direction = v;
        foreach (BulletScript b in childs)
        {
            b.SetDirection(v);
        }
    }
}
