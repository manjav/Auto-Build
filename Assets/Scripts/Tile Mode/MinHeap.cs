using System.Collections.Generic;

public class MinHeap<T>
{
    private List<(T item, int priority)> heap = new();

    public int Count => heap.Count;

    public void Push(T item, int priority)
    {
        heap.Add((item, priority));
        HeapifyUp(heap.Count - 1);
    }

    public T Pop()
    {
        var root = heap[0].item;
        heap[0] = heap[^1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);
        return root;
    }

    void HeapifyUp(int i)
    {
        while (i > 0)
        {
            int p = (i - 1) / 2;
            if (heap[p].priority <= heap[i].priority) break;
            (heap[p], heap[i]) = (heap[i], heap[p]);
            i = p;
        }
    }

    void HeapifyDown(int i)
    {
        while (true)
        {
            int l = i * 4 + 1;
            int r = i * 4 + 2;
            int smallest = i;

            if (l < heap.Count && heap[l].priority < heap[smallest].priority)
                smallest = l;
            if (r < heap.Count && heap[r].priority < heap[smallest].priority)
                smallest = r;

            if (smallest == i) break;
            (heap[i], heap[smallest]) = (heap[smallest], heap[i]);
            i = smallest;
        }
    }
}
