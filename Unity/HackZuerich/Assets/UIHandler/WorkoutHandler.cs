using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WorkoutHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LaunchItems();
    }
    
    public GameObject WorkoutItemPrefab;

    private void LaunchItems(){
        foreach(var ingredient in DataBase.instance.currentRecipe){
            var item = GetModel(ingredient.name);
            item.GetComponent<WorkoutItem>().Setup(ingredient);
            item.transform.parent = this.transform;
        }

        progressBarText.text = "Collected 0/" + maxNr;
    }

    private GameObject GetModel(string name){
        Debug.Log(name + " requested");
        if(map.Keys.Any(x => x.Contains(name.ToLower()))){
            Debug.Log("returning special model");
            //item exists. find it.
            string[] key = map.Keys.First(x => x.Contains(name.ToLower()));
            int kex = map[key];
            Debug.Log("key returned: " + kex);
            GameObject go = Instantiate(Models[kex]);
            GameObject parent = Instantiate(WorkoutItemPrefab);
            GameObject.Destroy(parent.GetComponent<MeshRenderer>());
            go.transform.parent = parent.transform;
            Debug.Log("-------------------------------------------------");
            return parent;
        }
        else{
            return Instantiate(WorkoutItemPrefab);
        }
    }

    #region Collection

    public GameObject colliderGO;

    public int maxNr = 5;
    List<Ingredient> collectedIngredients = new List<Ingredient>();

    public void ReportIngredientCollected(Ingredient ingredient){
        if(collectedIngredients.Any(x => x.name == ingredient.name)){
            return;
        }

        collectedIngredients.Add(ingredient);
        Debug.Log($"Ingredient {ingredient.name} collected");

        Vector3 scale = progressBar.localScale;
        scale.x = (float)collectedIngredients.Count / maxNr;
        progressBar.localScale = scale;
        progressBarText.text = $"Collected {collectedIngredients.Count}/{maxNr} \n+ {collectedIngredients.Count*10} Cumulus Points extra!";

        if(collectedIngredients.Count == 1){
            motivatorText.text = "The best things in life are actually really expensive.";
        }
        else if(collectedIngredients.Count == 2){
            motivatorText.text = "You can do it!";
        }
        else if(collectedIngredients.Count == 3){
            motivatorText.text = "If at first you don’t succeed, then skydiving definitely isn’t for you.";
        }

        if(collectedIngredients.Count == maxNr){
            ReportFinished();
        }
    }

    public Transform progressBar;
    public TextMeshPro progressBarText;
    public TextMeshPro motivatorText;

    #endregion

    public GameObject FinishedScreen1;
    public GameObject FinishedScreen2;
    public GameObject FinishedScreen3;

    
    private void ReportFinished(){
        ProfileScripts.instance.AddCal(collectedIngredients.Sum(x => x.kcal));
        FinishedScreen1.SetActive(true);

        StartCoroutine(Waiter());
    }

    IEnumerator Waiter(){
        yield return new WaitForSeconds(6);
        FinishedScreen1.SetActive(false);
        FinishedScreen2.SetActive(true);
        yield return new WaitForSeconds(8);
        FinishedScreen2.SetActive(false);
        FinishedScreen3.SetActive(true);
    }



    
    public GameObject[] Models;

    public Dictionary<string[], int> map = new Dictionary<string[], int>(){
        {new []{"paprika"}, 0},
        {new []{"zwiebel"}, 1},
        {new []{"tomaten"}, 2},
        {new []{"salat", "zuckerhut"}, 3},
        {new []{"reis"}, 4},
        {new []{"fleisch", "wurst", "meat"}, 5},
        {new []{"wein", "essig", "honig"}, 6},
        {new []{"salz", "curry", "pfeffer"}, 7},
        {new []{"kaese", "käse"}, 8},
        {new []{"blume"}, 9},
        
        };


}
