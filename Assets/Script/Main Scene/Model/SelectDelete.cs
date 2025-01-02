using UnityEngine;
using UnityEngine.EventSystems;

public class SelectDelete : MonoBehaviour
{
    public Camera cam;
    int layerMask = 1 << 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteraction.ButtonTYPE == Define.ButtonNAME.Delete && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
        {
            OnButtonClick();
        }
    }

    GameObject target;
    public void OnButtonClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(cam.transform.position, ray.direction * 1000.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {            
            if (target != null && target != hit.collider)
                target.GetComponent<MeshRenderer>().material.color = Color.white;

            target = hit.collider.gameObject;
            target.GetComponent<MeshRenderer>().material.color = Color.black;

            if (Input.GetMouseButtonDown(0))
                Destroy(target);
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
