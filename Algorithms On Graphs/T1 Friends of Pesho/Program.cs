namespace T1_Friends_of_Pesho
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Assignment URI http://bgcoder.com/Contests/Practice/Index/118#4
    /// </summary>
    class Program
    {
        private static int shortestTotalDistance = int.MaxValue;

        static void Main()
        {
            string[] tokens = Console.ReadLine().Split(' ');
            int totalLocations = int.Parse(tokens[0]),
                connections = int.Parse(tokens[1]),
                hospitals = int.Parse(tokens[2]);

            string[] hospitalNames = Console.ReadLine()
                .Split(' ');

            Dictionary<string, Location> locationsMap = ParseLocations(connections);

            CalculateDistanceToHomes(locationsMap, hospitalNames);

            CalculateTotals(locationsMap, hospitalNames);

            Console.WriteLine(shortestTotalDistance);
        }

        private static void CalculateTotals(
            Dictionary<string, Location> locationsMap, string[] hospitalNames)
        {
            int currentTotal = 0;

            foreach (var hospital in hospitalNames)
            {
                foreach (var pair in locationsMap[hospital].ShortestDistanceTo)
                {
                    if (hospitalNames.Contains(pair.Key))
                    {
                        continue;
                    }
                    else
                    {
                        currentTotal += pair.Value;
                        if (currentTotal >= shortestTotalDistance)
                        {
                            break;
                        }
                    }
                }

                if (currentTotal < shortestTotalDistance)
                {
                    shortestTotalDistance = currentTotal;
                }

                currentTotal = 0;
            }
        }

        private static void CalculateDistanceToHomes(
            Dictionary<string, Location> locationsMap, string[] hospitalNames)
        {
            foreach (var hospitalName in hospitalNames)
            {
                var hospital = locationsMap[hospitalName];

                CalculateDistanceToRest(
                    hospital, hospital.Connections, 0, new HashSet<string>());
            }
        }

        private static void CalculateDistanceToRest(
            Location current,
            Dictionary<string, Connection> connections,
            int currentDistance,
            HashSet<string> visited)
        {
            foreach (var name in connections.Keys)
            {
                if (visited.Contains(name))
                {
                    continue;
                }

                if (!current.ShortestDistanceTo.ContainsKey(name)
                    || (current.ShortestDistanceTo[name] >
                        (currentDistance + connections[name].Weight)))
                {
                    current.ShortestDistanceTo[name] =
                        currentDistance + connections[name].Weight;
                }

                visited.Add(name);
                CalculateDistanceToRest(current, connections[name].Endpoint.Connections, currentDistance + connections[name].Weight, visited);
                visited.Remove(name);
            }
        }

        private static Dictionary<string, Location> ParseLocations(int connections)
        {
            var locationsMap = new Dictionary<string, Location>();
            for (int i = 0; i < connections; i++)
            {
                string[] data = Console.ReadLine().Split(' ');
                if (!locationsMap.ContainsKey(data[0]))
                {
                    locationsMap[data[0]] = new Location(data[0]);
                }

                if (!locationsMap.ContainsKey(data[1]))
                {
                    locationsMap[data[1]] = new Location(data[1]);
                }

                locationsMap[data[0]].AddConnection(
                    locationsMap[data[1]], int.Parse(data[2]));
            }

            return locationsMap;
        }
    }

    class Location
    {
        public Location(string name)
        {
            this.Name = name;
            this.Connections = new Dictionary<string, Connection>();
            this.ShortestDistanceTo = new Dictionary<string, int>();
        }

        public bool IsHospital { get; set; }

        public string Name { get; set; }

        public Dictionary<string, Connection> Connections { get; set; }

        public Dictionary<string, int> ShortestDistanceTo { get; set; }

        public void AddConnection(Location endpoint, int weight)
        {
            this.Connections[endpoint.Name] = new Connection(endpoint, weight);

            if (!endpoint.Connections.ContainsKey(this.Name))
            {
                endpoint.AddConnection(this, weight);
            }
        }
    }

    class Connection
    {
        public Connection(Location endpoint, int weight)
        {
            this.Endpoint = endpoint;
            this.Weight = weight;
        }

        public int Weight { get; set; }

        public Location Endpoint { get; set; }
    }
}
