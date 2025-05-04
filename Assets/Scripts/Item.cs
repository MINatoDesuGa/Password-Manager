using TMPro;
using UnityEngine;

public class Item : MonoBehaviour {
    [SerializeField] private TMP_Text _title;
    
    private ItemData _itemData;

    public void Init(ItemData itemData) {
        _itemData = itemData;
        _title.text = itemData.Title;
    }
}
