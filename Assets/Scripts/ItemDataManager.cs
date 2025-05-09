using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class ItemDataManager : MonoBehaviour {
    private string _saveFilePath;

    public static event Action<Dictionary<string, ItemData>> OnItemLoad;

    private Dictionary<string, ItemData> _itemDataCollection = new();
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
    private void AddItem(string itemId, ItemData itemData) {
        _itemDataCollection[itemId] = itemData;
        SaveItemData();
    }
    private void SaveItemData() {
       string json = JsonConvert.SerializeObject( _itemDataCollection, Formatting.Indented );
        /// TODO: Encryption 
        File.WriteAllText(_saveFilePath, json);
        Debug.Log($"saved file to {_saveFilePath}");
    }
    private void LoadItemData() {
        if (File.Exists(_saveFilePath)) {
            string json = File.ReadAllText(_saveFilePath);
            _itemDataCollection = JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(json);
            /// TODO: Decryption
            OnItemLoad?.Invoke(_itemDataCollection);
        } else { 
            Debug.LogError("save file doesn't exist");  
        } 
    }
}
[System.Serializable]
public class ItemData {
    public string Id { get; private set; } // mail id / username
    public string Password { get; private set; } 
    public ItemData(string id, string password) {
        Id = id;
        Password = password;
    }
}