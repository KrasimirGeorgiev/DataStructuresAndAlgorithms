namespace TreeOfNodes
{
    using System;
    using System.Collections.Generic;

    public class TreeProcessing
    {
        public static IList<TreeOfNodes<T>> CalculateLongestPath<T>(TreeOfNodes<T> tree)
            where T : IComparable<T>
        {
            List<TreeOfNodes<T>> chain = new List<TreeOfNodes<T>>();
            chain.Add(tree);

            if (tree.Size > 1)
            {
                List<TreeOfNodes<T>> subChain = new List<TreeOfNodes<T>>();
                foreach (TreeOfNodes<T> descendant in tree.DirectDescendants)
                {
                    List<TreeOfNodes<T>> descendantChain =
                        new List<TreeOfNodes<T>>(CalculateLongestPath(descendant));

                    if (descendantChain.Count > subChain.Count)
                    {
                        subChain = descendantChain;
                    }
                }

                chain.AddRange(subChain);
            }

            return chain;
        }

        public static IList<TreeOfNodes<T>> GetMiddleNodes<T>(TreeOfNodes<T> tree)
            where T : IComparable<T>
        {
            List<TreeOfNodes<T>> middleNodes = new List<TreeOfNodes<T>>();

            if (tree.Parent != null && tree.Size > 1)
            {
                middleNodes.Add(tree);

            }
            else if (tree.Size > 1)
            {
                foreach (TreeOfNodes<T> descendant in tree.DirectDescendants)
                {
                    IList<TreeOfNodes<T>> descendantMiddleNodes = GetMiddleNodes(descendant);
                    if (descendantMiddleNodes.Count > 0)
                    {
                        middleNodes.AddRange(descendantMiddleNodes);
                    }
                }
            }

            return middleNodes;
        }

        public static IList<TreeOfNodes<T>> GetLeafNodes<T>(TreeOfNodes<T> tree)
            where T : IComparable<T>
        {
            List<TreeOfNodes<T>> leafs = new List<TreeOfNodes<T>>();

            if (tree.Size > 1)
            {
                foreach (TreeOfNodes<T> descendant in tree.DirectDescendants)
                {
                    leafs.AddRange(GetLeafNodes<T>(descendant));
                }
            }
            else
            {
                leafs.Add(tree);
            }

            return leafs;
        }

        public static TreeOfNodes<T> AssociateNodes<T>(Queue<TreeOfNodes<T>> availableNodes)
            where T : IComparable<T>
        {
            TreeOfNodes<T> rootTree = availableNodes.Dequeue();

            // Max possible itreations if there are unmached elements at this point
            // They will never match so brake the loop
            int remainingIterations = availableNodes.Count * availableNodes.Count;

            while (availableNodes.Count > 0 && remainingIterations > 0)
            {
                TreeOfNodes<T> tree = availableNodes.Dequeue();

                TreeOfNodes<T> parent = rootTree.Contains(tree.Value);
                if (parent != null)
                {
                    parent.AddMany(tree.DirectDescendants);
                }
                else
                {
                    parent = tree.Contains(rootTree.Value);
                    if (parent != null)
                    {
                        parent.AddMany(rootTree.DirectDescendants);
                        rootTree = tree;
                    }
                    else
                    {
                        availableNodes.Enqueue(tree);
                    }
                }

                remainingIterations--;
            }

            return rootTree;
        }
    }
}