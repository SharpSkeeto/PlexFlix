using Microsoft.UI.Xaml.Data;
using System;
using System.Collections;
using System.Collections.Specialized;
using Windows.Foundation;

namespace MyPlexManager.Interfaces;

public interface IIncrementalLoadingBase
{
    object this[int index] { get; set; }

    int Count { get; }
    bool HasMoreItems { get; }
    bool IsFixedSize { get; }
    bool IsReadOnly { get; }
    bool IsSynchronized { get; }
    object SyncRoot { get; }

    event NotifyCollectionChangedEventHandler? CollectionChanged;

    int Add(object? value);
    void Clear();
    bool Contains(object? value);
    void CopyTo(Array array, int index);
    IEnumerator GetEnumerator();
    int IndexOf(object? value);
    void Insert(int index, object? value);
    IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count);
    void Remove(object? value);
    void RemoveAt(int index);
}