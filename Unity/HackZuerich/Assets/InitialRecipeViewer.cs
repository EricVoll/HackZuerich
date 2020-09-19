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

}
