using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Complaint_SubManager : MonoBehaviour
{

    [SerializeField] public GameStatusData gameData;
    [SerializeField] private Transform TicketPrefab;
    [SerializeField] private Transform TicketList;
    private List<Transform> _tickets = new List<Transform>();
    private int ticketNumber = 203236;

    private void OnEnable() =>  StartCoroutine(SpawnRoutine());

    private void OnDisable() =>  StopAllCoroutines();

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(0);

        while(enabled)
        {
            if(!gameData.ComplaintGameData.MaxReached)
            {
                gameData.ComplaintGameData.ComplaintCount++;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Update()
    {
        if(!gameData.ComplaintGameData.MaxVisableReached)
        {
            SpawnTickets();
        }
    }

    private void SpawnTickets()
    {
        var ticket = Instantiate(TicketPrefab,TicketList);
        ticket.SetAsFirstSibling();
        _tickets.Add(ticket);
        gameData.ComplaintGameData.CurrentVisableCount++;
    }

    public void DestroyTicket()
    {
        var ticket = _tickets[0];
        _tickets.RemoveAt(0);
        Destroy(ticket.gameObject);
        gameData.ComplaintGameData.CurrentVisableCount--;
        gameData.ComplaintGameData.ComplaintCount--;
    }
}
