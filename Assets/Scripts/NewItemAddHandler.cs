using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class NewItemAddHandler : MonoBehaviour
{
    public static event Action<string, ItemData> OnItemAdded;
    
    [SerializeField] private GameObject newItemPrefab;
    [SerializeField] private Transform newItemParent;
    [Space(10)]
    [Header(("Input refs"))]
    [SerializeField] private TMP_InputField titleInputField;
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private TMP_InputField passwordInputField;
//=================================================================================================================
    private void Awake() {
        ItemDataManager.OnItemLoad += OnItemLoad;
        gameObject.SetActive(false);
    }
    private void OnDestroy() {
        ItemDataManager.OnItemLoad -= OnItemLoad;
    }

    //=================================================================================================================
    public void OnItemAddAction() {
        ValidateInput(); //TODO: check for empty fields or duplicates
        ItemData itemData = SpawnItem(titleInputField.text);
        OnItemAdded?.Invoke(titleInputField.text, itemData);
    }

    private ItemData SpawnItem(string itemTitle, ItemData itemData = null) {
        if (itemData == null) {
            itemData = new(idInputField.text, passwordInputField.text);    
        }
        
        Item item = Instantiate(newItemPrefab, newItemParent).GetComponent<Item>();
        if (item != null) {
            item.Init(itemTitle, itemData);
        }
        return itemData;
    }

    private void OnItemLoad(Dictionary<string, ItemData> itemDataCollection) {
        foreach (var itemData in itemDataCollection) {
            SpawnItem(itemData.Key, itemData.Value);
        }
    }
    private void ValidateInput() {
        /*
         * TODO: empty field check
         */
    }
}
