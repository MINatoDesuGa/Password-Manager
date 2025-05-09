using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public static event Action<string, ItemData> OnItemView;
    public static event Action<string, ItemData> OnItemEdit;
    
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _viewButton;
    [SerializeField] private Button _editButton;
    
    private string _itemTitle;
    private ItemData _itemData;

    private void Awake() {
        _viewButton.onClick.AddListener(() => OnItemView?.Invoke(_itemTitle, _itemData));
        _editButton.onClick.AddListener(() => OnItemEdit?.Invoke(_itemTitle, _itemData));
    }
    private void OnDestroy() {
        _viewButton.onClick.RemoveAllListeners();
        _editButton.onClick.RemoveAllListeners();
    }

    public void Init(string itemTitle, ItemData itemData) {
        _itemData = itemData;
        _itemTitle = _title.text = itemTitle;
    }
}
