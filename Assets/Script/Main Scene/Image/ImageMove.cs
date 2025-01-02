using UnityEngine;
using UnityEngine.EventSystems;

public class ImageMove : MonoBehaviour
{
    public Camera cam;
    int layerMask = 1 << 7;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.imageEditButtonName == Define.ImageEditButtonNAME.Move && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
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
            target = hit.collider.gameObject;
            Color tempColor = Color.gray;
            tempColor.a = 0.3137255f;
            target.GetComponent<MeshRenderer>().material.color = tempColor;

            if (Input.GetMouseButton(0))
            {
                target.transform.position = new Vector3((int)((ray.origin + ray.direction).x), 1.4f, (int)((ray.origin + ray.direction).z));
            }
        }
        else
        {
            if (target != null)
            {
                Color tempColor = Color.white;
                tempColor.a = 0.3137255f;
                target.GetComponent<MeshRenderer>().material.color = tempColor;
                target = null;
            }
        }
    }
}