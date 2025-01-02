using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalcFunc : MonoBehaviour
{
    public GameObject floorPrintLayerGroup;
    public GameObject[] floorCubeGroup;

    public List<float> cubeVolume = new List<float>();
    public List<float> cubeRateVolume = new List<float>();
    public Text dataText;
    public Text rateDataText;
    float finalData;
    float finalVolumeRateData;

    public Text realHeightInputDataText;

    public void Start()
    {
        finalData = 0;
        finalVolumeRateData = 0;

        floorPrintLayerGroup = GameObject.Find("Floor Print Layer Group");
        int count = floorPrintLayerGroup.transform.childCount;
        floorCubeGroup = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            floorCubeGroup[i] = floorPrintLayerGroup.transform.GetChild(i).transform.gameObject;
        }
    }

    public void Update()
    {
        float heightNum;

        if (float.TryParse(realHeightInputDataText.text, out heightNum))
            CalculateVolume(heightNum);
        else
            rateDataText.text = "비율 계산된 부피 값 : 0";
    }

    public void CalculateVolume(float idx)
    {
        // 높이 조절 코드.
        for (int i = 0; i < floorCubeGroup.Length; i++)
        {
            if (floorCubeGroup[i].transform.childCount > 0)
            {
                for (int j = 0; j < floorCubeGroup[i].transform.childCount; j++)
                {
                    Vector3 scale = floorCubeGroup[i].transform.GetChild(j).transform.localScale;
                    float volume = scale.x * scale.y * scale.z;
                    float rateVolume = CalculateRateVolume(scale.x, scale.y, scale.z, idx);
                    //Debug.Log(volume);
                    cubeVolume.Add(volume);
                    cubeRateVolume.Add(rateVolume);
                }
            }
        }

        for (int i = 0; i < cubeVolume.Count; i++)
        {
            finalData += cubeVolume[i];
        }

        for (int i = 0; i < cubeRateVolume.Count; i++)
        {
            finalVolumeRateData += cubeRateVolume[i];
        }

        dataText.text = "부피 값 : " + finalData;
        rateDataText.text = "비율 계산된 부피 값 : " + finalVolumeRateData + " m3";

        // 초기화
        finalData = 0;
        cubeVolume.Clear();

        finalVolumeRateData = 0;
        cubeRateVolume.Clear();
    }

    public float CalculateRateVolume(float x, float y, float z, float rateY)
    {
        float rate = rateY / y;
        float rateX = x * rate;
        float rateZ = z * rate;

        float finalRateData = rateX * rateY * rateZ;

        return finalRateData;
    }
}