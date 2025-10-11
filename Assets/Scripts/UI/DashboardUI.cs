using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashboardUI : MonoBehaviour
{
    [SerializeField] private Image remainingBudgetGraph;
    [SerializeField] private TextMeshProUGUI remainingBudgetText;

    // Debug
    public bool setRemaining = false;

    private void Start()
    {
        decimal remaining = BudgetManager.Instance.GetRemainingBudget();
        remainingBudgetText.text = $"€ {(remaining > 0 ? remaining.ToString("F2") : "0")}";

        UpdateGraphFill(remaining);
    }

    private void Update()
    {
        if (setRemaining)
        {
            decimal remaining = BudgetManager.Instance.GetRemainingBudget();
            remainingBudgetText.text = $"€ {remaining.ToString("F2")}";
            UpdateGraphFill(remaining);
        }

    }
    private void UpdateGraphFill(decimal remaining)
    {
        decimal income = BudgetManager.Instance.Income?.MonthlyAmount ?? 0;

        if (income <= 0)
        {
            remainingBudgetGraph.fillAmount = 0f;
            remainingBudgetGraph.color = Color.red;
            return;
        }

        float fillPercent = Mathf.Clamp01((float)(remaining / income));
        remainingBudgetGraph.fillAmount = fillPercent;

        // Color transition Green (100%) -> Yellow (50%) -> Red (0%)
        Color budgetColor = Color.Lerp(Color.red, Color.yellow, fillPercent);
        budgetColor = Color.Lerp(budgetColor, Color.green, fillPercent);
        remainingBudgetGraph.color = budgetColor;

    }

}
