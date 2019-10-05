using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public UnitType type;
    public float speed;
    Escouade escouade;
    public Rigidbody2D r2d;
    bool moving = true;
    bool attacking =false;
    bool attacked=false;
    private void Start()
    {
        r2d.velocity = -speed * transform.parent.position.normalized;
    }
    private void Update()
    {
        
    }
    public void SetMoving(bool b)
    {
        moving = b;
        r2d.simulated = b;
        GetComponent<Collider2D>().enabled = b;
    }

}
