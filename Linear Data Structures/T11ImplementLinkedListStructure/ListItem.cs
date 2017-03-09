namespace T11ImplementLinkedListStructure
{
    /// <summary>
    /// Define a class ListItem<T> that has two fields: value (of type T) and NextItem (of type ListItem<T>).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListItem<T>
    {
        private T value;

        private ListItem<T> nextItem;

        private ListItem<T> prevItem;

        public ListItem(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get { return this.value; }
        }

        public ListItem<T> NextItem
        {
            get { return this.nextItem; }
        }

        public ListItem<T> PrevItem
        {
            get { return this.prevItem; }
        }

        public void SetNext(T item)
        {
            this.nextItem = new ListItem<T>(item);
        }

        public void SetNext(ListItem<T> item)
        {
            this.nextItem = item;
        }

        public void SetPrev(T item)
        {
            this.prevItem = new ListItem<T>(item);
        }

        public void SetPrev(ListItem<T> item)
        {
            this.prevItem = item;
        }
    }
}
