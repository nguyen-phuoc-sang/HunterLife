public class AllItemModel
{
    public AllItemModel(string id, string image, string description, string itemName, int price, bool consumable)
    {
        _id = id;
        this.image = image;
        this.description = description;
        this.itemName = itemName;
        this.price = price;
        this.consumable = consumable;
    }

    public string _id { get; set; }
    public string image { get; set; }
    public string description { get; set; }

    public string itemName { get; set; }

    public int price { get; set; }

    public bool consumable { get; set; }


}
