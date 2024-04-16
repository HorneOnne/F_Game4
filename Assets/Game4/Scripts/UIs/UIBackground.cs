using UnityEngine.UI;

public class UIBackground : CustomCanvas
{
    public Image Background;

    private void Start()
    {
        GameManager.OnThemeChanged += UpdateTheme;
        UpdateTheme(GameManager.Instance.CurrentTheme);
    }

    private void OnDestroy()
    {
        GameManager.OnThemeChanged -= UpdateTheme;
    }


    private void UpdateTheme(ThemeDataSO data)
    {
        Background.sprite = data.Background;
    }
}
