using System;
using UnityEngine;

public class SavingGoal 
{
    public string GoalName { get; set; }
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }
    public decimal CurrentSaved {  get; set; }
    public decimal Progress => (CurrentSaved / GoalAmount) * 100;
}
