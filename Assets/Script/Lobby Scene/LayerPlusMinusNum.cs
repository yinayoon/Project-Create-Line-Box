using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerPlusMinusNum : MonoBehaviour
{
    public Text numText;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        num = 1;
        DecidedFigures.layerNum = 1;
        numText.text = num.ToString() + " Ãþ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlusMinusFunc(int idx)
    {
        switch (idx)
        {
            case 0:
                if (num >= 10)
                    num = 10;
                else
                    num++;

                numText.text = num.ToString() + " Ãþ";
                DecidedFigures.layerNum = num;
                break;
            case 1:
                if (num <= 1)
                    num = 1;
                else
                    num--;

                numText.text = num.ToString() + " Ãþ";
                DecidedFigures.layerNum = num;
                break;
        }
    }
}
