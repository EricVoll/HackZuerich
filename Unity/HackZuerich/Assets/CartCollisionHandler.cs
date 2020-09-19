using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartCollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){

        if(disabled && Time.realtimeSinceStartup - disabledAt > 3){
            this.GetComponent<BoxCollider>().enabled = true;
            disabled = false;
        }
    }
    bool disabled = true;
    
    float disabledAt = 0;
    public WorkoutHandler parentWorkoutHandler;

    void OnCollisionEnter(Collision collisionData){
        var item = collisionData.gameObject.GetComponent<WorkoutItem>();
        var ingr = item.ingredient;
        Debug.Log("Col!" + Time.realtimeSinceStartup);
        parentWorkoutHandler.ReportIngredientCollected(ingr);
        
        //Disabled collider for 3 seconds
        this.GetComponent<BoxCollider>().enabled = false;
        disabled = true;
        disabledAt = Time.realtimeSinceStartup;
    }
}
