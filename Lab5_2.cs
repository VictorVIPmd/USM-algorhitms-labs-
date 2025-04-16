using System;

class Node
{
    public int Value;
    public Node Next;

    public Node(int value)
    {
        Value = value;
        Next = null;
    }
}

class Queue
{
    private Node front;
    private Node rear;
    private int size;

    public bool IsEmpty()
    {
        return front == null;
    }

    public void Enqueue(int value)
    {
        var newNode = new Node(value);
        if (IsEmpty())
        {
            front = newNode;
            rear = newNode;
        }
        else
        {
            rear.Next = newNode;
            rear = newNode;
        }
        size++;
    }

    public int? Front()
    {
        return IsEmpty() ? null : front.Value;
    }

    public void Dequeue()
    {
        if (IsEmpty()) return;

        front = front.Next;
        if (front == null)
        {
            rear = null;
        }
        size--;
    }

    public int Size => size;
}

class Program
{
    static void Main()
    {
        var queue = new Queue();

        queue.Enqueue(10);
        queue.Enqueue(20);
        queue.Enqueue(30);

        Console.WriteLine("Front of queue: " + queue.Front());

        queue.Dequeue();
        Console.WriteLine("Front of queue after dequeue: " + queue.Front());

        queue.Dequeue();
        queue.Dequeue();
        Console.WriteLine("Is the queue empty? " + (queue.IsEmpty() ? "Yes" : "No"));
    }
}
