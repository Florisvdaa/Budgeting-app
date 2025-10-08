using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BudgetManager : MonoBehaviour
{
    public Income income = new Income();
    public List<Expense> expenses = new List<Expense>();
    public List<SavingGoal> goals = new List<SavingGoal>();

    public void AddExpense(string name, decimal amount, string tag, bool recurring)
    {
        expenses.Add(new Expense
        {
            Name = name,
            Amount = amount,
            Tag = tag,
            IsRecurring = recurring
        });
    }

    public decimal GetRemainingBudget()
    {
        decimal totalExpenses = expenses.Sum(e => e.Amount);
        return income.MonthlyAmount - totalExpenses;
    }
}
