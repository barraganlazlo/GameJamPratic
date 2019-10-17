using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{
    private Image imageCpnt;
    public Sprite image1;
    public Sprite image2;

    // Start is called before the first frame update
    void Start()
    {
        imageCpnt = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Swap()
    {
        imageCpnt.sprite = image2;
        StartCoroutine(ResetSwap());
    }

    private IEnumerator ResetSwap()
    {
        yield return new WaitForSeconds(0.15f);
        imageCpnt.sprite = image1;
    }
}
