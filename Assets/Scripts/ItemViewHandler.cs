using System;
using TMPro;
using UnityEngine;

public class ItemViewHandler : MonoBehaviour {
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _id;
    [SerializeField] private TMP_Text _password;

    private void Awake() {
        Item.OnItemView += OnItemView;
        gameObject.SetActive(false);
    }
    private void OnDestroy() {
        Item.OnItemView -= OnItemView;
    }

    private void OnItemView(string itemTitle, ItemData itemData) {
        _title.text = itemTitle;
        _id.text = itemData.Id;
        _password.text = itemData.Password;
        gameObject.SetActive(true);
    }
}
