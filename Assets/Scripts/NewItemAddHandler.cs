using UnityEngine;
using System;
using System.Collections.Generic;
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
        ValidateInput();
        ItemData itemData = SpawnItem();
        OnItemAdded?.Invoke(itemData);
    }

    private ItemData SpawnItem(ItemData itemData = null) {
        if (itemData == null) {
            itemData = new(titleInputField.text, idInputField.text, passwordInputField.text);    
        }
        
        Item item = Instantiate(newItemPrefab, newItemParent).GetComponent<Item>();
        if (item != null) {
            item.Init(itemData);
        }
        return itemData;
    }

    private void OnItemLoad(List<ItemData> itemDataList) {
        foreach (var itemData in itemDataList) {
            Debug.Log($"loading {itemData.Title}");
            SpawnItem(itemData);
        }
    }
    private void ValidateInput() {
        /*
         * TODO: empty field check
         */
    }
}
