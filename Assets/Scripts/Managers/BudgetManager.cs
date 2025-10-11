using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BudgetManager : MonoBehaviour
{
    public static BudgetManager Instance { get; private set; }

    private Income income;
    private List<Expense> expenses = new List<Expense>();
    private List<SavingGoal> goals = new List<SavingGoal>();
    private List<Tag> tags = new List<Tag>();

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

    private void Start()
    {
        //LoadTags();

        if (tags.Count != 0)
        {
            LoadTags();
        }
        else
        {
            InitializeDefaultTags();
        }
    }

    public void AddExpense(string name, decimal amount, Tag tag, bool recurring)
    {
        expenses.Add(new Expense
        {
            Name = name,
            Amount = amount,
            Tag = tag,
            IsRecurring = recurring
        });

        Debug.Log($"Successfully added Expense: {name}, {amount}, {tag.Name}, {recurring}");
    }

    public void AddTag(string name, string colorHex)
    {
        if(!tags.Any(t => t.Name == name))
        {
            tags.Add(new Tag { Name = name, ColorHex = colorHex });
            Debug.Log($"Tag added: {name}");
            SaveTags();
        }
    }

    public decimal GetRemainingBudget()
    {
        if (income == null)
        {
            Debug.LogWarning("Income is not set.");
            return 0m;
        }

        decimal totalExpenses = expenses?.Sum(e => e.Amount) ?? 0m;
        return income.MonthlyAmount - totalExpenses;
    }

    public Dictionary<string, decimal> GetExpenseByTag() => expenses.GroupBy(e => e.Tag.Name).ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
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


    // Create default Tags
    public void InitializeDefaultTags()
    {
        AddTag("Rent", "#00BFA6");          // Teal
        AddTag("Food", "#FFA726");          // Orange
        AddTag("Transport", "#29B6F6");     // Light Blue
        AddTag("Fun", "#AB47BC");           // Purple
        AddTag("Subscriptions", "#FDD835"); // Yellow
    }

    public void SaveTags()
    {
        string json = JsonUtility.ToJson(new TagListWrapper { Tags = tags });
        PlayerPrefs.SetString("tags", json);
    }
    public void LoadTags()
    {
        if (PlayerPrefs.HasKey("tags"))
        {
            string json = PlayerPrefs.GetString("tags");
            TagListWrapper wrapper = JsonUtility.FromJson<TagListWrapper>(json);
            tags = wrapper.Tags;
        }
    }
    public void SetIncome(decimal amount) => income = new Income(amount);

    // References
    public Income Income => income;
    public List<Expense> Expenses => expenses;
    public List<SavingGoal> SavingGoals => goals;
    public List<Tag> Tags => tags;
}

[System.Serializable]
public class TagListWrapper
{
    public List<Tag> Tags;
}
