using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoodOptionHandler : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    // Start is called before the first frame update
    public void SetFoodOption(FoodOption option)
    {
        if(meshRenderer ==  null){
            Debug.LogError("MeshRenderer is null!");
        }

        meshRenderer.material = new Material(Shader.Find("Standard"));
        var texture = option.GetTexture();
        meshRenderer.material.SetTexture("_MainTex", texture);
        Debug.Log("Set");
    }

    void Awake(){
        SetFoodOption(new FoodOption());
    }

}
