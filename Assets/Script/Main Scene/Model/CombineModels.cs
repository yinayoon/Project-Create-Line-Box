using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CombineModels : MonoBehaviour
{
    public List<MeshFilter> sourceMeshFilters;
    public MeshFilter targetMeshFilter;

    public GameObject cubeGroup;
    public Toggle hollowToggle;
    Transform[] child;
    Transform[] child_;

    public void Start()
    {
        hollowToggle.isOn = false;
        cubeGroup = GameObject.Find("Floor Print Layer Group");
    }

    public void Update()
    {
        if (cubeGroup.gameObject.transform.childCount > 0)
        {
            int childCount = cubeGroup.gameObject.transform.childCount;
            child_ = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                child_[i] = cubeGroup.gameObject.transform.GetChild(i);
            }

            for (int i = 0; i < child_.Length; i++)
            {
                if (child_[i].childCount > 0)
                {
                    //Debug.Log(i + ": " + "뭐가 있음");
                    for (int j = 0; j < child_[i].childCount; j++)
                    {
                        //Debug.Log(i + " / " + j + ": " + child[i].gameObject.transform.GetChild(j).name);
                        if (!hollowToggle.isOn)
                        {
                            //Debug.Log("Toggle Off");
                            child_[i].gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().enabled = true;
                        }
                        else
                        {
                            //Debug.Log("Toggle On");

                            //List<GameObject> child_j = new List<GameObject>();
                            child_j.Add(child_[i].GetChild(j).gameObject);
                            if (child_j.Count > 0)
                            {
                                for (int k = 0; k < child_[i].GetChild(j).childCount; k++)
                                {
                                    child_[i].gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().enabled = false;
                                    //child_[i].GetChild(j).GetChild(k).GetComponent<MeshFilter>();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void CombineMeshs()
    {
        CombineMeshsFunc();
        //this.gameObject.AddComponent<MeshRenderer>();
    }

    public List<GameObject> child_j = new List<GameObject>();

    [ContextMenu(itemName:"Combine Meshs")]
    private void CombineMeshsFunc()
    {
        if (cubeGroup.gameObject.transform.childCount > 0)
        {
            //Debug.Log(cubeGroup.gameObject.transform.childCount);
            int childCount = cubeGroup.gameObject.transform.childCount;
            child = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                child[i] = cubeGroup.gameObject.transform.GetChild(i);
            }

            for (int i = 0; i < child.Length; i++)
            {
                if (child[i].childCount > 0)
                {
                    //Debug.Log(i + ": " + "뭐가 있음");
                    for (int j = 0; j < child[i].childCount; j++)
                    {
                        //Debug.Log(i + " / " + j + ": " + child[i].gameObject.transform.GetChild(j).name);
                        if (!hollowToggle.isOn)
                        {
                            Debug.Log("Toggle Off");                            
                            sourceMeshFilters.Add(child[i].gameObject.transform.GetChild(j).GetComponent<MeshFilter>());
                        }
                        else
                        {
                            Debug.Log("Toggle On");
                            
                            //List<GameObject> child_j = new List<GameObject>();
                            child_j.Add(child[i].GetChild(j).gameObject);
                            if (child_j.Count > 0)
                            {
                                for (int k = 0; k < child[i].GetChild(j).childCount; k++)
                                {
                                    sourceMeshFilters.Add(child[i].GetChild(j).GetChild(k).GetComponent<MeshFilter>());
                                }
                            }
                        }
                    }
                }
            }
        }

        // 합치기
        var combine = new CombineInstance[sourceMeshFilters.Count];

        for (var i = 0; i < sourceMeshFilters.Count; i++)
        {
            combine[i].mesh = sourceMeshFilters[i].sharedMesh;
            combine[i].transform = sourceMeshFilters[i].transform.localToWorldMatrix;
        }

        var mesh = new Mesh();
        mesh.CombineMeshes(combine);
        targetMeshFilter.mesh = mesh;
    }
}