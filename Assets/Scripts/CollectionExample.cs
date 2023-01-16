using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectionExample : MonoBehaviour
{
    public Book[] _array;

    private void Start()
    {
        _array = new Book[10];

        for (int i = 0; i < 10; i++)
        {
            _array[i] = new Book(Random.Range(0.99f, 19.99f), "test", Random.Range(100, 200));
        }
        
        Debug.Log(_array[Random.Range(0, _array.Length)].Price);
    }
}

[Serializable]
public class Book
{
    public float Price;
    public string Title;
    public int PageCount;

    public Book(float price, string title, int pageCount)
    {
        Price = price;
        Title = title;
        PageCount = pageCount;
    }
}
