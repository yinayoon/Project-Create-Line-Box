using UnityEngine;
using UnityEngine.EventSystems;

public class ModelRotate : MonoBehaviour
{
    public Camera cam; 
    int layerMask = 1 << 6;

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.ButtonTYPE == Define.ButtonNAME.Rotate && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
        {
            OnButtonClick();
        }
    }

    GameObject target;
    public void OnButtonClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        ButtonInteraction.imageEditButtonName = Define.ImageEditButtonNAME.Default;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(cam.transform.position, ray.direction * 1000.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            if (target != null && target != hit.collider)
            {
                target.GetComponent<MeshRenderer>().material.color = Color.white;
            }

            target = hit.collider.gameObject;
            target.GetComponent<MeshRenderer>().material.color = Color.black;

            // 버튼을 누른 상태일 때 반응
            if (Input.GetMouseButton(0))
            {
                RotateFunc();
            }
        }
        else
        {
            if (target != null)
            {
                target.GetComponent<MeshRenderer>().material.color = Color.white;

                // 버튼을 누른 상태일 때 반응
                if (Input.GetMouseButton(0)) 
                { 
                    RotateFunc();
                }
               
                if (Input.GetMouseButtonUp(0))
                {
                    target = null;
                }
            }
        }
    }

    void RotateFunc()
    {
        //target.transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);

        if (Input.GetAxis("Mouse X") > 0)
        {
            target.transform.Rotate(0f, -1f, 0f, Space.World);
        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            target.transform.Rotate(0f, 1f, 0f, Space.World);
        }
    }
}
