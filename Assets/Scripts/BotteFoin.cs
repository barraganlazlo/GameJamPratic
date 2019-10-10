using System.Collections.Generic;
using UnityEngine;

public class BotteFoin : MonoBehaviour
{
    public static BotteFoin instance;
    public bool taken = false;
    public ButtonSprite button;
    List<GameObject> players;
    void Awake()
    {
        players = new List<GameObject>();
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!players.Contains(collision.gameObject))
        {
            players.Add(collision.gameObject);
        }
        ActivateButton();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (players.Contains(collision.gameObject))
        {
            players.Remove(collision.gameObject);
        }
        ActivateButton();
    }
    public void ActivateButton()
    {
        if (!taken && players.Count > 0)
        {
            button.isActive = true;
        }
        else
        {
            button.isActive = false;
        }
    }
}
