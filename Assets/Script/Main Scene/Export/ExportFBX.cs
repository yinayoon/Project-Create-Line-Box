using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFBXExporter;

public class ExportFBX : MonoBehaviour
{
    //fbx exporter package by https://github.com/KellanHiggins/UnityFBXExporter
    // webgl file saver plugin by https://github.com/nateonusapps/WebGLFileSaverForUnity
    //make sure that the objectToExport has a MeshFilter and MeshRenderer Component with some Material assigned to it
    // it does not have to have its own mesh, the meshes of children will be exported with their respective material colors
    [SerializeField] private GameObject objectToExport;
    [SerializeField] private Material someMaterial;

    CombineModels combineModels;
    public void Start()
    {
        combineModels = objectToExport.GetComponent<CombineModels>();
    }

    public void DownloadFBX()
    {
        // to assign a material, skip this if you already have one
        objectToExport.GetComponent<MeshRenderer>().material = someMaterial;
        string content = FBXExporter.MeshToString(objectToExport, null, true, true);
        // octet-stream is the right MIME Type for fbx files
        WebGLFileSaver.SaveFile(content, "Object.fbx", "application/octet-stream");

        // objectToExport √ ±‚»≠
        objectToExport.GetComponent<MeshFilter>().mesh = null;
        objectToExport.GetComponent<MeshRenderer>().material = null;
        combineModels.sourceMeshFilters.Clear();
        //Destroy(objectToExport.GetComponent<MeshRenderer>());
    }
}