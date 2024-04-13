using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event System.Action<ThemeDataSO> OnThemeChanged;
    public static event System.Action OnGallaryUpdate;

    public List<LevelDataSO> Levels;
    [HideInInspector] public LevelDataSO CurrentLevel;

    public List<MahjongSO> GalleryCollections;
    public ThemeDataSO ValentineTheme;
    public ThemeDataSO ChristmasTheme;
    public ThemeDataSO CurrentTheme;

    private void Awake()
    {
        // Check if an instance already exists, and destroy the duplicate
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // FPS
        Application.targetFrameRate = 60;

        CurrentLevel = Levels[0];
        CurrentTheme = ChristmasTheme;
    }

    private void Start()
    {
        // Make the GameObject persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    public void RandomLevel()
    {
        CurrentLevel = Levels[Random.Range(0, Levels.Count)];
    }

    public void SetChristmasTheme()
    {
        OnThemeChanged?.Invoke(ChristmasTheme);
        CurrentTheme = ChristmasTheme;
    }

    public void SetValentineTheme()
    {
        OnThemeChanged?.Invoke(ValentineTheme);
        CurrentTheme = ValentineTheme;
    }
    public void TriggerGallaryUpdated()
    {
        OnGallaryUpdate?.Invoke();
    }
}

