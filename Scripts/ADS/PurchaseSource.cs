using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class PurchaseSource : MonoBehaviour
{
    public Text statusText;
    private int _health;

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "health_1") _health += 1;
        else if (product.definition.id == "health_10") _health += 10;

        DisplayHealth();
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Покупка товара " + product.definition.id + "не удалось, потому что " + reason);
    }

    private void DisplayHealth()
    {
        statusText.text = _health.ToString();
    }
}

