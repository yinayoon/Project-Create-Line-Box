using UnityEngine;
using UnityEngine.EventSystems;

public class ModelMove : MonoBehaviour
{
    public Camera cam;
    private int creatFloorNum;
    int layerMask = 1 << 6;

    // Start is called before the first frame update
    void Start()
    {
        creatFloorNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.ButtonTYPE == Define.ButtonNAME.Move && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
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

        if (creatFloorNum != (LayerDropDown.dropdownValueNum + 1))
            creatFloorNum = (LayerDropDown.dropdownValueNum + 1);

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
                target.transform.position = new Vector3((int)((ray.origin + ray.direction).x), -5, (int)((ray.origin + ray.direction).z));

                float cubeHight = target.transform.localScale.y;
                for (int i = 0; i < creatFloorNum; i++)
                {
                    float cubePosition = Mathf.Abs(target.transform.position.y + cubeHight);
                    
                    cubePosition += 1.0f;

                    target.transform.position = new Vector3(target.transform.position.x, cubePosition, target.transform.position.z);
                }
            }
        }
        else
        {
            if (target != null)
            {
                target.GetComponent<MeshRenderer>().material.color = Color.white;
                target = null;
            }
        }
    }
}