using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public int idPlayer = 1;

    [HideInInspector] public string horizontal;
    [HideInInspector] public string vertical;

    [HideInInspector] public string interact;
    [HideInInspector] public string pick;


    private void Awake()
    {
        if (idPlayer == 1)
        {
            horizontal = InputManager.Instance.p1_Inputs["Horizontal"];
            vertical = InputManager.Instance.p1_Inputs["Vertical"];

            interact = InputManager.Instance.p1_Inputs["Interact"];
            pick = InputManager.Instance.p1_Inputs["Pick"];
        }
        else if (idPlayer == 2)
        {
            horizontal = InputManager.Instance.p2_Inputs["Horizontal"];
            vertical = InputManager.Instance.p2_Inputs["Vertical"];

            interact = InputManager.Instance.p2_Inputs["Interact"];
            pick = InputManager.Instance.p2_Inputs["Pick"];
        }

        Debug.Log(horizontal);
    }
}
