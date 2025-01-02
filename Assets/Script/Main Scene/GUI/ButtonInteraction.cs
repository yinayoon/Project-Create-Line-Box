using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    // 버튼 상태
    public static Define.ButtonNAME ButtonTYPE = Define.ButtonNAME.Default;
    public static Define.CameraDefine CamType = Define.CameraDefine.Default;
    public static Define.LoadImage loadImage = Define.LoadImage.None;
    public static Define.ImageEditButtonNAME imageEditButtonName = Define.ImageEditButtonNAME.Default;

    public static int dropDownIdx;

    public Text camText;
    public Button edit2DFloorPlane;
    public Button create3DFloorplane;
    public Button[] buttons;
    public Image buttonsImages;
    public Button[] imageButtons;
    public Image imagebuttonsImages;
    public Button[] loadCreateButtons;
    public Dropdown[] dropDown;
    public GameObject camRig;
    public GameObject[] planeArray;
    public GameObject inputHight;
    public Slider inputHightSlider;
    public InputField heightInputField;
    public GameObject dimension3dUI;

    public Toggle toggle;

    private bool signCreat;
    private bool signEdit;

    private int originToggleNum;

    public static float planeWidth;
    public static float planeHeight;
    
    string inputHeightValue;
    public static float staticHeightValue;

    bool inputTextSign;

    public Text widthText;
    public Text heightText;

    public void Start()
    {
        buttonsImages.transform.gameObject.SetActive(false);
        imagebuttonsImages.transform.gameObject.SetActive(false);

        inputTextSign = false;
        originToggleNum = 0;

        ButtonTYPE = Define.ButtonNAME.Default;
        CamType = Define.CameraDefine.Default;
        loadImage = Define.LoadImage.None;
        imageEditButtonName = Define.ImageEditButtonNAME.Default;

        GameObject planeGroup = GameObject.Find("Floor Planes");
        planeArray = new GameObject[DecidedFigures.layerNum];

        #region 상태 초기화를 위한 For문
        for (int i = 0; i < DecidedFigures.layerNum; i++)
            planeArray[i] = planeGroup.transform.GetChild(i).gameObject;

        for (int i = 0; i < imageButtons.Length; i++)
            imageButtons[i].gameObject.SetActive(false);

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(false);

        for (int i = 0; i < dropDown.Length; i++)
            dropDown[i].gameObject.SetActive(true);
        #endregion

        inputHight.SetActive(false);
        dimension3dUI.SetActive(false);

        signCreat = true;
        signEdit = true;
        loadImage = Define.LoadImage.Load;

        toggle.isOn = false;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        { 
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                InitializedButton();
                ChangeLoadCreateButtonState(true, true, true);
                return;
            }
        }
        inputTextSign = heightInputField.gameObject.transform.Find("Text").GetComponent<Text>().text.Length > 0;

        if (inputTextSign)
        {
            inputHeightValue = heightInputField.gameObject.transform.Find("Text").GetComponent<Text>().text;

            if (CamType == Define.CameraDefine.Perspective)
                staticHeightValue = inputHightSlider.value;
        }        

        dropDownIdx = dropDown[0].value; // 드롭다운 벨류 static으로 전달.

        if (originToggleNum != LayerDropDown.dropdownValueNum)
        {
            for (int i = 0; i < planeArray.Length; i++)
            {
                if (i == LayerDropDown.dropdownValueNum)
                {
                    originToggleNum = i;
                    planeArray[i].GetComponent<MeshCollider>().enabled = true;
                    continue;
                }
                else
                {
                    planeArray[i].GetComponent<MeshCollider>().enabled = false;
                }
            }
        }

        AllFloorPlaneOnOff();
    }

    public void OnClick(int idx)
    {
        imageEditButtonName = Define.ImageEditButtonNAME.Default;
        ChangeLoadCreateButtonState(true, true, true);

        switch (idx)
        {
            case 0:
                ButtonTYPE = Define.ButtonNAME.Default;
                break;
            case 1:
                ButtonTYPE = Define.ButtonNAME.Create;
                break;
            case 2:
                ButtonTYPE = Define.ButtonNAME.Delete;
                break;
            case 3:
                ButtonTYPE = Define.ButtonNAME.Move;
                break;
            case 4:
                ButtonTYPE = Define.ButtonNAME.Rotate;
                break;
            case 5:
                ButtonTYPE = Define.ButtonNAME.Scale;
                break;
        }
    }

    public void OnClickImage(int idx)
    {
        ButtonTYPE = Define.ButtonNAME.Default;
        ChangeLoadCreateButtonState(true, true, true);

        switch (idx)
        {
            case 0:
                imageEditButtonName = Define.ImageEditButtonNAME.Default;
                break;
            case 1:
                imageEditButtonName = Define.ImageEditButtonNAME.Move;
                break;
            case 2:
                imageEditButtonName = Define.ImageEditButtonNAME.Rotate;
                break;
            case 3:
                imageEditButtonName = Define.ImageEditButtonNAME.Scale;
                break;
        }
    }

    public GameObject camera_Othographic;
    public GameObject camera_Perspective;
    public Toggle hollowBoxToggle;

    public void ChangeDimension()
    {
        ChangeLoadCreateButtonState(true, true, true);
        if (CamType == Define.CameraDefine.Orthographic)
        {
            camera_Perspective.SetActive(true);
            camera_Othographic.SetActive(false);

            camText.text = "2차원";
            CamType = Define.CameraDefine.Perspective;

            camRig.transform.rotation = Quaternion.identity;

            InitializedButton();

            #region 버튼을 누를 때마다 변화하는 상태 초기화를 위한 For문
            for (int i = 0; i < imageButtons.Length; i++)
                imageButtons[i].gameObject.SetActive(false);

            for (int i = 0; i < loadCreateButtons.Length; i++)
                loadCreateButtons[i].gameObject.SetActive(false);

            for (int i = 0; i < dropDown.Length; i++)
                dropDown[i].gameObject.SetActive(false);
            #endregion

            ChangeStateForDimension(false);

            dimension3dUI.SetActive(true);
            inputHight.SetActive(true);

            loadImage = Define.LoadImage.None;
        }
        else
        {
            hollowBoxToggle.isOn = false;
            camera_Perspective.SetActive(false);
            camera_Othographic.SetActive(true);

            camText.text = "3차원";
            CamType = Define.CameraDefine.Orthographic;

            InitializedButton();

            #region 버튼을 누를 때마다 변화하는 상태 초기화를 위한 For문
            for (int i = 0; i < imageButtons.Length; i++)
                imageButtons[i].gameObject.SetActive(false);

            for (int i = 0; i < loadCreateButtons.Length; i++)
                loadCreateButtons[i].gameObject.SetActive(true);

            for (int i = 0; i < dropDown.Length; i++)
                dropDown[i].gameObject.SetActive(true);
            #endregion

            ChangeStateForDimension(true);

            dimension3dUI.SetActive(false);
            inputHight.SetActive(false);

            loadImage = Define.LoadImage.Load;
        }
    }

    // 차원에 따른 gui의 상태 변화를 위한 함수
    void ChangeStateForDimension(bool state)
    {
        toggle.transform.gameObject.SetActive(state);
        edit2DFloorPlane.transform.gameObject.SetActive(state);
        create3DFloorplane.transform.gameObject.SetActive(state);

        for (int i = 0; i < planeArray.Length; i++)
            planeArray[i].SetActive(state);
    }

    public void InitializedButton()
    {
        signCreat = true;
        signEdit = true;

        #region 버튼의 상태 초기화를 위한 For문
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(false);

        for (int i = 0; i < imageButtons.Length; i++)
            imageButtons[i].gameObject.SetActive(false);
        #endregion

        buttonsImages.transform.gameObject.SetActive(false);
        imagebuttonsImages.transform.gameObject.SetActive(false);
    }

    public void OnOffButtonCreateAndLoadAndEdit(int idx)
    {
        imageEditButtonName = Define.ImageEditButtonNAME.Default;
        ButtonTYPE = Define.ButtonNAME.Default;

        switch (idx)
        {
            case 0:
                InitializedButton();

                ChangeLoadCreateButtonState(true, true, true);
                GetImage.GetImageFromUserAsync(planeArray[dropDownIdx].name, "ReceiveImage");

                widthText.text = ClickAndGetImage.widthPoint.ToString();
                heightText.text = ClickAndGetImage.heightPoint.ToString();

                break;
            case 1: 
                signCreat = true;

                buttonsImages.transform.gameObject.SetActive(false);
                imagebuttonsImages.transform.gameObject.SetActive(true);

                for (int i = 0; i < buttons.Length; i++)
                    buttons[i].gameObject.SetActive(false);

                if (signEdit)
                {
                    for (int i = 0; i < imageButtons.Length; i++)
                        imageButtons[i].gameObject.SetActive(true);
                    signEdit = false;
                }
                else
                {
                    for (int i = 0; i < imageButtons.Length; i++)
                        imageButtons[i].gameObject.SetActive(false);
                    signEdit = true;
                }

                ChangeLoadCreateButtonState(true, false, true);
                break;
            case 2: 
                signEdit = true;

                buttonsImages.transform.gameObject.SetActive(true);
                imagebuttonsImages.transform.gameObject.SetActive(false);

                for (int i = 0; i < imageButtons.Length; i++)
                    imageButtons[i].gameObject.SetActive(false);

                if (signCreat)
                {
                    for (int i = 0; i < buttons.Length; i++)
                        buttons[i].gameObject.SetActive(true);
                    signCreat = false;
                }
                else
                {
                    for (int i = 0; i < buttons.Length; i++)
                        buttons[i].gameObject.SetActive(false);
                    signCreat = true;
                }

                ChangeLoadCreateButtonState(true, true, false);
                break;
        }
    }

    void ChangeLoadCreateButtonState(bool a, bool b, bool c)
    {
        loadCreateButtons[0].interactable = a;
        loadCreateButtons[1].interactable = b;
        loadCreateButtons[2].interactable = c;
    }

    void AllFloorPlaneOnOff()
    {
        if (toggle.isOn == true)
        {
            for (int i = 0; i < planeArray.Length; i++)
            {
                if (i == LayerDropDown.dropdownValueNum)
                {
                    originToggleNum = i;
                    continue;
                }
                else
                {
                    planeArray[i].SetActive(true);
                    planeArray[i].GetComponent<MeshCollider>().enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < planeArray.Length; i++)
            {
                if (i == LayerDropDown.dropdownValueNum)
                    continue;
                else
                {
                    planeArray[i].GetComponent<MeshCollider>().enabled = true;
                    planeArray[i].SetActive(false);
                }
            }
        }
    }
}