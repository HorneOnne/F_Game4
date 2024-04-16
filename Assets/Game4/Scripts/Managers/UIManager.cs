using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public UIMainmenu UIMainmenu;
    public UISettings UISettings;
    public UIWelcome UIWelcome;
    public UIGallery UIGallery;
    public UIChooseTheme UIChooseTheme;




    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        CloseAll();
        if(GameManager.Instance.ShowWelcome)
        {
            GameManager.Instance.ShowWelcome = false;
            DisplayUIWelcome(true);
        }
     
        DisplayMainmenu(true);
    }

    public void CloseAll()
    {
        DisplayMainmenu(false);
        DisplaySettingsMenu(false);
        DisplayUIWelcome(false);
        DisplayUIGallery(false);
        DisplayUIChooseTheme(false);
    }


    public void DisplayMainmenu(bool isActive)
    {
        UIMainmenu.DisplayCanvas(isActive);
    }

    public void DisplaySettingsMenu(bool isActive)
    {
        UISettings.DisplayCanvas(isActive);
    }

    public void DisplayUIWelcome(bool isActive)
    {
        UIWelcome.DisplayCanvas(isActive);
    }

    public void DisplayUIGallery(bool isActive)
    {
        UIGallery.DisplayCanvas(isActive);
    }

    public void DisplayUIChooseTheme(bool isActive)
    {
        UIChooseTheme.DisplayCanvas(isActive);
    }
}
