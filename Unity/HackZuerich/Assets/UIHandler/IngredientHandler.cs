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
        textMeshPro.text = ingredient.name;
        SetState(true);
        SetGrade(ingredient.ScoreHealth);
        SetRating(ingredient.Rating);
        SetCost(ingredient.Cost);
        StartCoroutine(SetImage());
        if (!String.IsNullOrEmpty(ingredient.brand))
        {
            BrandRenderer.gameObject.SetActive(true);
            StartCoroutine(SetBrand());
        }
        else
        {
            BrandRenderer.gameObject.SetActive(false);
        }

        UpdateDetails();
    }

    private Ingredient ingredient;

    public void IngreadientCheckBoxClicked()
    {
        ingredient.IsActive = !ingredient.IsActive;

        if (ingredient.IsActive)
        {
            CheckoutHandler.instance.AddItem(ingredient);
        }
        else
        {
            CheckoutHandler.instance.RemoveItem(ingredient);
        }
    }

    public void SetState(bool state)
    {
    }


    #region Scores


    public List<Material> materials;
    public GameObject HealthinessBackground;
    public TextMeshPro HealthinessText;



    //Sets a grade between 0/10
    private void SetGrade(string grade)
    {
        int index = Array.FindIndex(new string[] { "F", "E", "D", "C", "B", "A" }, x => x == grade);
        Debug.Log("Found index " + index);
        HealthinessBackground.GetComponent<MeshRenderer>().material = materials[index];
        HealthinessText.text = grade.ToString();
    }


    #endregion

    #region Cost

    public TextMeshPro CostText;

    private void SetCost(double cost)
    {
        string text = cost.ToString("C");
        CostText.text = text;
    }

    #endregion

    #region Starts
    public GameObject[] starsGO;
    private void SetRating(int stars)
    {
        for (int i = 0; i < stars; i++)
        {
            starsGO[i].SetActive(true);

        }
        for (int i = stars; i < 5; i++)
        {
            starsGO[i].SetActive(false);
        }
    }
    #endregion

    public MeshRenderer ImageRenderer;

    IEnumerator SetImage()
    {
        using (WWW www = new WWW(ingredient.url))
        {
            // Wait for download to complete
            yield return www;

            float factor = (float)www.texture.width / www.texture.height;
            float factory = 1f;

            if (factor > 1)
            {
                factory = 1 / factor;
                factor = 1f;
            }
            ImageRenderer.gameObject.transform.localScale = new Vector3(
                ImageRenderer.gameObject.transform.localScale.x * factor,
                ImageRenderer.gameObject.transform.localScale.y * factory,
                ImageRenderer.gameObject.transform.localScale.z);
            // assign texture
            ImageRenderer.material.mainTexture = www.texture;
        }
    }

    public MeshRenderer BrandRenderer;

    IEnumerator SetBrand()
    {
        using (WWW www = new WWW(ingredient.brand))
        {
            // Wait for download to complete
            yield return www;

            float factor = (float)www.texture.width / www.texture.height;
            float factory = 1f;

            if (factor > 1)
            {
                factory = 1 / factor;
                factor = 1f;
            }
            BrandRenderer.gameObject.transform.localScale = new Vector3(
                BrandRenderer.gameObject.transform.localScale.x * factor,
                BrandRenderer.gameObject.transform.localScale.y * factory,
                BrandRenderer.gameObject.transform.localScale.z);
            // assign texture
            BrandRenderer.material.mainTexture = www.texture;
        }
    }


    #region Details

    private void UpdateDetails()
    {
        if (ingredient.nutrient_levels != null)
        {
            string nutr = "Nutrients:\n";
            var n = ingredient.nutrient_levels;
            if (n.fat == "low" || n.fat == "high")
            {
                nutr += $"- {n.fat} fat\n";
            }
            if (n.salt == "low" || n.salt == "high")
            {
                nutr += $"- {n.salt} salt\n";
            }
            if (n.saturated_fat == "low" || n.saturated_fat == "high")
            {
                nutr += $"- {n.saturated_fat} saturated fats\n";
            }
            if (n.sugars == "low" || n.sugars == "high")
            {
                nutr += $"- {n.sugars} sugar\n";
            }
            if (nutr != "Nutrients:\n")
            {
                NutrientData.text = nutr;
            }
            else
            {
                nutr += "All good :)'";
                NutrientData.text = nutr;
            }
        }
        if (ingredient.allergens.Count > 0)
        {
            string txt = "Allergens:\n";
            for (int i = 0; i < ingredient.allergens.Count; i++)
            {
                Debug.Log(ingredient.allergens[i]);
                txt += $" - {ingredient.allergens[i]}\n";
            }
            if (txt != "Allergens:\n")
            {
                MeshAllergenes.text = txt;
            }
            else
            {
                txt += "All good :)";
                MeshAllergenes.text = txt;
            }

        }

        SwissMadeIcon.SetActive(ingredient.origin == "Switzerland");
        Mesh1.text = Math.Round(ingredient.weight, 2).ToString() + " kg";
        Mesh2.text = ingredient.kcal + " kcal";
    }

    public GameObject DetailView;
    private bool detailState = false;
    public void BtnClicked()
    {
        detailState = !detailState;
        DetailView.SetActive(detailState);

        if(detailState == true){
            DataBase.instance.GetReplacements(ingredient.id, Callback);
        }
    }

    public TextMeshPro NutrientData;
    public GameObject SwissMadeIcon;

    public TextMeshPro Mesh1;
    public TextMeshPro Mesh2;
    public TextMeshPro MeshAllergenes;

    #endregion

    #region Replacements

    List<Ingredient> replacements;

    public void Opt1(){
        if(replacements == null || replacements.Count == 0){
            return;
        }
        SetIngredient(replacements[0]);
    }
    public void Opt2(){
        if(replacements == null || replacements.Count == 0){
            return;
        }
        SetIngredient(replacements[1]);
    }
    public void Opt3(){
        if(replacements == null || replacements.Count == 0){
            return;
        }
        SetIngredient(replacements[2]);
    }

    void Callback(List<Ingredient> list){
        replacements = list;
    }

    #endregion
}
