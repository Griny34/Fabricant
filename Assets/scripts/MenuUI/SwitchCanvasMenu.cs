using UnityEngine;

public class SwitchCanvasMenu : MonoBehaviour
{
    [SerializeField] private GameObject _portrait;
    [SerializeField] private GameObject _landscape;

    private void Update()
    {
        if (Screen.width < Screen.height)
        {
            _portrait.SetActive(true);
            _landscape.SetActive(false);
            SwitchVerticalView();
        }
        else
        {
            _portrait.SetActive(false);
            _landscape.SetActive(true);
            SwitchHorizontalView();
        }
    }

    protected virtual void SwitchVerticalView()
    {
    } 
    
    protected virtual void SwitchHorizontalView()
    {
    }
}