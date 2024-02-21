using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sell.UI
{
    public class UISellDescription : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text title;

        [SerializeField]
        private TMP_Text description;

        public static int currentTotal;
        private int initialquantity;
        private int sumPrice;
        public  int price;
        public static int priceItem;

        [SerializeField] private TMP_Text total;
        [SerializeField] private TMP_Text quantity;
        [SerializeField] private TMP_Text btnIncrease;
        [SerializeField] private TMP_Text btnDecrease;
        [SerializeField] private TMP_Text btnPurchase;


        // Start is called before the first frame update
        void Start()
        {
            ResetDiscription();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ResetDiscription()
        {
            itemImage.gameObject.SetActive(false);
            title.text = "";
            description.text = "";
            total.text = "";
            quantity.text = "";
        }

        public void SetDescription(Sprite sprite, string itemname, string itemdesciption, int itemPrice, int itemquatity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            title.text = itemname;
            description.text = itemdesciption;
            quantity.text = currentTotal + "";
           
            initialquantity = SellController.quantity;
            price = SellController.price;
            total.text = itemPrice + "";
        }

        public void increase()
        {

          //  currentTotal += initialquantity;

           // Debug.Log(initialquantity);
            if(currentTotal < initialquantity)
            {
                currentTotal += 1;
                price = price * currentTotal;
                quantity.text = currentTotal + "";
                priceItem = price;
                
                SetDescription(itemImage.sprite, title.text, description.text, price, currentTotal);
            }
            



            

        }

        public void decrease()
        {

            // currentTotal -= initialquantity;

           
            if (currentTotal > 1 )
            {
                currentTotal -= 1;
                price = price * currentTotal;
                quantity.text = currentTotal + "";
                priceItem = price;
                
                SetDescription(itemImage.sprite, title.text, description.text,price, currentTotal);
            }
           


           

        }
    }
}
