using System;
using System.Collections.Generic;
using System.Diagnostics;

class StackArray
{
    private List<int> data = new List<int>();

    public bool IsEmpty()
    {
        return data.Count == 0;
    }

    public void Push(int value)
    {
        data.Add(value);
    }

    public int? GetLastElement()
    {
        if (IsEmpty()) return null;
        return data[^1]; // data[data.Count - 1]
    }

    public void Pop()
    {
        if (!IsEmpty())
        {
            data.RemoveAt(data.Count - 1);
        }
    }
}

class Node
{
    public int Value;
    public Node Next;

    public Node(int value, Node next = null)
    {
        Value = value;
        Next = next;
    }
}

class StackLinkedList
{
    private Node top;

    public bool IsEmpty()
    {
        return top == null;
    }

    public void Push(int value)
    {
        top = new Node(value, top);
    }

    public int? GetLastElement()
    {
        if (IsEmpty()) return null;
        return top.Value;
    }

    public void Pop()
    {
        if (!IsEmpty())
        {
            top = top.Next;
        }
    }
}

class Program
{
    static void Main()
    {
        var stackArray = new StackArray();
        stackArray.Push(10);
        stackArray.Push(20);
        stackArray.Push(30);

        Console.WriteLine("StackArray top element: " + stackArray.GetLastElement());
        stackArray.Pop();
        Console.WriteLine("StackArray top after pop: " + stackArray.GetLastElement());

        var stackLinkedList = new StackLinkedList();
        stackLinkedList.Push(100);
        stackLinkedList.Push(200);
        stackLinkedList.Push(300);

        Console.WriteLine("StackLinkedList top element: " + stackLinkedList.GetLastElement());
        stackLinkedList.Pop();
        Console.WriteLine("StackLinkedList top after pop: " + stackLinkedList.GetLastElement());
    }
}
