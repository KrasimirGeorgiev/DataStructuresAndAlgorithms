namespace HashTables.PhoneBook
{
    using HashTables.TableImplementation;

    public class PhoneBook
    {
        public PhoneBook()
        {
            this.Names = new HashTable<string, BookEntry>();
            this.Towns = new HashTable<string, BookEntry>();
            this.Phones = new HashTable<string, BookEntry>();
        }

        public HashTable<string, BookEntry> Names { get; set; }

        public HashTable<string, BookEntry> Towns { get; set; }

        public HashTable<string, BookEntry> Phones { get; set; }
    }
}