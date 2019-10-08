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
    public int tauxApparition;//Plus élévé = plus de chance d'apparaitre
    public int[] units;

  
}
public class EscouadeTypeComparer : IComparer<EscouadeType>
{
    public int Compare(EscouadeType x, EscouadeType y)
    {
        int l = "Escouade".Length;
        int xi = int.Parse(x.name.Substring(l, x.name.Length - l));
        int yi = int.Parse(y.name.Substring(l, y.name.Length - l));
        return xi.CompareTo(yi);
    }
}
