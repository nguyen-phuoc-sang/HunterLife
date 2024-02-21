public class UserModel
{
    public UserModel(string id, float positionX, float positionY, float positionZ)
    {
        this.id = id;
        this.positionX = positionX;
        this.positionY = positionY;
        this.positionZ = positionZ;
    }

    public UserModel(string id, float coin)
    {
        this.id = id;
        this.coin = coin;

    }

    public UserModel(string email, string password, string confirm_password)
    {
        this.email = email;
        this.password = password;
        this.confirm_password = confirm_password;
    }

    public UserModel(string email, string password)
    {
        this.email = email;
        this.password = password;
    }

    public UserModel(string email)
    {
        this.email = email;
    }

    public UserModel(string email, string password, string confirm_password, string otp) : this(email, password, confirm_password)
    {
        this.otp = otp;
    }

    public string email { get; set; }
    public string password { get; set; }
    public string confirm_password { get; set; }
    public string otp { get; set; }

    // save vị trí/ coin
    public string id { get; set; }
    public float positionX { get; set; }
    public float positionY { get; set; }
    public float positionZ { get; set; }
    public float coin { get; set; }

}
