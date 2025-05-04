using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public static event Action<ItemData> OnItemView;
    public static event Action<ItemData> OnItemEdit;
    
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _viewButton;
    [SerializeField] private Button _editButton;
    
    private ItemData _itemData;

    private void Awake() {
        _viewButton.onClick.AddListener(() => OnItemView?.Invoke(_itemData));
        _editButton.onClick.AddListener(() => OnItemEdit?.Invoke(_itemData));
    }
    private void OnDestroy() {
        _viewButton.onClick.RemoveAllListeners();
        _editButton.onClick.RemoveAllListeners();
    }

    public void Init(ItemData itemData) {
        _itemData = itemData;
        _title.text = itemData.Title;
    }
}
