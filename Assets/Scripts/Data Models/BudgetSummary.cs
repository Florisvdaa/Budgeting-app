using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BudgetSummary
{
    public Income Income { get; set; }
    public List<Expense> Expenses { get; set; }
    public List<SavingGoal> Goals { get; set; }
    public decimal TotalExpenses => Expenses.Sum(e => e.Amount);
    public decimal RemainingBudget => Income.MonthlyAmount - TotalExpenses;
}
