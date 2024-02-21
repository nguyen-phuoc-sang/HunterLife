using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.UI
{
    public class UIShopDescription : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text title;

        [SerializeField]
        private TMP_Text description;

        private int currentTotal = 0;
        private int initialPrice;

        [SerializeField] private TMP_Text total;
        [SerializeField] private TMP_Text btnIncrease;
        [SerializeField] private TMP_Text btnDecrease;
        [SerializeField] private TMP_Text btnPurchase;

        public void Awake()
        {
            ResetDiscription();
        }

        public void ResetDiscription()
        {
            itemImage.gameObject.SetActive(false);
            title.text = "";
            description.text = "";  
            total.text = "";
        }
        public void SetDescription(Sprite sprite, string itemname, string itemdesciption, int itemPrice)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            title.text = itemname;
            description.text = itemdesciption;

           
            initialPrice = itemPrice;
    
            total.text = itemPrice + "";
       

        }

        public void increase()
        {
      
            currentTotal += initialPrice;
            
            Debug.Log(initialPrice);
            
            total.text = currentTotal + "";

       
            SetDescription(itemImage.sprite, title.text, description.text, currentTotal);
    
        }

        public void decrease()
        {

            currentTotal -= initialPrice;

            Debug.Log(initialPrice);

            total.text = currentTotal + "";


            SetDescription(itemImage.sprite, title.text, description.text, currentTotal);

        }


    }
}
