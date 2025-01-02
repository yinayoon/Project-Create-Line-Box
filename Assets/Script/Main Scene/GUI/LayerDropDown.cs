using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerDropDown : MonoBehaviour
{
    public Dropdown layerDropdown;
    public static int dropdownValueNum;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject root = new GameObject("Floor Print Layer Group");

        for (int i = 0; i < DecidedFigures.layerNum - 1; i++)
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            layerDropdown.options.Add(data);
        }

        for (int i = 0; i < DecidedFigures.layerNum; i++)
        {
            layerDropdown.options[i].text = (i + 1).ToString() + " Ãþ";
            GameObject leap = new GameObject((i + 1) + " Floor Cube Group");
            leap.transform.parent = root.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dropdownValueNum != layerDropdown.value)
            dropdownValueNum = layerDropdown.value;
    }
}
