using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamScript : MonoBehaviour
{
    public Camera orthographic;
    public Camera perspective;

    public List<GameObject> FloorCubeGroups = new List<GameObject>();

    private void Start()
    {
        // Floor Print Layer Grou을 찾아서 floorPrintLayerGroup에 넣어 줌으로서 큐브를 층별로 관리하기 용이하도록 코드 작성
        GameObject floorPrintLayerGroup = GameObject.Find("Floor Print Layer Group");

        for (int i = 0; i < floorPrintLayerGroup.transform.childCount; i++)
            FloorCubeGroups.Add(floorPrintLayerGroup.transform.GetChild(i).gameObject);


        ButtonInteraction.CamType = Define.CameraDefine.Orthographic;
        orthographic.gameObject.SetActive(true);
        perspective.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ChangeDimension();
        }
    }

    void ChangeDimension()
    {
        if (ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
        {
            //orthographic.gameObject.SetActive(true);
            //perspective.gameObject.SetActive(false);

            OnOffCubeGroup();
        }
        else if (ButtonInteraction.CamType == Define.CameraDefine.Perspective)
        {
            //orthographic.gameObject.SetActive(false);
            //perspective.gameObject.SetActive(true);

            for (int i = 0; i < FloorCubeGroups.Count; i++)
                FloorCubeGroups[i].SetActive(true);
        }
    }

    void OnOffCubeGroup()
    {
        for (int i = 0; i < FloorCubeGroups.Count; i++)
        {
            if (i == LayerDropDown.dropdownValueNum)
            {
                FloorCubeGroups[i].SetActive(true);
                continue;
            }
            else
            {
                FloorCubeGroups[i].SetActive(false);
            }
        }
    }
}
