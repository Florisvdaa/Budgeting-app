using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpenseUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField amountField;
    [SerializeField] private TMP_Dropdown tagDropdown;
    [SerializeField] private Toggle recurringToggle;

    public BudgetManager budgetManager;

    public void OnAddExpenseClicked()
    {
        string name = nameField.text;
        decimal amount = decimal.Parse(amountField.text);
        string tag = tagDropdown.options[tagDropdown.value].text;
        bool recurring = recurringToggle.isOn;

        budgetManager.AddExpense(name, amount, tag, recurring);

        UIManager.Instance.ShowMainMenu();
    }
}
