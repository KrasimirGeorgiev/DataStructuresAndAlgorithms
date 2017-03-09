namespace TreeOfNodes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class TreeOfNodes<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private T value;

        private IList<TreeOfNodes<T>> descendants;

        private TreeOfNodes<T> parent;

        private int size;

        public TreeOfNodes()
        {
            this.size = 0;
            this.descendants = new List<TreeOfNodes<T>>();
        }

        public TreeOfNodes(T element)
            : this()
        {
            this.size = 1;
            this.Value = element;
        }

        public TreeOfNodes(TreeOfNodes<T> parent)
            : this()
        {
            this.parent = parent;
        }

        public IList<TreeOfNodes<T>> DirectDescendants
        {
            get { return this.descendants; }
        }

        public T Value
        {
            get { return this.value; }

            set { this.value = value; }
        }

        public int Size
        {
            get { return this.size; }
        }

        public TreeOfNodes<T> Parent
        {
            get { return this.parent; }

            private set { this.parent = value; }
        }

        public override string ToString()
        {
            if (this.size > 1)
            {
                var sb = new StringBuilder();
                List<TreeOfNodes<T>> toBeStringosan = new List<TreeOfNodes<T>>();

                sb.Append(this.Value)
                    .Append(" -> ");

                foreach (TreeOfNodes<T> descendant in this.DirectDescendants)
                {
                    sb.AppendFormat("{0}, ", descendant.Value);
                    if (descendant.size > 1)
                    {
                        toBeStringosan.Add(descendant);
                    }
                }

                sb.Remove(sb.Length - 2, 2);

                if (toBeStringosan.Count > 0)
                {
                    sb.AppendLine();
                    foreach (TreeOfNodes<T> descendant in toBeStringosan)
                    {
                        sb.AppendLine(descendant.ToString());
                    }
                }

                return sb.ToString();
            }
            else
            {
                return this.Value.ToString();
            }
        }

        public TreeOfNodes<T> Add(T element)
        {
            TreeOfNodes<T> result;

            if (this.size == 0)
            {
                this.Value = element;
                result = this;
            }
            else
            {
                result = new TreeOfNodes<T>(element) { parent = this };

                this.descendants.Add(result);
            }

            this.size++;

            return result;
        }

        public TreeOfNodes<T> Add(TreeOfNodes<T> tree)
        {
            if (this.Size > 0)
            {
                tree.Parent = this;
                this.descendants.Add(tree);
                this.size++;
            }

            return tree;
        }

        public void AddMany(IEnumerable<T> elements)
        {
            foreach (T element in elements)
            {
                this.Add(element);
            }
        }

        public void AddMany(IEnumerable<TreeOfNodes<T>> directDescendants)
        {
            foreach (TreeOfNodes<T> descendant in directDescendants)
            {
                this.Add(descendant);
            }
        }

        public TreeOfNodes<T> Remove(T element)
        {
            TreeOfNodes<T> removedElement = null;

            for (int i = 0; i < this.descendants.Count; i++)
            {
                if (this.descendants[i].Value.Equals(element))
                {
                    removedElement = this.descendants[i];
                    this.descendants.RemoveAt(i);
                    this.size--;
                    break;
                }
                else if (this.descendants[i].Size > 1)
                {
                    removedElement = this.descendants[i].Remove(element);
                    if (removedElement != null)
                    {
                        this.size--;
                        break;
                    }
                }
            }

            return removedElement;
        }

        /// <summary>
        /// Attempts to find the given value in all tree nodes (including self)
        /// </summary>
        /// <param name="element">The searched element</param>
        /// <returns>
        /// Returns The TreeNode which value is equal to <paramref name="element"/> or null if 
        /// it doesn't exist
        /// </returns>
        public TreeOfNodes<T> Contains(T element)
        {
            if (this.Value.Equals(element))
            {
                return this;
            }
            else if (this.descendants.Count > 0)
            {
                foreach (TreeOfNodes<T> descendant in this.descendants)
                {
                    TreeOfNodes<T> result = descendant.Contains(element);
                    if (result != null)
                    {
                        return result;
                    }
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Implements DFS Traversal
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (TreeOfNodes<T> descendant in this.descendants)
            {
                if (descendant.Size > 0)
                {
                    foreach (T value in descendant)
                    {
                        yield return value;
                    }
                }
                else
                {
                    yield return descendant.Value;
                }
            }

            yield return this.Value;
        }

        /// <summary>
        /// Implements DFS Traversal
        /// </summary>
        /// <returns>DFS Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
