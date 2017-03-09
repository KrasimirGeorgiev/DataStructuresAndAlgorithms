namespace TreeOfNodes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using HomeworkHelpers;

    /// <summary>
    /// You are given a tree of N nodes represented as a set of N-1 pairs of nodes 
    /// (parent node, child node), each in the range (0..N-1).
    /// 
    /// Write a program to read the tree and find:
    ///     the root node done
    ///     all leaf nodes done
    ///     all middle nodes done
    ///     the longest path in the tree done
    ///     (*) all paths in the tree with given sum `S` of their nodes todo
    ///     (*) all subtrees with given sum `S` of their nodes todo
    /// </summary>
    public class Program
    {
        private static HomeworkHelper helper = new HomeworkHelper();

        [STAThread]
        private static void Main()
        {
            helper.ConsoleMio.Setup();

            helper.ConsoleMio.PrintHeading("Task 1 Tree Of N Nodes");

            Queue<TreeOfNodes<int>> availableNodes = ParseUserInput(
                template: 0, splitChars: new[] { ' ' });

            TreeOfNodes<int> tree = TreeProcessing.AssociateNodes(availableNodes);

            Console.WriteLine();
            helper.ConsoleMio.WriteLine("Nodes Structure\n{node} -> {subNodes}", ConsoleColor.DarkBlue);
            Console.WriteLine(tree.ToString());

            helper.ConsoleMio.WriteLine("Root Node: {0}", ConsoleColor.DarkGreen, tree.Value);

            IList<TreeOfNodes<int>> leafNodes = TreeProcessing.GetLeafNodes(tree);
            helper.ConsoleMio.WriteLine(
                "Leaf Nodes: {0}", ConsoleColor.DarkGreen, string.Join(", ", leafNodes));

            IList<TreeOfNodes<int>> middleNodes = TreeProcessing.GetMiddleNodes(tree);
            helper.ConsoleMio.WriteLine(
                "Middle Nodes: {0}",
                ConsoleColor.DarkGreen,
                string.Join(", ", middleNodes.Select(n => n.Value)));

            IList<TreeOfNodes<int>> longestPath = TreeProcessing.CalculateLongestPath(tree);
            helper.ConsoleMio.WriteLine(
                "Longest Path: {0}",
                ConsoleColor.DarkGreen,
                string.Join(" -> ", longestPath.Select(n => n.Value)));
        }


        private static Queue<TreeOfNodes<T>> ParseUserInput<T>(T template, char[] splitChars)
            where T : IComparable<T>
        {
            try
            {
                Clipboard.SetText(File.ReadAllText("sampleInput.txt"));
                helper.ConsoleMio.WriteLine(
                "You can paste the sample input copied to your clipboard", ConsoleColor.DarkRed);
            }
            catch (IOException)
            {
            }

            Queue<TreeOfNodes<T>> createdNodes = new Queue<TreeOfNodes<T>>();
            HashSet<T> distinctNodes = new HashSet<T>();

            helper.ConsoleMio.Write("Enter total count of nodes N = ", ConsoleColor.DarkRed);
            int remainingNodes = int.Parse(helper.ConsoleMio.ReadInColor(ConsoleColor.DarkBlue));

            while (remainingNodes > 0)
            {
                helper.ConsoleMio.Write(
                    "Remaining Nodes(Repeating nodes are exluded): ",
                    ConsoleColor.DarkCyan);
                helper.ConsoleMio.WriteLine(remainingNodes.ToString(), ConsoleColor.DarkRed);

                helper.ConsoleMio.Write("Enter a sequence of nodes (first will be root): ", ConsoleColor.DarkCyan);

                int newNodesCount;
                T[] nodesToBe = GetNodesToBe(splitChars, distinctNodes, out newNodesCount);

                if (newNodesCount > remainingNodes)
                {
                    throw new ApplicationException(
                        nodesToBe.Length +
                        " Nodes were entered, but there is only space for " + remainingNodes +
                        " nodes");
                }

                remainingNodes -= newNodesCount;

                var rootNode = new TreeOfNodes<T>(nodesToBe[0]);
                for (int i = 1; i < nodesToBe.Length; i++)
                {
                    rootNode.Add(nodesToBe[i]);
                }

                createdNodes.Enqueue(rootNode);
            }

            return createdNodes;
        }

        private static T[] GetNodesToBe<T>(
            char[] splitChars, HashSet<T> distinctNodes, out int newNodesCount)
            where T : IComparable<T>
        {
            T[] nodesToBe = helper.ConsoleMio.ReadInColor(ConsoleColor.DarkBlue)
                .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
                .Select(element => (T)Convert.ChangeType(element, typeof(T)))
                .ToArray();

            newNodesCount = nodesToBe.Length;

            foreach (var element in nodesToBe)
            {
                if (distinctNodes.Contains(element))
                {
                    newNodesCount--;
                }

                distinctNodes.Add(element);
            }

            return nodesToBe;
        }
    }
}
