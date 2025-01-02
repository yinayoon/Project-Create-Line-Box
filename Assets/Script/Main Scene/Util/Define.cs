using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public enum ButtonNAME
    {
        Default = 0,
        Create = 1,
        Delete = 2,
        Move = 3,
        Rotate = 4,
        Scale = 5
    }

    public enum ImageEditButtonNAME
    {
        Default = 0,
        Move = 1,
        Rotate = 2,
        Scale = 3
    }

    public enum CameraDefine
    {
        Default = 0,
        Orthographic = 1,
        Perspective = 2
    }

    public enum LoadImage
    {
        None = 0,
        Load = 1
    }
}
