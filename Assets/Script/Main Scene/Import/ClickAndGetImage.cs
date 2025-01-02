﻿/*
 * Copyright 2016, Gregg Tavares.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     * Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above
 * copyright notice, this list of conditions and the following disclaimer
 * in the documentation and/or other materials provided with the
 * distribution.
 *     * Neither the name of Gregg Tavares. nor the names of its
 * contributors may be used to endorse or promote products derived from
 * this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using UnityEngine;
using System;
using System.Collections;

public class ClickAndGetImage : MonoBehaviour {

    public static int width = 0;
    public static int height = 0;

    public static float widthPoint = 0;
    public static float heightPoint = 0;

    public GameObject[] planeArray;

    private int originToggleNum;

    public void Start()
    {
        originToggleNum = 0;

        GameObject planeGroup = GameObject.Find("Floor Planes");

        if (planeGroup != null)
            return;

        planeArray = new GameObject[DecidedFigures.layerNum];
        for (int i = 0; i < DecidedFigures.layerNum; i++)
            planeArray[i] = planeGroup.transform.GetChild(i).gameObject;
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            // NOTE: gameObject.name MUST BE UNIQUE!!!!
            // GetImage.GetImageFromUserAsync(gameObject.name, "ReceiveImage");
        }
    }

    static string s_dataUrlPrefix = "data:image/png;base64,";
    public void ReceiveImage(string dataUrl)
    {
        if (dataUrl.StartsWith(s_dataUrlPrefix))
        {
            byte[] pngData = System.Convert.FromBase64String(dataUrl.Substring(s_dataUrlPrefix.Length));

            // Create a new Texture (or use some old one?)
            Texture2D tex = new Texture2D(1, 1); // does the size matter?
            if (tex.LoadImage(pngData))
            {
                Renderer renderer = GetComponent<Renderer>();

                renderer.material.mainTexture = tex;
            }
            else
            {
                Debug.LogError("could not decode image");
            }
        }
        else
        {
            Debug.LogError("Error getting image:" + dataUrl);
        }

        // 수정 요망
        GetImage.GetImageSize();
        width = GetImage.width;
        height = GetImage.height;

        // 드롭다운으로 선택된 이미지의 사이즈 변환
        for (int i = 0; i < planeArray.Length; i++)
        {
            if (i == LayerDropDown.dropdownValueNum)
            {
                planeArray[i].GetComponent<MeshCollider>().enabled = true;

                ReviseImageSize(i);
                continue;
            }
            else
            {
                planeArray[i].GetComponent<MeshCollider>().enabled = false;
            }
        }
    }

    // 이미지의 사이즈 변환 함수
    public void ReviseImageSize(int idx)
    {
        widthPoint = float.Parse(width.ToString()) / 50;
        heightPoint = float.Parse(height.ToString()) / 50;

        planeArray[idx].gameObject.transform.localScale = new Vector3(widthPoint, 1, heightPoint);
    }
}