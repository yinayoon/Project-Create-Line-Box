using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviseHeight : MonoBehaviour
{
    public float originValue;

    public GameObject floorPrintLayerGroup;
    public GameObject[] floorCubeGroup;

    public void Start()
    {
        floorPrintLayerGroup = GameObject.Find("Floor Print Layer Group");
        int count = floorPrintLayerGroup.transform.childCount;
        floorCubeGroup = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            floorCubeGroup[i] = floorPrintLayerGroup.transform.GetChild(i).transform.gameObject;
        }
    }

    void Update()
    {
        if (ButtonInteraction.CamType == Define.CameraDefine.Perspective)
        {
            // 높이 조절 코드.
            for (int i = 0; i < floorCubeGroup.Length; i++)
            {
                if (floorCubeGroup[i].transform.childCount > 0)
                {
                    for (int j = 0; j < floorCubeGroup[i].transform.childCount; j++)
                    {
                        Vector3 scaleVector = floorCubeGroup[i].transform.GetChild(j).transform.localScale;
                        Vector3 posVector = floorCubeGroup[i].transform.GetChild(j).transform.position;
                        posVector.y = (i + 1) * ButtonInteraction.staticHeightValue;
                        floorCubeGroup[i].transform.GetChild(j).transform.localScale = new Vector3(scaleVector.x, ButtonInteraction.staticHeightValue, scaleVector.z);
                        floorCubeGroup[i].transform.GetChild(j).transform.position = posVector;
                    }
                }
            }
        }
    }
}

