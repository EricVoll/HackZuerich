using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckoutHandler : MonoBehaviour
{
    public static CheckoutHandler instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    CheckoutCart cart = new CheckoutCart();

    public void AddItem(Ingredient item)
    {
        cart.Items.Add(item);
        UpdateUI();
    }


    #region price label
    public TextMeshPro CostLabel;
    private void UpdateUI()
    {
        CostLabel.text = "for " + cart.TotalAmount.ToString("C");
    }
    #endregion

    #region StoryLineItem Exit

    public void ReportCheckoutBtnClicked()
    {
        Debug.Log("clicked me");
        this.GetComponent<StoryLineStep>().ReportStepFinished(new int[] { 3 });
    }

    #endregion

}
