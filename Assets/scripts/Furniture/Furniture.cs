using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    
    public string GetName()
    {
        return _name;
    }
  
    public int GetVolumePrice()
    {
        return _price;
    }
}