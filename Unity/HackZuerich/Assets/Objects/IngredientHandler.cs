using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientHandler : MonoBehaviour
{

    //References
    public TextMeshPro textMeshPro;
    public Microsoft.MixedReality.Toolkit.UI.PressableButtonHoloLens2 Checkbox;
    public GameObject IconOn;
    public GameObject IconOff;

    public void SetIngredient(Ingredient ingredient){
        textMeshPro.text = ingredient.name.singular;
        SetState(true);
        SetGrade((int)(Time.realtimeSinceStartup*1000)%11);
    }

    private Ingredient ingredient;

    public void IngreadientCheckBoxClicked(){
        ingredient.IsActive = !ingredient.IsActive;
    }

    public void SetState(bool state){
    }


    public List<Material> materials;
    public GameObject GradeBackground;
    public TextMeshPro TextMeshHealthGrade;
    //Sets a grade between 0/10
    private void SetGrade(int grade){
        int index = (int)grade/2;
        GradeBackground.GetComponent<MeshRenderer>().material = materials[index];
        TextMeshHealthGrade.text = grade.ToString();
    }

}
