using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public UnitType type;
    public float speed;
    [HideInInspector]
    public Escouade escouade;
    Rigidbody2D r2d;
    SpriteRenderer spriteRenderer;
    bool moving = true;
    bool attacking = false;
    bool attacked = false;
    bool fleeing = false;
    public float attackSpeed = 1f;
    public float attackSpeedStart = 1f;
    float attackTimer;
    int order;
    Animator animator;
    PreviewTrigger pt;
    public void Begin()
    {
        r2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sortingOrder = order;
        //r2d.velocity = (escouade.spawner.epouvantail.posToAim.transform.position - transform.position).normalized * speed;
        r2d.velocity = (escouade.spawner.epouvantail.posToAim.transform.position - escouade.transform.position).normalized * speed;

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
        animator.SetBool("walking", b);

        if (b)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        else
            gameObject.GetComponent<AudioSource>().Stop();
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
        r2d.velocity = (escouade.spawner.transform.position - transform.position).normalized * speed;
        transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
        escouade.RemoveUnit(this);
        SetMoving(true);
        if (pt != null)
        {
            pt.DecreaseUnit(type.id);
        }
    }
    public bool IsFleeing()
    {
        return fleeing;
    }
    public void Attack()
    {
        animator.SetTrigger("attack");
    }
    public void DamageEpouvantail()
    {
        escouade.spawner.epouvantail.Damage(type);
    }
    public void OnOutOfBounds()
    {
        if (fleeing)
        {
            Destroy(gameObject);
        }
    }
    public void Flip()
    {
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    public void SetOrder(int i)
    {
        order = i;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PreviewTrigger p = collision.GetComponent<PreviewTrigger>();
        if (p!=null)
        {
            pt = p;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PreviewTrigger p = collision.GetComponent<PreviewTrigger>();
        if (p != null)
        {
            pt = null;
        }
    }
}
