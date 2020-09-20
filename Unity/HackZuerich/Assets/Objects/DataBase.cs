using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System;
using UnityEngine.Networking;

public class DataBase : MonoBehaviour
{
    public void Start()
    {
        instance = this;

        //test purpose
        Debug.Log("instance currentRecipe");
    }

    public static DataBase instance;

    public List<Ingredient> currentRecipe;
    public InitRecipe initRecipe;
    public System.Action RecipeReceived;
    public System.Action InitRecipeReceived;


    private string id;
    public void ReportFoodSelection(string id)
    {
        this.id = id;
        Debug.Log("Requesting for id " + id);
        var com = new RestCommunicator();
        StartCoroutine(GetInitRecipe("https://mrshopper.azurewebsites.net/get_recipe?recipe_id=" + id));

        //Debug.Log("Requesting item");
        //initRecipe = MockInitRecipe();
        //RecipeReceived?.Invoke();
    }

    private IEnumerator GetInitRecipe(string url)
    {
        Debug.Log("Sending to " + url);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    initRecipe = JsonConvert.DeserializeObject<InitRecipe>(jsonResult);

                    InitRecipeReceived?.Invoke();
                    Debug.Log(jsonResult);
                }
            }
        }
    }

    private IEnumerator GetIngredients(string url)
    {
        Debug.Log("Sending to " + url);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    currentRecipe = JsonConvert.DeserializeObject<List<Ingredient>>(jsonResult);

                    RecipeReceived?.Invoke();
                    Debug.Log(jsonResult);
                }
            }
        }
    }

    private IEnumerator GetSubstitute(string url)
    {
        Debug.Log("Sending to " + url);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    Debug.Log(jsonResult);
                    var list = JsonConvert.DeserializeObject<Substitute>(jsonResult);

                    List<Ingredient> ingredients = new List<Ingredient>(){
                        list.cheapest,
                        list.personal_best,
                        list.best_grade
                    };

                    SubstituteCallBack(ingredients);
                }
            }
        }
    }



    public void ReportRecipeAproval()
    {
        StartCoroutine(GetIngredients("https://mrshopper.azurewebsites.net/get_ingredients?recipe_id=" + this.id));
        //currentRecipe = MockRecipeImport();
        //RecipeReceived?.Invoke();
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

    Action<List<Ingredient>> SubstituteCallBack;
    public void GetReplacements(string ingredientId, Action<List<Ingredient>> Callback)
    {
        SubstituteCallBack = Callback;
        StartCoroutine(GetSubstitute("https://mrshopper.azurewebsites.net/get_substitute?ingredient_id=" + ingredientId));
        /*var list = new List<Ingredient>(){
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
        Callback(list);*/
    }


}

class Substitute
{
    public Ingredient cheapest { get; set; }
    public Ingredient best_grade { get; set; }
    public Ingredient personal_best { get; set; }

}
