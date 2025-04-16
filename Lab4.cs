using System;
using System.Collections.Generic;

class GraphNode
{
    public int Value;
    public List<GraphNode> Neighbors = new List<GraphNode>();

    public GraphNode(int value)
    {
        Value = value;
    }
}

class Program
{
    static void AddUndirectedEdge(GraphNode a, GraphNode b)
    {
        if (!a.Neighbors.Contains(b)) a.Neighbors.Add(b);
        if (!b.Neighbors.Contains(a)) b.Neighbors.Add(a);
    }

    static void PrintNeighborsOfNeighbors(GraphNode node)
    {
        var seen = new HashSet<GraphNode>();
        foreach (var neighbor in node.Neighbors)
        {
            foreach (var neighborsNeighbor in neighbor.Neighbors)
            {
                if (!seen.Contains(neighborsNeighbor) && neighborsNeighbor != node)
                {
                    Console.WriteLine(neighborsNeighbor.Value);
                    seen.Add(neighborsNeighbor);
                }
            }
        }
    }

    static int SumOfNeighbors(GraphNode node)
    {
        int sum = 0;
        foreach (var neighbor in node.Neighbors)
        {
            sum += neighbor.Value;
        }
        return sum;
    }

    static void DFS(GraphNode node, HashSet<GraphNode> visited)
    {
        if (visited.Contains(node)) return;

        Console.WriteLine(node.Value);
        visited.Add(node);

        foreach (var neighbor in node.Neighbors)
        {
            DFS(neighbor, visited);
        }
    }

    static void BFS(GraphNode start)
    {
        var visited = new HashSet<GraphNode>();
        var queue = new Queue<GraphNode>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (visited.Contains(current)) continue;

            Console.WriteLine(current.Value);
            visited.Add(current);

            foreach (var neighbor in current.Neighbors)
            {
                if (!visited.Contains(neighbor))
                    queue.Enqueue(neighbor);
            }
        }
    }

    static void PrintGraph(GraphNode start)
    {
        var visited = new HashSet<GraphNode>();
        var queue = new Queue<GraphNode>();
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            Console.Write($"{node.Value}: ");
            foreach (var neighbor in node.Neighbors)
            {
                Console.Write($"{neighbor.Value} ");
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
            Console.WriteLine();
        }
    }

    static void Main()
    {
        var n1 = new GraphNode(1);
        var n2 = new GraphNode(2);
        var n3 = new GraphNode(3);
        var n4 = new GraphNode(4);

        AddUndirectedEdge(n1, n2);
        AddUndirectedEdge(n1, n3);
        AddUndirectedEdge(n1, n4);
        AddUndirectedEdge(n2, n3);
        AddUndirectedEdge(n3, n4);

        Console.WriteLine("Соседи соседей узла 2:");
        PrintNeighborsOfNeighbors(n2);

        Console.WriteLine("\nСумма значений соседей узла 3:");
        Console.WriteLine(SumOfNeighbors(n3));

        Console.WriteLine("\nDFS от узла 1:");
        DFS(n1, new HashSet<GraphNode>());

        Console.WriteLine("\nBFS от узла 1:");
        BFS(n1);

        Console.WriteLine("\nВизуализация графа:");
        PrintGraph(n1);
    }
}