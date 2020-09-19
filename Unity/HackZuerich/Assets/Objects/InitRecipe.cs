using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class Nutrients    {
        public int calories { get; set; } 
        public int carbohydrates { get; set; } 
        public double carbohydrates_percent { get; set; } 
        public int fat { get; set; } 
        public double fat_percent { get; set; } 
        public int kilojoule { get; set; } 
        public int proteins { get; set; } 
        public double proteins_percent { get; set; } 
    }


    public class MyArray    {
        public string language { get; set; } 
        public string title { get; set; } 
        public int duration { get; set; } 
        public Nutrients nutrients { get; set; } 
        public List<string> steps { get; set; } 
        public List<Ingredient> ingredients { get; set; } 
        public string image { get; set; } 
    }

    public class InitRecipe    {
        public List<MyArray> MyArray { get; set; } 
    }


