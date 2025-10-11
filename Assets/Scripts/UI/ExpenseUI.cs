using System.Collections.Generic;
using System.Linq;
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
    private void Start()
    {
        PopulateTagDropdown();

    }

    private void PopulateTagDropdown()
    {
        tagDropdown.ClearOptions();

        List<string> tagNames = BudgetManager.Instance.Tags.Select(tag => tag.Name).ToList();

        tagDropdown.AddOptions(tagNames);
    }
    public void OnAddExpenseClicked()
    {
        string name = nameField.text;
        decimal amount = decimal.Parse(amountField.text);
        string selectedTagName = tagDropdown.options[tagDropdown.value].text;
        bool recurring = recurringToggle.isOn;

        Tag selectedTag = BudgetManager.Instance.Tags
            .FirstOrDefault(t => t.Name == selectedTagName);

        if (selectedTag == null)
        {
            Debug.LogWarning($"Tag '{selectedTagName}' not found.");
            return;
        }

        budgetManager.AddExpense(name, amount, selectedTag, recurring);

        UIManager.Instance.ShowMainMenu();
    }
}
