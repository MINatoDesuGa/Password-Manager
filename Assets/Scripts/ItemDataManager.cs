using System;
using System.Collections.Generic;
using UnityEngine;
public class ItemDataManager : MonoBehaviour {
    private ItemDataList _itemDataList = new();

    private void OnEnable() {
        throw new NotImplementedException();
    }

    private void OnDisable() {
        throw new NotImplementedException();
    }

    private void AddItem(ItemData itemData) {
        _itemDataList.ItemDataCollection.Add(itemData);
    }
}
[System.Serializable]
public class ItemData {
    public string Title { get; private set; }
    public string Id { get; private set; }
    public string Password { get; private set; }
    public ItemData(string title, string id, string password) {
        Title = title; 
        Id = id;
        Password = password;
    }
}
[System.Serializable]
public class ItemDataList {
    public List<ItemData> ItemDataCollection = new();
}