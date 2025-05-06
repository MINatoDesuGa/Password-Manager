using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class ItemDataManager : MonoBehaviour {
    private string _saveFilePath;

    public static event Action<List<ItemData>> OnItemLoad;

    private List<ItemData> _itemDataList = new();
//===========================================================================================
    private void Start() {
        _saveFilePath = Application.persistentDataPath + "/itemData.json";
        LoadItemData();
    }

    private void OnEnable() {
        NewItemAddHandler.OnItemAdded += AddItem;
    }

    private void OnDisable() {
        NewItemAddHandler.OnItemAdded += AddItem;
    }
//===========================================================================================
    private void AddItem(ItemData itemData) {
        Debug.Log($"add item {itemData.Title}");
        _itemDataList.Add(itemData);
        SaveItemData();
    }
    private void SaveItemData() {
       string json = JsonConvert.SerializeObject( _itemDataList, Formatting.Indented );
        /// TODO: Encryption 
        File.WriteAllText(_saveFilePath, json);
        Debug.Log($"saved file to {_saveFilePath}");
    }
    private void LoadItemData() {
        if (File.Exists(_saveFilePath)) {
            string json = File.ReadAllText(_saveFilePath);
            _itemDataList = JsonConvert.DeserializeObject<List<ItemData>>(json);
            /// TODO: Decryption
            OnItemLoad?.Invoke(_itemDataList);
        } else { 
            Debug.LogError("save file doesn't exist");  
        } 
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