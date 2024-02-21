using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISellItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private TMP_Text priceTxt;
    [SerializeField] private Image boder;

    public event Action<UISellItem> OnItemclick, OnRightMouseButtonClick;
    private bool emty = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

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
            Debug.Log("Click Item Sell");

        }
    }
}
