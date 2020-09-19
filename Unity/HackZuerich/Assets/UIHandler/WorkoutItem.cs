using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public Ingredient ingredient;

    public Material[] materials;

    private float ScoreHealthToInt(string health){
        switch(health){
            case "A":
                return .001f;
            case "B":
                return .0015f;
            case "C":
                return .002f;
            case "D":
                return .0025f;
            case "E":
                return .003f;
            case "F":
                return .0035f;
        }
        return 0.001f;
    }

    Dictionary<string, int> dict = new Dictionary<string, int>(){
      { "A" , 0 },
      { "B" , 1 },
      { "C" , 2 },
      { "D" , 3 },
      { "E" , 4 },
      { "F" , 5 },
    };

    public void Setup(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        Vector3 speed = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized;
        speed = speed * ScoreHealthToInt(ingredient.ScoreHealth);
        this.GetComponent<Rigidbody>().velocity = speed;
        this.GetComponent<MeshRenderer>().material = materials[dict[ingredient.ScoreHealth]];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
