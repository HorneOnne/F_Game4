using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class UIGameplayGallery : CustomCanvas
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
            UIGameplayManager.Instance.DisplayUIGameplayGallery(false);
        });

        GameManager.OnThemeChanged += UpdateTheme;
        GameManager.OnGallaryUpdate += UpdateGallary;
    }

    private void OnDestroy()
    {
        CloseBtn.onClick.RemoveAllListeners();
        GameManager.OnThemeChanged -= UpdateTheme;
        GameManager.OnGallaryUpdate -= UpdateGallary;
    }

    private void UpdateTheme(ThemeDataSO data)
    {
        for (int i = 0; i < _slots.Count; i++)
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


    private void UpdateGallary()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (GameManager.Instance.GalleryCollections[i].Unlock)
            {
                _slots[i].Icon.sprite = GameManager.Instance.GalleryCollections[i].GalleryActiveIcon;
            }
            else
            {
                _slots[i].Icon.sprite = GameManager.Instance.GalleryCollections[i].GalleryDeactiveIcon;
            }
        }
    }

}

