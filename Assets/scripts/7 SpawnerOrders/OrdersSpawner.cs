using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersSpawner : MonoBehaviour
{
    [SerializeField] private List<Order>_orders;
    [SerializeField] private Transform _pointOrder;
    [SerializeField] private Transform _pointFinish;
    [SerializeField] private float _speed;
    [SerializeField] private Canvas _canvas;

    private List<Order> _validOrders = new List<Order>();
    private Coroutine _coroutine;

    private void Start()
    {
        StartCorutineOrder();
    }

    private void CreateOrder()
    {
        int number = Random.Range(0, _orders.Count);

        Order clon = _orders[number];

        if (clon.GetIsOpen() == true)
        {
            Order order = Instantiate(clon, _pointOrder.position, Quaternion.identity, _pointFinish);

            _validOrders.Add(order);
        }
        else
        {
            CreateOrder();
        }        
    }

    public Order RelevantOrder()
    {
        return _validOrders[_validOrders.Count - 1];
    }

    private void StartCorutineOrder()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(CameOrder());
    }

    private IEnumerator CameOrder()
    {
        yield return new WaitForSeconds(1f);

        CreateOrder();

        while (RelevantOrder().transform.position != _pointFinish.position)
        {
            RelevantOrder().transform.position = Vector3.MoveTowards(RelevantOrder().transform.position, _pointFinish.position, _speed * Time.deltaTime);

            yield return null;
        }
    }

    public void DestroyOrder()
    {
        Destroy(RelevantOrder().gameObject);
        _validOrders.Clear();

        StartCorutineOrder();
    }
}

