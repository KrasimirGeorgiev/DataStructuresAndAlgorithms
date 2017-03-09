namespace OnlineMarket
{
    using System;

    public class Program
    {
        private static void Main()
        {
            var shop = new Shop();
            CommandParser.Shop = shop;

            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                CommandParser.ParseCommand(command);
            }
        }
    }
}

