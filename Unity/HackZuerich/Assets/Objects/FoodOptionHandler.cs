using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
public class FoodOptionHandler : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Action<string> ReportPressed;
    FoodOption option;

    // Start is called before the first frame update
    public void SetFoodOption(FoodOption option, Action<string> Callback)
    {
        ReportPressed = Callback;
        this.option = option;

        if(meshRenderer ==  null){
            Debug.LogError("MeshRenderer is null!");
        }

        meshRenderer.material = new Material(Shader.Find("Standard"));
        var texture = option.GetTexture();
        meshRenderer.material.SetTexture("_MainTex", texture);
        Debug.Log("Set");
    }



    public void BtnPressed(){
        Debug.Log("Buttonw as pressed");
        ReportPressed?.Invoke(option.id);
    }

}
