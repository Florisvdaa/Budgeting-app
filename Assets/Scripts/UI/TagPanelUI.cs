using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TagPanelUI : MonoBehaviour
{
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject tagEntryPrefab;

    private void Start()
    {
        var tagExpenses = BudgetManager.Instance.GetExpenseByTag();
        var tags = BudgetManager.Instance.Tags;

        foreach (var tag in tags)
        {
            GameObject entry = Instantiate(tagEntryPrefab, contentParent);

            // Use named references instead of indexing
            var nameText = entry.transform.Find("TagName").GetComponent<TextMeshProUGUI>();
            var amountText = entry.transform.Find("TagAmount").GetComponent<TextMeshProUGUI>();
            var colorImage = entry.transform.Find("TagColor").GetComponent<Image>();

            nameText.text = tag.Name;
            amountText.text = "€ " + (tagExpenses.ContainsKey(tag.Name) ? tagExpenses[tag.Name].ToString("F2") : "0.00");

            if (ColorUtility.TryParseHtmlString(tag.ColorHex, out Color color))
            {
                colorImage.color = color;
            }
        }
    }
}
