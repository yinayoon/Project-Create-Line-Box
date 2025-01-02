using UnityEngine;
using UnityEngine.EventSystems;

public class ImageRotate : MonoBehaviour
{
    public Camera cam;
    int layerMask = 1 << 7;
    Color tempColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.imageEditButtonName == Define.ImageEditButtonNAME.Rotate && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
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

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(cam.transform.position, ray.direction * 1000.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            if (target != null && target != hit.collider)
            {
                tempColor = Color.white;
                tempColor.a = 0.3137255f;
                target.GetComponent<MeshRenderer>().material.color = tempColor;
            }

            target = hit.collider.gameObject;
            tempColor = Color.gray;
            tempColor.a = 0.3137255f;
            target.GetComponent<MeshRenderer>().material.color = tempColor;

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
                tempColor = Color.white;
                tempColor.a = 0.3137255f;
                target.GetComponent<MeshRenderer>().material.color = tempColor;

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