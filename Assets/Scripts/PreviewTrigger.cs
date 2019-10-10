using System.Collections;
using UnityEngine;

public class PreviewTrigger : MonoBehaviour
{
    int[] unitCount;
    public GameObject[] previews;
    private void Start()
    {
        unitCount = new int[3];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (!unit.IsFleeing())
        {
            int i = unit.type.id;
            if (i == 3)
            {
                i = 2;
            }
            unitCount[i]+=1;
        }
        PreviewUnits();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (!unit.IsFleeing())
        {
            int i = unit.type.id;
            if (i==3)
            {
                i = 2;
            }
            unitCount[i] -= 1;
            if(unitCount[i]<0)
            {
                unitCount[i] =0;

            }
        }
        PreviewUnits();
    }
    void PreviewUnits()
    {
        for (int i=0;i<unitCount.Length;i++)
        {
            if (unitCount[i]>0)
            {
                previews[i].SetActive(true);
            }
            else
            {
                previews[i].SetActive(false);
            }
        }
    }
    public void DecreaseUnit(int id)
    {
        int i = id;
        if (i == 3)
        {
            i = 2;
        }
        unitCount[i] -= 1;
        if (unitCount[i] < 0)
        {
            unitCount[i] = 0;

        }
    }
}
