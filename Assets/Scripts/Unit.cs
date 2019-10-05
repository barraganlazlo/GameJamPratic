using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public UnitType type;
    public float speed;
    Escouade escouade;
    public Rigidbody2D r2d;
    bool moving = true;
    private void Start()
    {
        r2d.velocity = -speed * transform.parent.position.normalized;
    }
    public void SetMoving(bool b)
    {
        moving = b;
        r2d.simulated = b;
        GetComponent<Collider2D>().enabled = b;
    }

}
