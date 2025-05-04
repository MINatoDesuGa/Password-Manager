using UnityEngine;
using System;
using TMPro;

public class NewItemAddHandler : MonoBehaviour
{
    public static event Action<ItemData> OnItemAdded;
    
    [SerializeField] private GameObject newItemPrefab;
    [SerializeField] private Transform newItemParent;
    [Space(10)]
    [Header(("Input refs"))]
    [SerializeField] private TMP_InputField titleInputField;
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnItemAddAction() {
        ValidateInput();
        ItemData itemData = new(titleInputField.text, idInputField.text, passwordInputField.text);
        Item item = Instantiate(newItemPrefab, newItemParent).GetComponent<Item>();
        if (item != null) {
            item.Init(itemData);
        }
        OnItemAdded?.Invoke(itemData);
    }

    private void ValidateInput() {
        /*
         * TODO: empty field check
         */
    }
}
