using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLineHandler : MonoBehaviour
{
    public List<GameObject> Prefabs;

    List<GameObject> LivingGameObjects = new List<GameObject>();

    public void Awake(){
        LaunchStep(0);
    }

    private void LaunchStep(int step){
        for (int i = LivingGameObjects.Count - 1; i >= 0 ; i--)
        {
            GameObject.Destroy(LivingGameObjects[i]);
        }

        if(Prefabs.Count > step){
            GameObject newGameObject = Instantiate(Prefabs[step]);
            LivingGameObjects.Add(newGameObject);
            newGameObject.GetComponent<StoryLineStep>().SetCallBack(LaunchStep);
        }
        else{
            Debug.LogError("Requested Step was too high. Index out of range");
        }
    }
}
