using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using TMPro;
public class FoodOptionHandler : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public TextMeshPro foodOptionNameText;
    public Action<string> ReportPressed;
    FoodOption option;

    // Start is called before the first frame update
    public void SetFoodOption(FoodOption option, Action<string> Callback)
    {
        ReportPressed = Callback;
        this.option = option;

        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer is null!");
        }

        foodOptionNameText.text = option.name;
/*
        meshRenderer.material = new Material(Shader.Find("Standard"));
        var texture = option.GetTexture();
        meshRenderer.material.SetTexture("_MainTex", texture);
  
  */

        StartCoroutine(SetImage());
    }


    IEnumerator SetImage()
    {
        // Start a download of the given URL
        using (WWW www = new WWW(option.url))
        {
            // Wait for download to complete
            yield return www;

            // assign texture
            meshRenderer.material.mainTexture = www.texture;
        }

    }

    public void BtnPressed()
    {
        ReportPressed?.Invoke(option.id);
    }

}
