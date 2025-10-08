using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BudgetManager : MonoBehaviour
{
    public static BudgetManager Instance { get; private set; }

    private Income income;
    private List<Expense> expenses = new List<Expense>();
    private List<SavingGoal> goals = new List<SavingGoal>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddExpense(string name, decimal amount, string tag, bool recurring)
    {
        expenses.Add(new Expense
        {
            Name = name,
            Amount = amount,
            Tag = tag,
            IsRecurring = recurring
        });

        Debug.Log($"Succesfully added Expense: {name} , {amount} , {tag} , {recurring}");
    }

    public decimal GetRemainingBudget()
    {
        decimal totalExpenses = expenses.Sum(e => e.Amount);
        return income.MonthlyAmount - totalExpenses;
    }
    public void SaveIncome()
    {
        PlayerPrefs.SetString("income", income.MonthlyAmount.ToString());
    }

    public void LoadIncome()
    {
        if (PlayerPrefs.HasKey("income"))
        {
            decimal amount = decimal.Parse(PlayerPrefs.GetString("income"));
            income = new Income(amount);
        }
    }


    public void SetIncome(decimal amount) => income = new Income(amount);

    // References
    public Income Income => income;
    public List<Expense> Expenses => expenses;
    public List<SavingGoal> SavingGoals => goals;
}
