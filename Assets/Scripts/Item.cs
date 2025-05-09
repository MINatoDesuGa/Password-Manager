using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler {
    public static event Action<ItemAction, string, ItemData> OnItemAction;
    
    private const float ITEM_VIEW_TRIGGER_DELAY = 0.8f;

    [SerializeField] private TMP_Text _title;
    //TODO: delete item (on swipe left)

    private string _itemTitle;
    private ItemData _itemData;

    private bool _canActivateAction = true; // this will be used to not trigger multiple action at once

    private WaitForSeconds _delayDuration;
    private Coroutine _delayViewCoroutine;
    //=====================================================================
    public void Init(string itemTitle, ItemData itemData) {
        _itemData = itemData;
        _itemTitle = _title.text = itemTitle;

        _delayDuration = new(ITEM_VIEW_TRIGGER_DELAY);
    }
    private void ResetCoroutine() {
        if(_delayViewCoroutine != null) { 
            StopCoroutine( _delayViewCoroutine );
            _delayViewCoroutine = null;
        }
    }
    public void OnPointerClick(PointerEventData eventData) {
        if(!_canActivateAction) { // this means other actions was triggered
            _canActivateAction = true;
            return;
        }
        OnItemAction?.Invoke(ItemAction.View, _itemTitle, _itemData);
    }
    public void OnPointerDown(PointerEventData eventData) {
        //On hold for specified trigger time, trigger item view
        ResetCoroutine();
        _delayViewCoroutine = StartCoroutine(DelayItemViewTrigger());

        IEnumerator DelayItemViewTrigger() {
            yield return _delayDuration;
            Debug.Log("Edit item triggered");
            _canActivateAction = false;
            OnItemAction?.Invoke(ItemAction.Edit, _itemTitle, _itemData);
        }
    }
    public void OnPointerUp(PointerEventData eventData) {
        ResetCoroutine(); //on released earler than specified trigger time, reset item view trigger
    }
}
public enum ItemAction {
    Edit, Delete, View
}