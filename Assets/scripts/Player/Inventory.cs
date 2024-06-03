using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _pointPrefab;
    [SerializeField] private float _interval;
    [SerializeField] protected float _maxCountItem;
    [SerializeField] private Transform _newPointPosition;

    private List<Item> _items = new List<Item>();
    private List<GameObject> _points = new List<GameObject>();

    public bool IsFull => _items.Count >= _maxCountItem;

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
        item.transform.SetParent(null);

        _newPointPosition.position -= Vector3.up * _interval;
        
        Destroy(_points[_points.Count - 1]);
        _points.Remove(_points[_points.Count - 1]);
    }

    public List<Item> GetListChair()
    {
        return _items;
    }

    public Item GetLastItem()
    {
        if (_items.Count <= 0)
        {
            return null;
        }

        return _items[_items.Count - 1];
    }

    public void AddItem(Item item)
    {
        if (_maxCountItem > _items.Count)
        {           
            _items.Add(item);
            item.StartMove(CreatePoint());
            item.transform.SetParent(gameObject.transform, true);                 
        }
    }

    private Transform CreatePoint()
    {
        GameObject point = Instantiate(_pointPrefab.gameObject);

        point.transform.SetParent(transform, true);

        point.transform.name = _points.Count.ToString();
        
        _points.Add(point);

        point.transform.position = _newPointPosition.position;

        _newPointPosition.position += Vector3.up * _interval;

        return point.transform;
    }
}
