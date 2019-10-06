using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public UnitType type;
    public float speed;
    [HideInInspector]
    public Escouade escouade;
    public Rigidbody2D r2d;
    bool moving = true;
    bool attacking =false;
    bool attacked=false;
    bool fleeing = false;
    public float attackSpeed = 1f;
    public float attackSpeedStart = 1f;
    float attackTimer;
    void Start()
    {

        if (r2d == null)
        {
            r2d=GetComponent<Rigidbody2D>();
        }
        r2d.velocity = speed * -escouade.transform.position.normalized ;

    }
    void Update()
    {
        if (attacking)
        {
            if (attackTimer < 0)
            {
                attackTimer = attackSpeed;
                Attack();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    void SetMoving(bool b)
    {
        moving = b;
        r2d.simulated = b;
        GetComponent<Collider2D>().enabled = b;
    }
    public void StartAttacking()
    {
        if (attacking)
        {
            return;
        }
        attacking = true;
        attackTimer = attackSpeedStart;
        SetMoving(false);
    }
    void StopAttacking()
    {
        attacking = false;
    }
    public void StartFleeing()
    {
        fleeing = true;
        StopAttacking();
        r2d.velocity = (escouade.spawner.transform.position-transform.position).normalized * speed;
        escouade.RemoveUnit(this);
        SetMoving(true);
    }
    public bool IsFleeing()
    {
        return fleeing;
    }
    public void Attack()
    {
        DamageEpouvantail();
    }
    public void DamageEpouvantail()
    {
        escouade.spawner.epouvantail.Damage(type);
    }
    private void OnBecameInvisible()
    {
        if (fleeing)
        {
            Destroy(gameObject);
        }
    }
}
