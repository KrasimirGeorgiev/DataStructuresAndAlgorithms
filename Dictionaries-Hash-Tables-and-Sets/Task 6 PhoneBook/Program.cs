namespace Task_6_PhoneBook
{
    using System;
    using System.Text.RegularExpressions;
    using HashTables.PhoneBook;
    using HomeworkHelpers;

    /// <summary>
    /// A text file phones.txt holds information about people, their town and phone number
    /// Duplicates can occur in people names, towns and phone numbers. Write a program to 
    /// read the phones file and execute a sequence of commands given in the file commands.txt
    ///     find(name) – display all matching records by given name (first, middle, last or nickname)
    ///     find(name, town) – display all matching records by given name and town
    /// </summary>
    public class Program
    {
        private static HomeworkHelper helper = new HomeworkHelper();

        private static void Main(string[] args)
        {
            helper.ConsoleMio.Setup();

            helper.ConsoleMio.PrintHeading("Task 6 Phonebook");

            Regex pattern = new Regex(@"find\((.+)\)");

            var phonebook = FileParser.ParsePhoneBook("phones.txt", new[] { '|' });
            string[] commands = FileParser.ParseCommands("commands.txt");

            foreach (string command in commands)
            {
                string name = pattern.Match(command).Groups[1].Value;
                if (phonebook.Names.Find(name) != null)
                {
                    helper.ConsoleMio.WriteLine(phonebook.Names[name].ToString(), ConsoleColor.DarkGreen);
                }
            }

        }
    }
}
