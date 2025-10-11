using TMPro;
using UnityEngine;

public class IncomeUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField incomeField;

    // Debug
    public DashboardUI dashboard;

    public void OnSaveIncomeClicked()
    {
        if(decimal.TryParse(incomeField.text, out decimal amount))
        {
            BudgetManager.Instance.SetIncome(amount);

            dashboard.setRemaining = true;

            UIManager.Instance.ShowMainMenu();
        }
        else
        {
            Debug.LogWarning("Invalid Income Input");
        }
    }
}
