using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFurniture : MonoBehaviour
{
    [SerializeField] private Transform _pointStartStack;
    [SerializeField] private int _maxCountFurniture;

    private List<Furniture> _furnitures = new List<Furniture>();

    public bool IsFull { get; private set; } = false;

    private void Start()
    {
        
    }

    public void RemoveFurniture(Furniture furniture, Transform pointDestroy)
    {
        if (furniture == null) return;

        furniture.transform.SetParent(null);

        furniture.transform.DOJump(pointDestroy.position, 1, 1, 0.5f).OnComplete(
            () =>
            {
                RemoveFurnitur(furniture);
                Destroy(furniture.gameObject);
            });
    }

    public Transform GetTransform()
    {
        return _pointStartStack;
    }

    public List<Furniture> GetListStack()
    {
        return _furnitures;
    }

    public Furniture GetFurniture()
    {
        if(_furnitures.Count == 0)
        {
            return null;
        }

        return _furnitures[_furnitures.Count - 1];
    }

    public void AddFurnitur(Furniture furniture)
    {
         _furnitures.Add(furniture);
         IsFull = true;      
    }

    public void RemoveFurnitur(Furniture furniture)
    {
         _furnitures.Remove(furniture);
         IsFull = false;
    }
}
