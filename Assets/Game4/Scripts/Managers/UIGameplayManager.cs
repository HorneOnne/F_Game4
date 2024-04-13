using UnityEngine;
public class UIGameplayManager : MonoBehaviour
{
    public static UIGameplayManager Instance { get; private set; }


    public UIGameplay UIGameplay;
    public UIWin UIWin;
    public UIGameplaySettings UIGameplaySettings;
    public UIGameplayChooseTheme UIGameplayChooseTheme;
    public UIGameplayGallery UIGameplayGallery;




    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        CloseAll();
    }


    public void CloseAll()
    {
        DisplayUIWin(false);
        DisplayUIGameplaySettings(false);
        DisplayUIGameplayChooseTheme(false);
        DisplayUIGameplayGallery(false);
    }

    public void DisplayUIWin(bool isActive)
    {
        UIWin.DisplayCanvas(isActive);
    }



    public void DisplayUIGameplaySettings(bool isActive)
    {
        UIGameplaySettings.DisplayCanvas(isActive);
    }

    public void DisplayUIGameplayChooseTheme(bool isActive)
    {
        UIGameplayChooseTheme.DisplayCanvas(isActive);
    }

    public void DisplayUIGameplayGallery(bool isActive)
    {
        UIGameplayGallery.DisplayCanvas(isActive);
    }

}
