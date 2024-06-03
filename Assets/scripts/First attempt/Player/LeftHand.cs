using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeftHand : MonoBehaviour
{
    public static LeftHand Instance { get; private set;}

    [SerializeField] private BoardsPlayer _boardsPlayer;

    public LaborMovement ActiveLaborMovement { get; private set; }

    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.TryGetComponent<Chair—reationMovements>(out var chair—reationMovements) == true)
        {
            _boardsPlayer.AddBoard(chair—reationMovements);
        }  
        
        if(collider.transform.TryGetComponent<LaborMovement>(out var laborMovement) == true)
        {
            if (laborMovement != ActiveLaborMovement)
            {
                Debug.LogWarning("Bordres not equal!");
            }
            ActiveLaborMovement = null;
            laborMovement.enabled = false;
        }
    }

    public void SetActiveBoart(LaborMovement labor)
    {
        ActiveLaborMovement = labor;
    }
}
