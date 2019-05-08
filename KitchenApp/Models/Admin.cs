namespace KitchenApp.Models
{
    public class Admin : User
    {
        public override string Role { get { return Helper.ADMIN_ROLE; } }
    }
}
