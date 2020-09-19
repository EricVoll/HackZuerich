using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class DataBase : MonoBehaviour
{
    public void Start(){
        instance = this;

        //test purpose
        currentRecipe = MockRecipeImport();
        Debug.Log("instance currentRecipe");
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
        string json = recipeMockTextFile.text;
        Debug.Log("found json");
        Recipe r = JsonConvert.DeserializeObject<Recipe>(CleanJson(json));
        if(r == null){
            Debug.Log("Recipe is null!");
        }
        return r;
    }

    private string CleanJson(string json){
        return json;
    }

    //Mocks
    public TextAsset recipeMockTextFile;
}
