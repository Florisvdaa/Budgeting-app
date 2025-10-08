using UnityEngine;

public class Income
{
    public decimal MonthlyAmount { get; set; }
    public Income(decimal amount)
    {
        MonthlyAmount = amount;
    }
}
