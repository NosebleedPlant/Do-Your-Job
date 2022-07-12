using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicketMiniGame : MonoBehaviour
{
    //store prefab
    //increment value
    //start position
    //array of current tickets
    //spawn a ticket
    [SerializeField]
    private Transform TicketPrefab;
    [SerializeField]
    private Transform TicketList;
    private List<Transform> _tickets = new List<Transform>();
    //private int _ticketNo = 203236;

    private void Awake()
    {
        SpawnTickets();
    }

    private void SpawnTickets()
    {
        if(_tickets.Count<4)
        {
            var ticket = Instantiate(TicketPrefab,TicketList);
            ticket.SetAsFirstSibling();
            _tickets.Add(ticket);
        }
    }

    public void DestroyTicket()
    {
        var ticket = _tickets[0];
        _tickets.RemoveAt(0);
        Destroy(ticket.gameObject);
    }
}
