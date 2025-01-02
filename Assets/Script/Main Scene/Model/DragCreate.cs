using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragCreate : MonoBehaviour
{
    public Transform cubeObject;
    public Rigidbody bead;
    public Camera cam;
    public float absoluteYPosition = 0f;
    public float absoluteYScale = 0.5f;

    public List<GameObject> FloorCubeGroups = new List<GameObject>();

    private Transform activeObject;
    private Vector3 startPosition;
    private int creatFloorNum;

    // Use this for initialization
    void Start()
    {
        creatFloorNum = 1;

        // Floor Print Layer Group을 찾아서 floorPrintLayerGroup에 넣어 줌으로서 큐브를 층별로 관리하기 용이하도록 코드 작성
        GameObject floorPrintLayerGroup = GameObject.Find("Floor Print Layer Group");

        for (int i = 0; i < floorPrintLayerGroup.transform.childCount; i++)
            FloorCubeGroups.Add(floorPrintLayerGroup.transform.GetChild(i).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.ButtonTYPE == Define.ButtonNAME.Create && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
        {
            OnOffCubeGroup();
            OnButtonClick();
        }
    }

    void OnOffCubeGroup()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

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

    void OnButtonClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (creatFloorNum != (LayerDropDown.dropdownValueNum + 1))
            creatFloorNum = (LayerDropDown.dropdownValueNum + 1);

        Vector3 position = cam.ScreenToWorldPoint(Input.mousePosition);
        position.y = absoluteYPosition;

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Left-Click
            if (Input.GetMouseButtonDown(0))
            {
                activeObject = Instantiate(cubeObject, this.transform.position, Quaternion.identity) as Transform;
                startPosition = position;
                activeObject.name = cubeObject.name;

                activeObject.transform.parent = GameObject.Find(creatFloorNum + " Floor Cube Group").transform;

                Form(activeObject, startPosition, position);
            }
            if (Input.GetMouseButton(0))
            {
                Form(activeObject, startPosition, position);
            }
            if (Input.GetMouseButtonUp(0))
            {
                activeObject.localScale = new Vector3(Mathf.Abs(activeObject.localScale.x), Mathf.Abs(activeObject.localScale.y), Mathf.Abs(activeObject.localScale.z));

                if (Mathf.Abs(activeObject.transform.localScale.x) <= 5 && Mathf.Abs(activeObject.transform.localScale.z) <= 5)
                    Destroy(activeObject.gameObject);

                // 수정 요망
                float cubeHight = activeObject.transform.localScale.y;
                for (int i = 0; i < creatFloorNum; i++)
                {
                    float cubePosition = Mathf.Abs(activeObject.position.y + cubeHight);

                    cubePosition += 1.0f;

                    activeObject.position = new Vector3(activeObject.position.x, cubePosition, activeObject.position.z);
                }
            }
        }
    }

    private void Form(Transform shape, Vector3 start, Vector3 end)
    {
        Vector3 scale = end - start;
        scale = new Vector3((int)scale.x, (int)scale.y, (int)scale.z);
        scale.y = absoluteYScale;

        Vector3 pos = start + scale / 2;
        pos = new Vector3((int)pos.x, (int)pos.y, (int)pos.z);
        pos.y = absoluteYPosition;

        shape.position = pos;
        shape.localScale = scale;
    }
}