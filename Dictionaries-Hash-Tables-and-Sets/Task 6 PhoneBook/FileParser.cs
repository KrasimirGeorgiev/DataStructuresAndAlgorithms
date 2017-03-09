namespace HashTables.PhoneBook
{
    using System.IO;

    public class FileParser
    {
        public static PhoneBook ParsePhoneBook(
            string path, char[] splitChars)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var phonebook = new PhoneBook();

            using (TextReader reader = new StreamReader(path))
            {
                string nextLine;
                while ((nextLine = reader.ReadLine()) != null)
                {
                    string[] elements = nextLine.Split(splitChars);
                    var entry = new BookEntry
                    {
                        Name = elements[0].Trim(),
                        Town = elements[1].Trim(),
                        Phone = elements[2].Trim()
                    };

                    // Since entry is reference type the book entry is shared among all 
                    // tables and the memory it takes is not trippled
                    phonebook.Names.Add(entry.Name, entry);
                    phonebook.Towns.Add(entry.Town, entry);
                    phonebook.Phones.Add(entry.Phone, entry);
                }
            }

            return phonebook;
        }

        public static string[] ParseCommands(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(path);

            return lines;
        }
    }
}