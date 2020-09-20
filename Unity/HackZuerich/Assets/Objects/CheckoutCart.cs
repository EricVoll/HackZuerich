using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckoutCart
{
    public CheckoutCart()
    {
        instance = this;
    }
    public static CheckoutCart instance;
    public double TotalAmount => Items.Sum(x => x.Cost);

    public List<Ingredient> Items {get;set;} = new List<Ingredient>();
}
