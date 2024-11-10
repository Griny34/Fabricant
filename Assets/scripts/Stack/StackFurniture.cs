using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackFurniture : MonoBehaviour
{
    [SerializeField] private Transform _pointStartStack;

    private List<Furniture> _furnitures = new List<Furniture>();
    private float _delay = 0.5f;
    private float _powerJump = 1;
    private int _numberJumps = 1;

    public bool IsFull { get; private set; } = false;

    public void RemoveFurniture(Furniture furniture, Transform pointDestroy)
    {
        if (furniture == null) 
            return;

        furniture.transform.SetParent(null);

        furniture.transform.DOJump(
            pointDestroy.position,
            _powerJump, _numberJumps,
            _delay).OnComplete(() =>
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
        if (_furnitures.Count == 0)
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
