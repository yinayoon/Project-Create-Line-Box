using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModelScale : MonoBehaviour
{
    int layerMask = 1 << 6;

    public float sizingFactor = 0.1f;
    private GameObject target = null;
    private float startSize;
    private float startX;
    private float startY;

    float prevX;
    float currX;
    float prevY;
    float currY;

    float distanceX;
    float distanceY;

    int timeIdx;
    List<Vector2> xy = new List<Vector2>();

    public void Start()
    {
        timeIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.ButtonTYPE == Define.ButtonNAME.Scale && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
        {
            OnButtonClick();
        }
    }

    public void OnButtonClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        ButtonInteraction.imageEditButtonName = Define.ImageEditButtonNAME.Default;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, layerMask))
        {            
            if (target != hit.collider && target != null)
            {
                target.GetComponent<MeshRenderer>().material.color = Color.white;
                target = hit.collider.gameObject;
                target.GetComponent<MeshRenderer>().material.color = Color.black;
            }
            else if (target == null)
            {
                target = hit.collider.gameObject;
                target.GetComponent<MeshRenderer>().material.color = Color.black;
            }

            if (Input.GetMouseButtonDown(0))
            {
                ChangeScaleInit();
            }
            else if (Input.GetMouseButton(0))
            {
                ChangeScale(target);
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
                target.GetComponent<MeshRenderer>().material.color = Color.white;

                if (target != null && Input.GetMouseButton(0))
                {
                    ChangeScale(target);
                }
                else
                {
                    target = null;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                target.GetComponent<MeshRenderer>().material.color = Color.white;
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
}