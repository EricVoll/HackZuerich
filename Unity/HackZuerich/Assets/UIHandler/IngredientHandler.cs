using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class IngredientHandler : MonoBehaviour
{

    //References
    public TextMeshPro textMeshPro;
    public Microsoft.MixedReality.Toolkit.UI.PressableButtonHoloLens2 Checkbox;
    public GameObject IconOn;
    public GameObject IconOff;

    public void SetIngredient(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        textMeshPro.text = ingredient.name.singular;
        SetState(true);
        SetGrade(ingredient.ScoreHealth, nrType.Health);
        SetGrade(ingredient.ScoreCarbon, nrType.CarbonFootPrint);
        SetCost(ingredient.Cost);
    }

    private Ingredient ingredient;

    public void IngreadientCheckBoxClicked()
    {
        ingredient.IsActive = !ingredient.IsActive;
        CheckoutHandler.instance.AddItem(ingredient);
    }

    public void SetState(bool state)
    {
    }


    #region Scores


    public List<Material> materials;
    public GameObject HealthinessBackground;
    public TextMeshPro HealthinessText;

    public GameObject CarbonfootprintBackground;
    public TextMeshPro CarbonfootprintText;

    enum nrType { Health, CarbonFootPrint }

    //Sets a grade between 0/10
    private void SetGrade(string grade, nrType type)
    {
        int index = Array.FindIndex(new string[] { "F", "E", "D", "C", "B", "A" }, x => x == grade);
        Debug.Log("Found index " + index);
        if (type == nrType.Health)
        {
            HealthinessBackground.GetComponent<MeshRenderer>().material = materials[index];
            HealthinessText.text = grade.ToString();
        }
        else if (type == nrType.CarbonFootPrint)
        {
            CarbonfootprintBackground.GetComponent<MeshRenderer>().material = materials[index];
            CarbonfootprintText.text = grade.ToString();
        }
    }


    #endregion

    #region Cost

    public TextMeshPro CostText;

    private void SetCost(double cost){
        string text = cost.ToString("C");
        CostText.text = text;
    }

    #endregion
}
