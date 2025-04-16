using System;
using System.Diagnostics;

public class SingleList<T>
{
    public class Element
    {
        public T Data;
        public Element NextElement;
        public Element(T data) { Data = data; NextElement = null; }
    }

    public Element First { get; private set; }
    public Element Last { get; private set; }
    public int Size { get; private set; } = 0;

    public Element AddAfter(Element reference, T data)
    {
        var element = new Element(data);
        if (reference == null)
        {
            element.NextElement = First;
            First = element;
            if (Last == null) Last = element;
        }
        else
        {
            element.NextElement = reference.NextElement;
            reference.NextElement = element;
            if (reference == Last) Last = element;
        }
        Size++;
        return element;
    }

    public (Element before, Element match) Search(T data)
    {
        Element previous = null, current = First;
        while (current != null)
        {
            if (current.Data.Equals(data)) return (previous, current);
            previous = current;
            current = current.NextElement;
        }
        return (null, null);
    }

    public void DeleteAfter(Element reference)
    {
        if (reference == null)
        {
            if (First != null) First = First.NextElement;
            if (First == null) Last = null;
        }
        else if (reference.NextElement != null)
        {
            if (reference.NextElement == Last) Last = reference;
            reference.NextElement = reference.NextElement.NextElement;
        }
        Size--;
    }

    public void ValidateNoCycle()
    {
        int traversed = 0;
        var node = First;
        while (node != null)
        {
            traversed++;
            node = node.NextElement;
            Debug.Assert(traversed <= Size, "Обнаружен цикл в однонаправленном списке!");
        }
    }
}

public class DoubleList<T>
{
    public class Cell
    {
        public T Content;
        public Cell NextCell, PrevCell;
        public Cell(T content) { Content = content; }
    }

    public Cell Start { get; private set; }
    public Cell End { get; private set; }
    public int Length { get; private set; } = 0;

    public Cell AppendAfter(Cell point, T content)
    {
        var newCell = new Cell(content);
        if (point == null)
        {
            newCell.NextCell = Start;
            if (Start != null) Start.PrevCell = newCell;
            Start = newCell;
            if (End == null) End = newCell;
        }
        else
        {
            newCell.NextCell = point.NextCell;
            newCell.PrevCell = point;
            if (point.NextCell != null) point.NextCell.PrevCell = newCell;
            point.NextCell = newCell;
            if (point == End) End = newCell;
        }
        Length++;
        return newCell;
    }

    public Cell PrependBefore(Cell point, T content)
    {
        var newCell = new Cell(content);
        if (point == null || point == Start)
        {
            newCell.NextCell = Start;
            if (Start != null) Start.PrevCell = newCell;
            Start = newCell;
            if (End == null) End = newCell;
        }
        else
        {
            newCell.PrevCell = point.PrevCell;
            newCell.NextCell = point;
            point.PrevCell.NextCell = newCell;
            point.PrevCell = newCell;
        }
        Length++;
        return newCell;
    }

    public Cell Locate(T content)
    {
        var current = Start;
        while (current != null)
        {
            if (current.Content.Equals(content)) return current;
            current = current.NextCell;
        }
        return null;
    }

    public void Detach(Cell cell)
    {
        if (cell == null) return;
        if (cell == Start) Start = cell.NextCell;
        if (cell == End) End = cell.PrevCell;
        if (cell.PrevCell != null) cell.PrevCell.NextCell = cell.NextCell;
        if (cell.NextCell != null) cell.NextCell.PrevCell = cell.PrevCell;
        Length--;
    }

    public void ValidateStructure()
    {
        int checkedCount = 0;
        var node = Start;
        while (node != null)
        {
            checkedCount++;
            node = node.NextCell;
            Debug.Assert(checkedCount <= Length, "Обнаружен цикл в двусвязном списке!");
        }
    }
}

class EntryPoint
{
    static void Main()
    {
        var list1 = new SingleList<int>();
        var a = list1.AddAfter(null, 5);
        var b = list1.AddAfter(a, 10);
        list1.AddAfter(b, 15);
        Debug.Assert(list1.Search(10).match != null);
        list1.DeleteAfter(a);
        list1.ValidateNoCycle();

        var list2 = new DoubleList<int>();
        var c = list2.AppendAfter(null, 100);
        var d = list2.AppendAfter(c, 200);
        list2.PrependBefore(d, 150);
        Debug.Assert(list2.Locate(150) != null);
        list2.Detach(c);
        list2.ValidateStructure();

        Console.WriteLine("Все проверки завершены успешно.");
    }
}
