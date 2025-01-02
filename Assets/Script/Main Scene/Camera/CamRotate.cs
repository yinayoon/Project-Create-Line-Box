using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamRotate : MonoBehaviour
{
    public Transform follow;
    Vector2 m_Input;

    public void LateUpdate()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Rotate();
        }
    }

    void Rotate()
    {
        if (Input.GetMouseButton(0))
        {
            m_Input.x = Input.GetAxis("Mouse X");
            m_Input.y = Input.GetAxis("Mouse Y");

            if (m_Input.magnitude != 0)
            {
                Quaternion q = follow.rotation;
                q.eulerAngles = new Vector3(q.eulerAngles.x - m_Input.y * 2, q.eulerAngles.y + m_Input.x * 2, q.eulerAngles.z);
                follow.rotation = q;
            }
        }
    }
}