using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LaunchItems();
    }
    
    public GameObject WorkoutItemPrefab;

    private void LaunchItems(){
        foreach(var ingredient in DataBase.instance.currentRecipe.data.ingredients){
            var item = Instantiate(WorkoutItemPrefab);
            item.GetComponent<WorkoutItem>().Setup(ingredient);
            item.transform.parent = this.transform;
        }
    }
    
}
