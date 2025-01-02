using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloorPlane : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] planeArray;
    public GameObject decidedFigures;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.Find("DecidedFigures") == null)
        {
            GameObject go = Instantiate(decidedFigures, Vector3.zero, Quaternion.identity);
            go.name = decidedFigures.name;
            DecidedFigures.layerNum = 1;

            DontDestroyOnLoad(go);
        }

        GameObject floorPlanes = new GameObject("Floor Planes");
        floorPlanes.transform.position = new Vector3(0, 1.4f, -10);
        floorPlanes.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        planeArray = new GameObject[DecidedFigures.layerNum];

        for (int i = 0; i < DecidedFigures.layerNum; i++)
        {
            GameObject floorPlane = Instantiate(prefab, floorPlanes.transform.position, floorPlanes.transform.rotation);
            floorPlane.name = prefab.name;
            floorPlane.transform.parent = floorPlanes.transform;
            planeArray[i] = floorPlane;
        }

        InitFloorPlane();
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.loadImage == Define.LoadImage.None)
        {
            if (planeArray.Length <= 0)
                return;

            for (int i = 0; i < DecidedFigures.layerNum; i++)
                planeArray[i].SetActive(false);
        }
        else
            InitFloorPlane();
    }

    void InitFloorPlane()
    {
        for (int i = 0; i < DecidedFigures.layerNum; i++)
        {
            if (i == ButtonInteraction.dropDownIdx)
                planeArray[i].SetActive(true);
            else
                planeArray[i].SetActive(false);
        }
    }
}