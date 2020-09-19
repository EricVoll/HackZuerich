using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class DataBase : MonoBehaviour
{
    public void Start(){
        instance = this;
    }

    public static DataBase instance;

    public Recipe currentRecipe;
    public System.Action RecipeReceived;

    public void ReportFoodSelection(string id){
        Debug.Log("Requesting item");
        currentRecipe = MockRecipeImport();
        RecipeReceived?.Invoke();
    }

    private Recipe MockRecipeImport(){
        TextAsset bindata = Resources.Load("Texture") as TextAsset;
        string json = bindata.text;
        Debug.Log("found json");
        Recipe r = JsonConvert.DeserializeObject<Recipe>(json);
        if(r == null){
            Debug.Log("Recipe is null!");
        }
        return r;
    }
}
