using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (ButtonInteraction.ButtonTYPE == Define.ButtonNAME.Scale && ButtonInteraction.CamType == Define.CameraDefine.Orthographic)
        //{
        //    OnButtonClick();
        //}
    }

    GameObject target;
    public void OnButtonClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(cam.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (target != null && target != hit.collider)
            {
                target.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            target = hit.collider.gameObject;
            target.GetComponent<MeshRenderer>().material.color = Color.black;

            // 버튼을 누른 상태일 때 반응
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.name);
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
