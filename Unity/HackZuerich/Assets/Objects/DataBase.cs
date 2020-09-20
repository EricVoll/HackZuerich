using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System;

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
        InitRecipe r = JsonConvert.DeserializeObject<InitRecipe>(json);
        return r;
    }

    private string CleanJson(string json)
    {
        return json;
    }

    //Mocks
    public TextAsset recipeMockTextFile;
    public TextAsset initRecipeMockTextFile;

    public void GetReplacements(string ingredientId, Action<List<Ingredient>> Callback)
    {
        var list = new List<Ingredient>(){
                new Ingredient(){
                    Rating = 4,
                    name = "Test Replacement 1",
                    Cost = 12,
                    ScoreHealth = "A",
                    url = "https://image.migros.ch/original/5e6d8d8a9f2f12880b3d5f1f7ba8606cd14db6a4/agnesi-pesto-alla-genovese.png"
                },new Ingredient(){
                    Rating = 4,
                    name = "Test Replacement 2",
                    Cost = 5,
                    ScoreHealth = "V",
                    url = "https://image.migros.ch/original/5e6d8d8a9f2f12880b3d5f1f7ba8606cd14db6a4/agnesi-pesto-alla-genovese.png"
                },new Ingredient(){
                    Rating = 2,
                    name = "Test Replacement 3",
                    Cost = 100,
                    ScoreHealth = "C",
                    url = "https://image.migros.ch/original/5e6d8d8a9f2f12880b3d5f1f7ba8606cd14db6a4/agnesi-pesto-alla-genovese.png"
                }
            };
        Callback(list);
    }


}
