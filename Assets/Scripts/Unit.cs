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
    float attackTimer;
    void Start()
    {
        AudioManager.instance.PlayOnEntity("Sound", gameObject);

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
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z += 180;
        transform.rotation = Quaternion.Euler(rot);
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
}
