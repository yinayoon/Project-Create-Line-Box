using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecidedFigures : MonoBehaviour
{
    //DecidedFigures decidedFigures;

    public static int layerNum;

    private void Start()
    {
        //decidedFigures = this;
        //DontDestroyOnLoad(decidedFigures.transform);
        DontDestroyOnLoad(transform);
    }
}
