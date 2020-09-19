using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public class Name
{
    public string singular { get; set; }
    public string plural { get; set; }
}

public class Ingredient
{
    public string id { get; set; }
    public int ingredient_id {get;set;}
    public string name { get; set; }

    //custom ones
    public bool IsActive {get; set;}
    [JsonProperty("price")]
    public double Cost {get;set;}
    [JsonProperty("health_score")]
    public string ScoreHealth {get;set;}
    public double ratings {get;set;}
    public int Rating {get { return (int)ratings; } set {}} 
    public int kcal {get;set;}
    [JsonProperty("image")]
    public string url {get;set;}
    public string brand {get;set;}
    public string origin {get;set;}
    public double weight {get;set;}
    public NutrientLevels nutrient_levels { get; set; } 
    public List<string> allergens {get;set;}
}
    public class NutrientLevels    {
        public string saturated_fat { get; set; } 
        public string sugars { get; set; } 
        public string fat { get; set; } 
        public string salt { get; set; } 
    }

    public class Te {
        public Ingredient[] data {get;set;}
    }