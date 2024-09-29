using UnityEngine;

public class PointOff : MonoBehaviour
{
    public static PointOff Instance { get; private set; }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}