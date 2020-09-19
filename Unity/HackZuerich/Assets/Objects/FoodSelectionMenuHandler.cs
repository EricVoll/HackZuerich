using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSelectionMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject FoodOptionButtonPrefab;
    public GameObject FoodOptionsContainer;
    
    void Awake(){
        //Replace this with Rest Call later
        Options = GetMockList();

        foreach(var opt in Options){
            GameObject btn = GameObject.Instantiate(FoodOptionButtonPrefab);
            btn.transform.parent = FoodOptionsContainer.transform;
            //btn.GetComponent<FoodOptionHandler>().SetFoodOption(opt);
        }

        FoodOptionsContainer.GetComponent<Microsoft.MixedReality.Toolkit.Utilities.GridObjectCollection>().UpdateCollection();
    }

    public List<FoodOption> Options = new List<FoodOption>();

    private List<FoodOption> GetMockList(){
        List<FoodOption> options = new List<FoodOption>(){
            new FoodOption(),
            new FoodOption(),
            new FoodOption(),
            new FoodOption(),
            new FoodOption(),
            new FoodOption(),
            new FoodOption(),
            new FoodOption(),
            new FoodOption(),
        };
        return options;
    }
}
