using UnityEngine;

public class WallTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (!unit.IsFleeing())
        {
            unit.StartAttacking();
        }
    }
}
