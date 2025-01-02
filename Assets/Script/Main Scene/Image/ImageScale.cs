using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageScale : MonoBehaviour
{
    public Camera cam;
    int layerMask = 1 << 7;

    public float sizingFactor = 0.1f;
    private float startSize;
    private float startX;
    private float startY;

    public List<Vector2> xy = new List<Vector2>();

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.imageEditButtonName == Define.ImageEditButtonNAME.Scale && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
        {
            OnButtonClick();
        }
        else if (ButtonInteraction.imageEditButtonName == Define.ImageEditButtonNAME.Default)
        {
            if (target != null)
            {
                Color tempColor = Color.white;
                tempColor.a = 0.3137255f;
                target.GetComponent<MeshRenderer>().material.color = tempColor;
            }
        }
    }

    GameObject target;
    public void OnButtonClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        ButtonInteraction.ButtonTYPE = Define.ButtonNAME.Default;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, layerMask))
        {
            target = hit.collider.gameObject;
            Color tempColor = Color.gray;
            tempColor.a = 0.3137255f;
            target.GetComponent<MeshRenderer>().material.color = tempColor;

            if (Input.GetMouseButtonDown(0))
                ChangeScaleInit();
            else if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButton(1))
                {
                    ChangeScale(target);
                    return;
                }

                ChangeScaleXY(target);
            }
            else if (Input.GetMouseButtonUp(0))
                target = null;
        }
        else
        {
            if (target != null)
            {
                Color tempColor = Color.white;
                tempColor.a = 0.3137255f;
                target.GetComponent<MeshRenderer>().material.color = tempColor;

                if (target != null && Input.GetMouseButton(0))
                {
                    if (Input.GetMouseButton(1))
                    {
                        ChangeScale(target);
                        return;
                    }

                    ChangeScaleXY(target);
                }
                else
                    target = null;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                target = null;
            }
        }
    }

    void ChangeScaleInit()
    {
        float positionZ = 10.0f;
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionZ);
        startX = position.x;
        startY = position.y;
        position = Camera.main.ScreenToWorldPoint(position);
        startSize = target.transform.localScale.y;
    }

    void ChangeScale(GameObject target)
    {
        Vector3 size = target.transform.localScale;
        size.x = startSize + (Input.mousePosition.x - startX) * sizingFactor;
        size.z = startSize + (Input.mousePosition.y - startY) * sizingFactor;
        target.transform.localScale = size;
    }


    List<Vector3> sizeList = new List<Vector3>();
    void ChangeScaleXY(GameObject target)
    {
        sizeList.Add(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        if (sizeList.Count > 2)
        {
            float distanceX = sizeList[1].x - sizeList[0].x;
            float distanceY = sizeList[1].y - sizeList[0].y;

            if (distanceX != 0 && distanceY != 0)
            {

                Vector3 Nomal = target.transform.localScale.normalized;

                //Debug.Log("X : " + distanceX + " / Y : " + distanceY);
                if (Mathf.Abs(distanceX) > Mathf.Abs(distanceY))
                {
                    //Debug.Log("X");
                    if (distanceX > 0)
                    {
                        target.transform.localScale += new Vector3(Nomal.x, 0, Nomal.z);
                    }
                    else if (distanceX < 0)
                    {
                        target.transform.localScale -= new Vector3(Nomal.x, 0, Nomal.z);
                    }
                }
                if (Mathf.Abs(distanceX) < Mathf.Abs(distanceY))
                {
                    //Debug.Log("Y");
                    if (distanceY > 0)
                    {
                        target.transform.localScale += new Vector3(Nomal.x, 0, Nomal.z);
                    }
                    else if (distanceY < 0)
                    {
                        target.transform.localScale -= new Vector3(Nomal.x, 0, Nomal.z);
                    }
                }
            }

            sizeList.Clear();
        }
    }
}