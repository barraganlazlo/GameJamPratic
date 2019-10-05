using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEscouadeType", menuName = "EscouadeType", order = 2)]
public class EscouadeType : ScriptableObject
{
    public int id;
    public int height;
    public int width;
    public int waveBegin;
    public int waveEnd;
    public float tauxApparition;//Plus élévé = plus de chance d'apparaitre
    public int[] units;

  
}
public class EscouadeTypeComparer : IComparer<EscouadeType>
{
    public int Compare(EscouadeType x, EscouadeType y)
    {
        return x.id.CompareTo(y.id);
    }
}
