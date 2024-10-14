using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private string _name;

    private int _volumePrice;

    public string GetName()
    {
        return _name;
    }
  
    public int GiveVolumePrice()
    {
        return GetPrice();
    }

    protected virtual int GetPrice()
    {
        return _volumePrice;
    }
}
