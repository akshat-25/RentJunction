﻿using RentJunction.Helper;
using RentJunction.Models;
using RentJunction.Views;

public class UI
{
    public IAuthController AuthController { get; set; }
    public UI(IAuthController authController)
    {
        AuthController = authController;
    }

    public  void StartMenu()
    {
        while (true)
        {
            Console.WriteLine(Strings.startMenu);

            var input = Console.ReadLine();
            bool isValidInput;

            while (true)
            {
                isValidInput = CheckValidity.IsValidInput(input);
                if (!isValidInput)
                {
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }

            Menu option = Enum.Parse<Menu>(input);
            Console.WriteLine();
            switch (option)
                {
                    case Menu.create_account:
                        Register();
                        break;
                    case Menu.login:
                        Login();
                        break;
                    case Menu.exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine(Strings.invalid);
                        break;
                }
        }
    }
    public void Register()
    {
            //List<User> userList = UserController.GetUserMasterList();
            User userEntity = RequestUserInput.Details();
            
            Console.WriteLine(Strings.chooseRole);

            var roleTaken = Console.ReadLine();
            bool isValidRole;

            while (true)
            {
                isValidRole = CheckValidity.IsValidRole(roleTaken);
            if (isValidRole)
            {
                break;
            }

            else
            {
                Console.WriteLine(Strings.validRole);
                roleTaken = Console.ReadLine();
            }
              
            }
            User user =  RoleHelper.RoleSetter(userEntity, Convert.ToInt32(roleTaken));
            bool isValidUser = AuthController.Register(user);
            
            if (isValidUser)
            {
                Console.WriteLine(Strings.regsuccess);
                Strings.Design();
                Console.WriteLine(Strings.enterCred);
                Login();
            }
            else
            {
                Console.WriteLine(Strings.wrong);
                Strings.Design();
                Register();
        }
     }
    public void Login()
    {
        Console.WriteLine(Strings.enterUserName);
        string username = Console.ReadLine();
        Console.WriteLine();
        while (!CheckValidity.CheckNull(username))
        {
            Console.WriteLine(Strings.userNameNotEmpty);
            username = Console.ReadLine();
        }

        Console.WriteLine(Strings.pswd);
        string password = CheckValidity.HideCharacter();
        Console.WriteLine();
        Strings.Design();

        while (!CheckValidity.CheckNull(password))
        {
            Console.WriteLine(Strings.passwordNameNotEmpty);
            password = CheckValidity.HideCharacter();
        }

        try
        {
            bool isLoginSuccess = AuthController.GetUserUI(username, password);
            if (!isLoginSuccess) {
                Console.WriteLine(Strings.loginFailed);
            }
        }
        catch(Exception ex)
        {
            File.AppendAllText(Strings.errorLoggerPath, ex.ToString() + DateTime.Now);
            Console.WriteLine("User Not Found!!");
        }
       
        Strings.Design();
    }

}