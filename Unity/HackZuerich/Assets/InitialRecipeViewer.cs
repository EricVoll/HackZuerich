using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InitialRecipeViewer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DataBase.instance.initRecipe != null)
        {
            initRecipe = DataBase.instance.initRecipe;
            UpdateUI();
        }
        else
        {
            DataBase.instance.InitRecipeReceived = ReportInitRecipeLoaded;
        }
    }

    private void ReportInitRecipeLoaded()
    {
        initRecipe = DataBase.instance.initRecipe;
        UpdateUI();
    }

    InitRecipe initRecipe;

    private void UpdateUI()
    {
        //Load Image
        StartCoroutine(LoadImage());

        Name.text = "Your recipe: " + initRecipe.title;
        Duration.text = ((int)initRecipe.duration / 60).ToString() + " h " + (initRecipe.duration % 60).ToString() + " min";

        if (languageMap.ContainsKey(initRecipe.language))
        {
            Language.text = languageMap[initRecipe.language];
        }
        else
        {
            Language.text = initRecipe.language;
        }

        if (initRecipe.steps.Count > 0)
            InstructionsText.text = "Schritt 1: " + initRecipe.steps[0];
        else
            InstructionsText.text = "No instructions";

        TextMeshes[0].text = initRecipe.nutrients.calories.ToString();
        TextMeshes[1].text = initRecipe.nutrients.carbohydrates.ToString();
        TextMeshes[2].text = initRecipe.nutrients.carbohydrates_percent + " %";
        TextMeshes[3].text = initRecipe.nutrients.fat.ToString();
        TextMeshes[4].text = initRecipe.nutrients.fat_percent +  " %";
        TextMeshes[5].text = initRecipe.nutrients.kilojoule + " kJ";
        TextMeshes[6].text = initRecipe.nutrients.proteins.ToString();
        TextMeshes[7].text = initRecipe.nutrients.proteins_percent +" %";
    }

    Dictionary<string, string> languageMap = new Dictionary<string, string>(){
        {"de", "German"},
        {"fr", "French"},
        {"it", "Italian"},
        {"en", "English"}
    };


    #region EndStoryLine

    public void ReportEndClick()
    {
        DataBase.instance.ReportRecipeAproval();
        this.GetComponent<StoryLineStep>().ReportStepFinished(new int[] { 2, 3 });
    }

    #endregion


    #region Image

    IEnumerator LoadImage()
    {
        using (WWW www = new WWW(initRecipe.image))
        {
            // Wait for download to complete
            yield return www;

            // assign texture
            Image.material.mainTexture = www.texture;
        }
    }

    public MeshRenderer Image;

    #endregion

    #region Text Fields

    public TextMeshPro Name;
    public TextMeshPro Duration;
    public TextMeshPro Language;

    #endregion

    #region Instructions

    public GameObject ToolTip;
    public TextMeshPro InstructionsText;
    private int currentIndex = 0;
    private bool TooltipState = false;
    public void InstructionBtnClicked()
    {
        TooltipState = !TooltipState;
        ToolTip.SetActive(TooltipState);
    }

    public void NextInstr()
    {
        if(initRecipe.steps.Count > currentIndex+1){
            currentIndex++;
        }
        SetText(currentIndex);
    }
    public void PrevInstr()
    {
        if(currentIndex-1 >= 0){
            currentIndex--;
        }
        SetText(currentIndex);
    }
    private void SetText(int index){
        InstructionsText.text = $"Step {currentIndex+1}: {initRecipe.steps[currentIndex]}";
    }

    #endregion

    #region Nutrients
    bool NutrientsActive = false;
    public void ReportNutrientsClicked(){
        NutrientsActive = !NutrientsActive;
        NutrientsPanel.SetActive(NutrientsActive);
    }
    public GameObject NutrientsPanel;

    public TextMeshPro[] TextMeshes;


        
    #endregion

}
