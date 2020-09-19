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
            new FoodOption(){id = "penne-al-pesto-mit-zucchetti", name="Pasta", url = "https://recipeimages.migros.ch/crop/v-w-4000-h-1702-a-center_center/684e2a07195f6e3d6b92723002b109d9c8db5661/penne-al-pesto-mit-zucchetti-0-47-20.jpg"},
            new FoodOption(){id = "schoggitarte-mit-erdbeeren",name="Tarte", url ="https://images.eatsmarter.de/sites/default/files/styles/576x432/public/schokoladentorte-mit-erdbeeren-218475.jpg"},
            new FoodOption(){id = "pizza-mit-cherrytomaten",name="Pizza", url = "https://ww2.bettybossi.ch/static/rezepte/x/bb_mcco170508_0008a_x.jpg"},
            new FoodOption(){id = "schokoladenkuchen-mit-fruchtsalat",name="Cake", url="https://thumbs.dreamstime.com/z/schokoladenkuchen-mit-fruchtsalat-79100434.jpg"},
            new FoodOption(){id = "bunte-salat-bowl",name="Salad", url ="https://i2.wp.com/kuechencottage.de/wp-content/uploads/2018/08/Salad-Bowl-3_wm.jpg?w=1600&ssl=1"},
            new FoodOption(){id = "SomeFood5", name="Demo1",url = "https://www.daskochrezept.de/sites/default/files/styles/43l/public/rezepte/2010/3/Pizza-Margherita-4bb0ecf864075.jpg?itok=DTfMNXj9"},
            new FoodOption(){id = "SomeFood6", name="Demo2",url = "https://www.daskochrezept.de/sites/default/files/styles/43l/public/rezepte/2010/3/Pizza-Margherita-4bb0ecf864075.jpg?itok=DTfMNXj9"},
            new FoodOption(){id = "SomeFood7", name="Demo3",url = "https://www.daskochrezept.de/sites/default/files/styles/43l/public/rezepte/2010/3/Pizza-Margherita-4bb0ecf864075.jpg?itok=DTfMNXj9"},
            new FoodOption(){id = "SomeFood8", name="Demo4",url = "https://www.daskochrezept.de/sites/default/files/styles/43l/public/rezepte/2010/3/Pizza-Margherita-4bb0ecf864075.jpg?itok=DTfMNXj9"},
        };
        return options;
    }
}
