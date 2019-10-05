using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEscouadeType", menuName = "EscouadeType", order = 2)]
public class EscouadeType : ScriptableObject
{
    public int height;
    public int width;
    public int waveBegin;
    public int waveEnd;
    public int[] units;
}
