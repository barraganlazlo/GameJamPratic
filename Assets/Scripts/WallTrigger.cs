using UnityEngine;

public class WallTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Unit>().SetMoving(false);
    }
}
