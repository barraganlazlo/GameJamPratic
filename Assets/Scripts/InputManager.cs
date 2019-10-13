using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [HideInInspector] public enum InputIndex { I1, I2}
    [HideInInspector] public enum PlayerIndex { P1, P2}
    [HideInInspector] public enum InputType { Controller, Keyboard };
    private InputType P1type = InputType.Controller;
    private InputType P2type = InputType.Controller;

    [Header("Controller Inputs")]
    [SerializeField] private string C_HorizontalAxis;
    [SerializeField] private string C_VerticalAxis;
    [SerializeField] private string C_InteractButton;
    [SerializeField] private string C_PickButton;

    [Header("KeyBoardInputs")]
    [SerializeField] private string K_HorizontalAxis;
    [SerializeField] private string K_VerticalAxis;
    [SerializeField] private string K_InteractButton;
    [SerializeField] private string K_PickButton;

    public Dictionary<string, string> p1_Inputs;
    public Dictionary<string, string> p2_Inputs;

    [Header ("DropDowns References")]
    [SerializeField] private GameObject Player1Select;
    [SerializeField] private GameObject Player2Select;
    private Dropdown DP_Player1;
    private Dropdown DP_Player2;

    public GameObject continueButton;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }

        DontDestroyOnLoad(gameObject);

        DP_Player1 = Player1Select.GetComponent<Dropdown>();
        DP_Player2 = Player2Select.GetComponent<Dropdown>();

        p1_Inputs = new Dictionary<string, string>();
        p2_Inputs = new Dictionary<string, string>();

        p1_Inputs["Horizontal"] = "";
        p1_Inputs["Vertical"] = "";
        p1_Inputs["Interact"] = "";
        p1_Inputs["Pick"] = "";

        p2_Inputs["Horizontal"] = "";
        p2_Inputs["Vertical"] = "";
        p2_Inputs["Interact"] = "";
        p2_Inputs["Pick"] = "";

        //Init
        SetInputTypes();
        SetAllPlayerInputs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PrintP1Inputs();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            PrintP2Inputs();
        }

        if (continueButton != null)
        {
            if (CheckControllersConnected() && !continueButton.activeSelf)
            {
                continueButton.SetActive(true);
            }
            else if (!CheckControllersConnected() && continueButton.activeSelf)
            {
                continueButton.SetActive(false);
            }
        }
    }

    private void PrintP1Inputs()
    {
        foreach(var key in p1_Inputs.Values)
        {
            Debug.Log(key);
        }
    }

    private void PrintP2Inputs()
    {
        foreach (var key in p2_Inputs.Values)
        {
            Debug.Log(key);
        }
    }


    public void SetInputTypes()
    {
        if (DP_Player1.value == 0)
        {
            P1type = InputType.Controller;
        }
        else if (DP_Player1.value == 1)
        {
            P1type = InputType.Keyboard;
        }

        if (DP_Player2.value == 0)
        {
            P2type = InputType.Controller;
        }
        else if (DP_Player2.value == 1)
        {
            P2type = InputType.Keyboard;
        }
    }

    public void SetAllPlayerInputs()
    {
        if (P1type == InputType.Controller)
        {
            //C && K
            if (P2type == InputType.Keyboard) 
            {
                SetPlayerControls(PlayerIndex.P1, InputIndex.I1, InputType.Controller);
                SetPlayerControls(PlayerIndex.P2, InputIndex.I1, InputType.Keyboard);
            }
            //C && C
            else
            {
                SetPlayerControls(PlayerIndex.P1, InputIndex.I1, InputType.Controller);
                SetPlayerControls(PlayerIndex.P2, InputIndex.I2, InputType.Controller);
            }
        }
        else
        {
            //K && K
            if (P2type == InputType.Keyboard)
            {
                SetPlayerControls(PlayerIndex.P1, InputIndex.I1, InputType.Keyboard);
                SetPlayerControls(PlayerIndex.P2, InputIndex.I2, InputType.Keyboard);
            }
            //K && C
            else
            {
                SetPlayerControls(PlayerIndex.P1, InputIndex.I1, InputType.Keyboard);
                SetPlayerControls(PlayerIndex.P2, InputIndex.I1, InputType.Controller);
            }
        }
    }

    private void SetPlayerControls(PlayerIndex playerIndex, InputIndex inputIndex, InputType inputType)
    {
        if (playerIndex == PlayerIndex.P1)
        {
            if (inputType == InputType.Controller)
            {
                if (inputIndex == InputIndex.I1)
                {
                    p1_Inputs["Horizontal"] = C_HorizontalAxis + "1";
                    p1_Inputs["Vertical"] = C_VerticalAxis + "1";
                    p1_Inputs["Interact"] = C_InteractButton + "1";
                    p1_Inputs["Pick"] = C_PickButton + "1";
                }
                else
                {
                    p1_Inputs["Horizontal"] = C_HorizontalAxis + "2";
                    p1_Inputs["Vertical"] = C_VerticalAxis + "2";
                    p1_Inputs["Interact"] = C_InteractButton + "2";
                    p1_Inputs["Pick"] = C_PickButton + "2";
                }
            }
            else
            {
                if (inputIndex == InputIndex.I1)
                {
                    p1_Inputs["Horizontal"] = K_HorizontalAxis + "1";
                    p1_Inputs["Vertical"] = K_VerticalAxis + "1";
                    p1_Inputs["Interact"] = K_InteractButton + "1";
                    p1_Inputs["Pick"] = K_PickButton + "1";
                }
                else
                {
                    p1_Inputs["Horizontal"] = K_HorizontalAxis + "2";
                    p1_Inputs["Vertical"] = K_VerticalAxis + "2";
                    p1_Inputs["Interact"] = K_InteractButton + "2";
                    p1_Inputs["Pick"] = K_PickButton + "2";
                }
            }
        }
        else
        {
            if (inputType == InputType.Controller)
            {
                if (inputIndex == InputIndex.I1)
                {
                    p2_Inputs["Horizontal"] = C_HorizontalAxis + "1";
                    p2_Inputs["Vertical"] = C_VerticalAxis + "1";
                    p2_Inputs["Interact"] = C_InteractButton + "1";
                    p2_Inputs["Pick"] = C_PickButton + "1";
                }
                else
                {
                    p2_Inputs["Horizontal"] = C_HorizontalAxis + "2";
                    p2_Inputs["Vertical"] = C_VerticalAxis + "2";
                    p2_Inputs["Interact"] = C_InteractButton + "2";
                    p2_Inputs["Pick"] = C_PickButton + "2";
                }
            }
            else
            {
                if (inputIndex == InputIndex.I1)
                {
                    p2_Inputs["Horizontal"] = K_HorizontalAxis + "1";
                    p2_Inputs["Vertical"] = K_VerticalAxis + "1";
                    p2_Inputs["Interact"] = K_InteractButton + "1";
                    p2_Inputs["Pick"] = K_PickButton + "1";
                }
                else
                {
                    p2_Inputs["Horizontal"] = K_HorizontalAxis + "2";
                    p2_Inputs["Vertical"] = K_VerticalAxis + "2";
                    p2_Inputs["Interact"] = K_InteractButton + "2";
                    p2_Inputs["Pick"] = K_PickButton + "2";
                }
            }
        }
    }

    private bool CheckControllersConnected()
    {
        int size = Input.GetJoystickNames().Length;
        if (P1type == InputType.Controller && P2type == InputType.Controller)
        {
            if (size == 0)
            {
                Debug.LogError("no controllers detected");
                return false;
            }
            else if (size == 1)
            {
                Debug.LogError("only one controller is connected");
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (P1type == InputType.Controller || P2type == InputType.Controller)
        {
            if (size == 0)
            {
                Debug.LogError("no controllers detected");
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }
}
