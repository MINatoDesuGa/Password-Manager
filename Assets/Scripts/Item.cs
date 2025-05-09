using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler, IDragHandler {
    public static event Action<ItemAction, string, ItemData> OnItemAction;
    
    private const float ITEM_VIEW_TRIGGER_DELAY = 0.8f;
    private const float ITEM_DELETE_TRIGGER_DISTANCE = 100f; // trigger delete distance of item drag
    private const float ITEM_DELETE_START_THRESHOLD_DISTANCE = 10f; //when to start tweening item for delete

    [SerializeField] private TMP_Text _title;
    [SerializeField] private RectTransform _targetItemTransform;
    //TODO: delete item (on swipe left)

    private string _itemTitle;
    private ItemData _itemData;

    private bool _canActivateAction = true; // this will be used to not trigger multiple action at once
    private Vector2 _startTouchPos;
    private Vector2 _initialItemSize;
    private float _minDragX;

    private WaitForSeconds _delayDuration;
    private Coroutine _delayViewCoroutine;
    //=====================================================================
    public void Init(string itemTitle, ItemData itemData) {
        _itemData = itemData;
        _itemTitle = _title.text = itemTitle;

        _delayDuration = new(ITEM_VIEW_TRIGGER_DELAY);
        _initialItemSize = _targetItemTransform.rect.size;
        _minDragX = _initialItemSize.x - ITEM_DELETE_TRIGGER_DISTANCE;
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
        if (_initialItemSize != _targetItemTransform.sizeDelta) {
            _initialItemSize = _targetItemTransform.sizeDelta;
            _minDragX = _initialItemSize.x - ITEM_DELETE_TRIGGER_DISTANCE;
        }
        _startTouchPos = eventData.position;
        _delayViewCoroutine = StartCoroutine(DelayItemViewTrigger());

        IEnumerator DelayItemViewTrigger() {
            yield return _delayDuration;
            Debug.Log("Edit item triggered");
            _canActivateAction = false;
            OnItemAction?.Invoke(ItemAction.Edit, _itemTitle, _itemData);
        }
    }
    public void OnPointerUp(PointerEventData eventData) {
        _targetItemTransform.sizeDelta = _initialItemSize;
        ResetCoroutine(); //on released earler than specified trigger time, reset item view trigger
    }
    public void OnDrag(PointerEventData eventData) {
        _targetItemTransform.sizeDelta = new Vector2 (
            Mathf.Clamp( _initialItemSize.x - (_startTouchPos.x - eventData.position.x), _minDragX, _initialItemSize.x) 
            , _initialItemSize.y);
    }
}
public enum ItemAction {
    Edit, Delete, View
}