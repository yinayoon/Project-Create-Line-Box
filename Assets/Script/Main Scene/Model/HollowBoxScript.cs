using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowBoxScript : MonoBehaviour
{
    public GameObject prefab;
    public float wallThickness = 0.5f;

    void OnEnable()
    {
        prefab = Resources.Load<GameObject>("Prefabs/HollowBox");

        MakeHollowBox(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        TrackBoxSizeChange(transform.gameObject);
    }

    // 처음 한번 실행되는 함수
    void MakeHollowBox(GameObject targetModel)
    {
        // HollowBox를 위한 벽면 생성
        GameObject width_1 = Instantiate(prefab, targetModel.transform.position, Quaternion.identity);
        GameObject width_2 = Instantiate(prefab, targetModel.transform.position, Quaternion.identity);
        GameObject length_1 = Instantiate(prefab, targetModel.transform.position, Quaternion.identity);
        GameObject length_2 = Instantiate(prefab, targetModel.transform.position, Quaternion.identity);
        GameObject ground = Instantiate(prefab, targetModel.transform.position, Quaternion.identity);

        // 오브젝트 이름 변경
        width_1.name = "width_1";
        width_2.name = "width_2";
        length_1.name = "length_1";
        length_2.name = "length_2";
        ground.name = "ground";

        // 각각의 오브젝트들에 공통된 부모 오브젝트 부여
        GameObject parent = new GameObject();
        parent.transform.position = targetModel.transform.position;
        width_1.transform.SetParent(parent.transform);
        width_2.transform.SetParent(parent.transform);
        length_1.transform.SetParent(parent.transform);
        length_2.transform.SetParent(parent.transform);
        ground.transform.SetParent(parent.transform);
        parent.transform.rotation = targetModel.transform.rotation;

        width_1.transform.SetParent(targetModel.transform);
        width_2.transform.SetParent(targetModel.transform);
        length_1.transform.SetParent(targetModel.transform);
        length_2.transform.SetParent(targetModel.transform);
        ground.transform.SetParent(targetModel.transform);

        Destroy(parent);
    }

    Vector3 tempScale;
    float tempWallThickness;
    void TrackBoxSizeChange(GameObject targetModel)
    {
        GameObject width_1 = targetModel.transform.Find("width_1").gameObject;
        GameObject width_2 = targetModel.transform.Find("width_2").gameObject;
        GameObject length_1 = targetModel.transform.Find("length_1").gameObject;
        GameObject length_2 = targetModel.transform.Find("length_2").gameObject;
        GameObject ground = targetModel.transform.Find("ground").gameObject;

        if (tempScale != targetModel.transform.localScale || tempWallThickness != wallThickness)
        {
            length_1.transform.localPosition = new Vector3(0.5f, 0, 0);
            length_2.transform.localPosition = new Vector3(-0.5f, 0, 0);
            ground.transform.localPosition = new Vector3(0, -0.5f, 0);
            width_1.transform.localPosition = new Vector3(0, 0, 0.5f);
            width_2.transform.localPosition = new Vector3(0, 0, -0.5f);

            width_1.transform.SetParent(null);
            width_2.transform.SetParent(null);
            length_1.transform.SetParent(null);
            length_2.transform.SetParent(null);
            ground.transform.SetParent(null);

            Vector3 p = targetModel.transform.position;
            p.y = 0;
            targetModel.transform.position = p;

            width_1.transform.localScale = new Vector3(targetModel.transform.localScale.x, targetModel.transform.localScale.y, wallThickness);
            width_2.transform.localScale = new Vector3(targetModel.transform.localScale.x, targetModel.transform.localScale.y, wallThickness);
            length_1.transform.localScale = new Vector3(wallThickness, targetModel.transform.localScale.y, targetModel.transform.localScale.z);
            length_2.transform.localScale = new Vector3(wallThickness, targetModel.transform.localScale.y, targetModel.transform.localScale.z);
            ground.transform.localScale = new Vector3(targetModel.transform.localScale.x, 1, targetModel.transform.localScale.z);

            length_1.transform.position = new Vector3(length_1.transform.position.x - (wallThickness / 2), targetModel.transform.position.y, length_1.transform.position.z);
            length_2.transform.position = new Vector3(length_2.transform.position.x + (wallThickness / 2), targetModel.transform.position.y, length_2.transform.position.z);
            width_1.transform.position = new Vector3(width_1.transform.position.x, targetModel.transform.position.y, width_1.transform.position.z - (wallThickness / 2));
            width_2.transform.position = new Vector3(width_2.transform.position.x, targetModel.transform.position.y, width_2.transform.position.z + (wallThickness / 2));
            ground.transform.position = new Vector3(ground.transform.position.x, (targetModel.transform.position.y - (targetModel.transform.localScale.y / 2)), ground.transform.position.z);

            width_1.transform.SetParent(targetModel.transform);
            width_2.transform.SetParent(targetModel.transform);
            length_1.transform.SetParent(targetModel.transform);
            length_2.transform.SetParent(targetModel.transform);
            ground.transform.SetParent(targetModel.transform);
        }

        tempScale = targetModel.transform.localScale;
        tempWallThickness = wallThickness;        
    }        
}