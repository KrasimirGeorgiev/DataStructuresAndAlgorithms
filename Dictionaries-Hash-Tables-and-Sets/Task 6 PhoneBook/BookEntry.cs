namespace HashTables.PhoneBook
{
    using System;

    public class BookEntry : IComparable<BookEntry>
    {
        public string Name { get; set; }

        public string Town { get; set; }

        public string Phone { get; set; }

        public int CompareTo(BookEntry other)
        {
            int nameCompare = String.Compare(this.Name, other.Name, StringComparison.Ordinal);

            if (nameCompare != 0)
            {
                return nameCompare;
            }
            else
            {
                int townCompare = String.Compare(this.Town, other.Town, StringComparison.Ordinal);
                if (townCompare != 0)
                {
                    return townCompare;
                }
                else
                {
                    return String.Compare(this.Phone, other.Phone, StringComparison.Ordinal);
                }
            }

        }

        public override string ToString()
        {
            return $"{this.Name,-30} | {this.Town,-30} | {this.Phone,-30}";
        }
    }
}