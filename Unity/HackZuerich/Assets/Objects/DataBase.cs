using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public void Start(){
        instance = this;
    }

    public static DataBase instance;
    
    public void ReportFoodSelection(string id){
        Debug.Log("Requesting item");
    }
}
