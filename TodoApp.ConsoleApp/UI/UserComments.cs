namespace TodoApp.ConsoleApp.UI
{
    static class UserComments
    {
        public static string WelcomeMessage()
        {
            string message = "Hello, this program is making a task's and save them to xml document. Enjoy :)";
            return message;
        }

        public static string FieldCantBeEmpty()
        {
            string message="This field can't be empty!";
            return message;
        }

        public static string InvalidComand()
        {
            string message = "Error! Invalid comand!";
            return message;
        }

        public static string YesOrNo()
        {
            string message = "Please, enter either \"yes\" or \"no\"";
            return message;
        }

        public static string SaveCompleted()
        {
            string message = "Save completed!";
            return message;
        }

        public static string NoSavedTasks()
        {
            string message = "There is no saved task's!";
            return message;
        }

        public static string GoodBye()
        {
            string message = "Good bye!";
            return message;
        }
    }
}
