using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

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

    public void SaveAllData()
    {
        BudgetManager.Instance.SaveIncome();
        SaveExpenses();
        BudgetManager.Instance.SaveTags();

        Debug.Log("All data saved.");
    }

    public void LoadAllData()
    {
        BudgetManager.Instance.LoadIncome();
        LoadExpenses();
        BudgetManager.Instance.LoadTags();

        Debug.Log("All data loaded.");
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        BudgetManager.Instance.SetIncome(0);
        BudgetManager.Instance.Expenses.Clear();
        BudgetManager.Instance.Tags.Clear();

        Debug.Log("All data cleared.");
    }

    private void SaveExpenses()
    {
        var expenses = BudgetManager.Instance.Expenses;
        List<SerializableExpense> serializable = expenses.Select(e => new SerializableExpense
        {
            Name = e.Name,
            Amount = e.Amount,
            TagName = e.Tag.Name,
            IsRecurring = e.IsRecurring
        }).ToList();

        string json = JsonUtility.ToJson(new ExpenseListWrapper { Expenses = serializable });
        PlayerPrefs.SetString("expenses", json);
    }

    private void LoadExpenses()
    {
        if (PlayerPrefs.HasKey("expenses"))
        {
            string json = PlayerPrefs.GetString("expenses");
            ExpenseListWrapper wrapper = JsonUtility.FromJson<ExpenseListWrapper>(json);

            BudgetManager.Instance.Expenses.Clear();

            foreach (var e in wrapper.Expenses)
            {
                Tag tag = BudgetManager.Instance.Tags.FirstOrDefault(t => t.Name == e.TagName);
                if (tag != null)
                {
                    BudgetManager.Instance.AddExpense(e.Name, e.Amount, tag, e.IsRecurring);
                }
            }
        }
    }
}

[System.Serializable]
public class SerializableExpense
{
    public string Name;
    public decimal Amount;
    public string TagName;
    public bool IsRecurring;
}

[System.Serializable]
public class ExpenseListWrapper
{
    public List<SerializableExpense> Expenses;
}

