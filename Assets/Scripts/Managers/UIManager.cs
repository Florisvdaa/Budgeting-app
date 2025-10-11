using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private List<UIPanel> panels;
    [SerializeField] private GameObject mainMenuPanel;

    [Header("Buttons")]
    [SerializeField] private Button returnButton;
    [SerializeField] private Button incomeButton;
    [SerializeField] private Button expenseButton;
    [SerializeField] private Button tagsButton;

    private Dictionary<string, GameObject> panelDict = new Dictionary<string, GameObject>();

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

        foreach (var panel in panels)
        {
            if (!panelDict.ContainsKey(panel.panelName))
            {
                panelDict.Add(panel.panelName, panel.panelObject);
            }
        }

        // Buttons setup
        incomeButton.onClick.AddListener(() => ShowPanel("Income"));
        expenseButton.onClick.AddListener(() => ShowPanel("Expense"));
        tagsButton.onClick.AddListener(() => ShowPanel("TagPanel"));
        returnButton.onClick.AddListener(() => ShowMainMenu());
    }

    private void Start()
    {
        ShowMainMenu();
    }


    public void ShowPanel(string panelName)
    {
        foreach (var panel in panelDict.Values)
        {
            panel.SetActive(false);
        }

        if (panelDict.ContainsKey(panelName))
        {
            panelDict[panelName].SetActive(true);
            SetNavigationButtonsActive(false);
            returnButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found.");
        }
    }
    public void ShowMainMenu()
    {
        foreach (var panel in panelDict.Values)
        {
            panel.SetActive(false);
        }

        // Activate the dashboard panel explicitly
        if (panelDict.ContainsKey("Dashboard"))
        {
            panelDict["Dashboard"].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Dashboard panel not found in panelDict.");
        }

        SetNavigationButtonsActive(true);
        returnButton.gameObject.SetActive(false);
    }

    private void SetNavigationButtonsActive(bool isActive)
    {
        incomeButton.gameObject.SetActive(isActive);
        expenseButton.gameObject.SetActive(isActive);
        tagsButton.gameObject.SetActive(isActive);
    }
}

[System.Serializable]
public class UIPanel
{
    public string panelName;
    public GameObject panelObject;
}

