using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;
public class FoodSelectionMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject FoodOptionButtonPrefab;
    public GameObject FoodOptionsContainer;

    void Awake()
    {
        //Replace this with Rest Call later
        Options = GetMockList();

        foreach (var opt in Options)
        {
            GameObject btn = GameObject.Instantiate(FoodOptionButtonPrefab);
            btn.transform.parent = FoodOptionsContainer.transform;
            btn.GetComponent<FoodOptionHandler>().SetFoodOption(opt, ReportButtonPressed);
        }

        FoodOptionsContainer.GetComponent<Microsoft.MixedReality.Toolkit.Utilities.GridObjectCollection>().UpdateCollection();
    }

    private void ReportButtonPressed(string optionId)
    {
        DataBase.instance.ReportFoodSelection(optionId);
        this.GetComponent<StoryLineStep>().ReportStepFinished(new[] { 1, 2 });
    }

    public List<FoodOption> Options = new List<FoodOption>();

    private List<FoodOption> GetMockList()
    {
        List<FoodOption> options = new List<FoodOption>(){
            new FoodOption(){id = "SomeFood0"},
            new FoodOption(){id = "SomeFood1"},
            new FoodOption(){id = "SomeFood2"},
            new FoodOption(){id = "SomeFood3"},
            new FoodOption(){id = "SomeFood4"},
            new FoodOption(){id = "SomeFood5"},
            new FoodOption(){id = "SomeFood6"},
            new FoodOption(){id = "SomeFood7"},
            new FoodOption(){id = "SomeFood8"},
        };
        return options;
    }
}
