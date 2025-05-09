using System;
using TMPro;
using UnityEngine;

public class ItemActionHandler : MonoBehaviour {
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _id;
    [SerializeField] private TMP_Text _password;

    private void Awake() {
        Item.OnItemAction += OnItemAction;
        gameObject.SetActive(false);
    }
    private void OnDestroy() {
        Item.OnItemAction -= OnItemAction;
    }

    private void OnItemAction(ItemAction itemAction, string itemTitle, ItemData itemData) {
        switch(itemAction) {
            case ItemAction.View:
                _title.text = itemTitle;
                _id.text = itemData.Id;
                _password.text = itemData.Password;
                gameObject.SetActive(true);
                break;
            case ItemAction.Edit:
                break;
            case ItemAction.Delete:
                break;
        }
        
    }
}
