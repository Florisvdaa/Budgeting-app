using UnityEngine;

public class Expense 
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public Tag Tag { get; set; }
    public bool IsRecurring { get; set; }
}
