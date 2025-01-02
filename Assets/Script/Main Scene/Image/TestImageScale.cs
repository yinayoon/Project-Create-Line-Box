using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestImageScale : MonoBehaviour
{
    public float sizingFactor = 0.1f;
    private float startSize;
    private float startX;
    private float startY;

    GameObject target;

    List<Vector3> sizeList = new List<Vector3>();

    private void Start()
    {

    }

    void Update()
    {
        OnButtonClick();
    }

    public void OnButtonClick()
    {
        ButtonInteraction.ButtonTYPE = Define.ButtonNAME.Default;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            target = hit.collider.gameObject;
            Color tempColor = Color.gray;
            tempColor.a = 0.3137255f;
            target.GetComponent<MeshRenderer>().material.color = tempColor;

            if (Input.GetMouseButtonDown(0))
            {
                ChangeScaleInit();
            }
            else if (Input.GetMouseButton(0))
            {
                ChangeScaleXY(target);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                target = null;
            }
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
                    ChangeScaleXY(target);
                }
                else
                {
                    target = null;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                target = null;
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
                            target.transform.localScale += new Vector3(Nomal.x / 2, 0, Nomal.z / 2);
                        }
                        else if (distanceX < 0)
                        {
                            target.transform.localScale -= new Vector3(Nomal.x / 2, 0, Nomal.z / 2);
                        }
                    }
                    if (Mathf.Abs(distanceX) < Mathf.Abs(distanceY))
                    {
                        //Debug.Log("Y");
                        if (distanceY > 0)
                        {
                            target.transform.localScale += new Vector3(Nomal.x / 2, 0, Nomal.z / 2);
                        }
                        else if (distanceY < 0)
                        {
                            target.transform.localScale -= new Vector3(Nomal.x / 2, 0, Nomal.z / 2);
                        }
                    }
                }

                sizeList.Clear();
            }
        }
    }
}
