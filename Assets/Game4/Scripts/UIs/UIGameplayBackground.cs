using UnityEngine.UI;

public class UIGameplayBackground : CustomCanvas
{
    public Image Background;

    private void Start()
    {
        GameManager.OnThemeChanged += UpdateTheme;
        Background.sprite = GameManager.Instance.CurrentTheme.Background;
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
