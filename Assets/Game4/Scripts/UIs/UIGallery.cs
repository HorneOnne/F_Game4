using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class UIGallery : CustomCanvas
{
    public Button CloseBtn;
    public Transform ContentParent;
    private List<UISlots> _slots;
    [SerializeField] private UISlots _slotPrefab;
    private void Start()
    {
        _slots = new();
        for (int i = 0; i < GameManager.Instance.GalleryCollections.Count; i++)
        {
            var slot = Instantiate(_slotPrefab, ContentParent.transform);
            if (GameManager.Instance.GalleryCollections[i].Unlock)
            {
                slot.Icon.sprite = GameManager.Instance.GalleryCollections[i].GalleryActiveIcon;
            }
            else
            {
                slot.Icon.sprite = GameManager.Instance.GalleryCollections[i].GalleryDeactiveIcon;
            }
            _slots.Add(slot);
        }

        CloseBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button, false);
            UIManager.Instance.CloseAll();
            UIManager.Instance.DisplayMainmenu(true);
        });

        GameManager.OnThemeChanged += UpdateTheme;
    }

    private void OnDestroy()
    {
        CloseBtn.onClick.RemoveAllListeners();
        GameManager.OnThemeChanged -= UpdateTheme;
    }

    private void UpdateTheme(ThemeDataSO data)
    {
        for(int i = 0; i < _slots.Count; i++)
        {
            if (GameManager.Instance.GalleryCollections[i].Unlock)
            {
                _slots[i].Background.sprite = data.GalleryUnlockSlot;
            }
            else
            {
                _slots[i].Background.sprite = data.GalleryLockSlot;
            }
         
        }
    }
}
