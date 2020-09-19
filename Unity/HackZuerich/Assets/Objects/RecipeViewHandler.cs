using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class RecipeViewHandler : MonoBehaviour
{

    //Prefabs
    public GameObject IngredientPrefab;

    //Container references
    public GameObject IngredientsContainer;

    // Start is called before the first frame update
    void Awake()
    {
        if(DataBase.instance.currentRecipe == null){
            DataBase.instance.RecipeReceived = ReportRecipeReceived;
        }
        else{
            Debug.Log("Loaded recipe");
            recipe = DataBase.instance.currentRecipe;
            ProcessRecipe();
        }
    }

    private void ReportRecipeReceived(){
        recipe = DataBase.instance.currentRecipe;
        ProcessRecipe();
    }

    Recipe recipe;

    private void ProcessRecipe(){

        //Display all ingredients
        foreach(var ingr in recipe.data.ingredients.Take(5)){
            GameObject ingredientGO = Instantiate(IngredientPrefab);
            ingredientGO.GetComponent<IngredientHandler>().SetIngredient(ingr);
            ingredientGO.transform.parent = IngredientsContainer.transform;
        }

        //Update collection placement after adding all items
        IngredientsContainer.GetComponent<GridObjectCollection>().UpdateCollection();
    }
}
