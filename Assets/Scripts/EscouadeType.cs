using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEscouadeType", menuName = "EscouadeType", order = 2)]
public class EscouadeType : ScriptableObject
{
    public int[,] units= new int[4,4];
}
