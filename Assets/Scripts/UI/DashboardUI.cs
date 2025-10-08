using TMPro;
using UnityEngine;

public class DashboardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainingBudgetText;

    // Debug
    public bool setRemaining = false;

    private void Start()
    {
        remainingBudgetText.text = "Remaining: € ";
    }

    private void Update()
    {
        if(setRemaining)
            remainingBudgetText.text = $"Remaining: € {BudgetManager.Instance.GetRemainingBudget().ToString("F2")}";
    }
}
