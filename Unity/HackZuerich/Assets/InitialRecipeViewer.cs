using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRecipeViewer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(DataBase.instance.initRecipe != null){
            initRecipe = DataBase.instance.initRecipe;
            UpdateUI();
        }
        else{
            DataBase.instance.InitRecipeReceived = ReportInitRecipeLoaded;
        }
    }

    private void ReportInitRecipeLoaded(){
        initRecipe = DataBase.instance.initRecipe;
        UpdateUI();
    }

    InitRecipe initRecipe;

    private void UpdateUI(){
        
    }



    #region EndStoryLine

    public void ReportEndClick(){
        DataBase.instance.ReportRecipeAproval();
        this.GetComponent<StoryLineStep>().ReportStepFinished(new int[]{2, 3});
    }
        
    #endregion
}
