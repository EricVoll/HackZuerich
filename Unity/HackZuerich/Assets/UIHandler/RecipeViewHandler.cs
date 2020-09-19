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
        if (DataBase.instance.currentRecipe == null)
        {
            DataBase.instance.RecipeReceived = ReportRecipeReceived;
        }
        else
        {
            ingredients = DataBase.instance.currentRecipe;
            ProcessRecipe();
        }
    }

    private void ReportRecipeReceived()
    {
        ingredients = DataBase.instance.currentRecipe;
        ProcessRecipe();
    }

    List<Ingredient> ingredients;

    private void ProcessRecipe()
    {
        //Display all ingredients
        foreach (var ingr in ingredients.Skip(2).Take(5))
        {
            GameObject ingredientGO = Instantiate(IngredientPrefab);

            //Make up data
            if (ingr.Cost == 0)
                ingr.Cost = Random.Range(0.5f, 50f);

            ingr.ScoreHealth = new string[] { "F", "E", "D", "C", "B", "A" }[Random.Range(0, 6)];
            ingr.Rating = Random.Range(0, 6);
            
            ingredientGO.GetComponent<IngredientHandler>().SetIngredient(ingr);
            ingredientGO.transform.parent = IngredientsContainer.transform;
        }

        //Update collection placement after adding all items
        IngredientsContainer.GetComponent<GridObjectCollection>().UpdateCollection();
    }
}
