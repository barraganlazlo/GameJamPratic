using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 3.0f;
    public float LifeTime = 4;
    [HideInInspector]
    public bool begun;
    [HideInInspector]
    public Vector3 direction;
    static float anglediff = 0.1f;
    public virtual void Begin()
    {
        Destroy(gameObject, LifeTime);
        begun = true;
        int i = -transform.childCount/2;
        foreach (Transform child in transform)
        {
            float angle = (Mathf.Atan2(direction.y, direction.x)+i*anglediff) * Mathf.Rad2Deg;
            child.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            i += 1;
        }
    }
    void Update()
    {
        if (begun)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }
}
