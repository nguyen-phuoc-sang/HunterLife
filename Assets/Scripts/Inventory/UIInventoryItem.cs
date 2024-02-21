using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,
    IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private Image boder;

    public event Action<UIInventoryItem> OnItemclick, OnItemDroppedOn,
        OnItemBeginDrag, OnItemEndDrag, OnRightMouseButtonClick;
    private bool emty = true;




    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        emty = true;
    }

    public void Deselect()
    {
        boder.enabled = (false);
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        emty = false;
    }

    public void Select()
    {
        boder.enabled = true;
    }


    public void OnPointerClick(PointerEventData pointerdata)
    {
        if (pointerdata.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClick?.Invoke(this);
        }
        else
        {
            OnItemclick?.Invoke(this);
            Debug.Log("Click item inven");

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (emty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);

    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);

    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
