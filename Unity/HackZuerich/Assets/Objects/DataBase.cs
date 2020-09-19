using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class DataBase : MonoBehaviour
{
    public void Start()
    {
        instance = this;

        //test purpose
        currentRecipe = MockRecipeImport();
        Debug.Log("instance currentRecipe");
    }

    public static DataBase instance;

    public List<Ingredient> currentRecipe;
    public InitRecipe initRecipe;
    public System.Action RecipeReceived;
    public System.Action InitRecipeReceived;



    public void ReportFoodSelection(string id)
    {
        Debug.Log("Requesting item");
        initRecipe = MockInitRecipe();
        RecipeReceived?.Invoke();
    }

    public void ReportRecipeAproval()
    {
        currentRecipe = MockRecipeImport();
        RecipeReceived?.Invoke();
    }


    private List<Ingredient> MockRecipeImport()
    {
        string json = recipeMockTextFile.text;
        Debug.Log("found json");
        List<Ingredient> r = JsonConvert.DeserializeObject<List<Ingredient>>(CleanJson(json));
        if (r == null)
        {
            Debug.Log("Recipe is null!");
        }
        return r;
    }

    private InitRecipe MockInitRecipe()
    {
        string json = initRecipeMockTextFile.text;
        InitRecipe r = new InitRecipe() { MyArray = JsonConvert.DeserializeObject<List<MyArray>>(json) };
        return r;
    }

    private string CleanJson(string json)
    {
        return json;
    }

    //Mocks
    public TextAsset recipeMockTextFile;
    public TextAsset initRecipeMockTextFile;
}
