namespace RentJunction.Views
{
    public class Program
    {
        public static void Main()
        {
            UI ui = new UI(new AuthController(DBUsers.Instance), new UserController(DBUsers.Instance));
            ui.StartMenu();
        }
    }
}