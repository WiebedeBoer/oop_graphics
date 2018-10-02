using System;
using System.Collections.Generic;

namespace Models
{
    class Dijkstra
    {
        Dictionary<string, Dictionary<string, int>> vertices = new Dictionary<string, Dictionary<string, int>>();

        public void add_vertex(string name, Dictionary<string, int> edges)
        {
            vertices[name] = edges;
        }

        public List<string> shortest_path(string start, string finish)
        {
            var previous = new Dictionary<string, string>();
            var distances = new Dictionary<string, int>();
            var nodes = new List<string>();

            List<string> path = null;

            foreach (var vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == finish)
                {
                    path = new List<string>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }
            path.Reverse();
            return path;
        }
    }

 
    class GraphClass
    {
        public Dijkstra g = new Dijkstra();
            public GraphClass() {
                g.add_vertex("vrachtdepot", new Dictionary<string, int>() {{"RA", 1}, {"LA", 1}});
                g.add_vertex("RA", new Dictionary<string, int>() {{"vrachtdepot", 1}, {"RB", 2}});
                g.add_vertex("RB", new Dictionary<string, int>() {{"RA", 2}, {"RC", 2}});
                g.add_vertex("RC", new Dictionary<string, int>() {{"RC", 2}, {"RD", 2}});
                g.add_vertex("RD", new Dictionary<string, int>() {{"RC", 2}});
                g.add_vertex("LA", new Dictionary<string, int>() {{"vrachtdepot", 1}, {"LB", 2}});
                g.add_vertex("LB", new Dictionary<string, int>() {{"LA", 2}, {"LC", 2}});
                g.add_vertex("LC", new Dictionary<string, int>() {{"LC", 2}, {"LD", 2}});
                g.add_vertex("LD", new Dictionary<string, int>() {{"LC", 2}});
            }
            public List<string> GetPath(string startingPlace, string destination) {
                //g.shortest_path('A', 'H').ForEach( x => Console.WriteLine(x) );
                return(g.shortest_path(startingPlace, destination));
            }
    }
    
}