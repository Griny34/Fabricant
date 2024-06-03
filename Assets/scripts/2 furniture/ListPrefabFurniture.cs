using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ListPrefabFurniture", menuName = "Furninure/Create new Furniture", order = 51)]
public class ListPrefabFurniture : ScriptableObject
{
    [SerializeField] private List<Furniture> _furnitureList;

    public List<Furniture> GetListPrefabFurniture()
    {
        return _furnitureList;
    }
}
