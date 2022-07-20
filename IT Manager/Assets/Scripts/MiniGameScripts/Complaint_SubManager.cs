using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Complaint_SubManager : MonoBehaviour
{

    [SerializeField] public GameStatusData gameData;
    [SerializeField] private Complaint_List ComplaintList;
    private int ticketNumber = 203236;

    private void OnEnable() 
    {
        StartCoroutine(AddTickets());
        StartCoroutine(SpawnTickets());
    }

    private void OnDisable() =>  StopAllCoroutines();

    private IEnumerator AddTickets()
    {
        yield return new WaitForSeconds(0);

        while(enabled)
        {
            if(!gameData.ComplaintGameData.MaxReached)
            {
                gameData.ComplaintGameData.ComplaintCount++;
            }
            yield return new WaitForSeconds(gameData.ComplaintGameData.SpawnRate);
        }
    }

    private int _visableMaxCount = 4;
    private int _currentVisableCount = 0;
    private float _wait = 0;
    private IEnumerator SpawnTickets()
    {
        while(enabled)
        {
            if(_currentVisableCount<_visableMaxCount && _currentVisableCount<gameData.ComplaintGameData.ComplaintCount)
            {
                _currentVisableCount++;
                ComplaintList.InstanceComplaint();
                _wait = 0;
            }
            yield return new WaitForSeconds(_wait);
        }
    }

    public void DestroyTicket()
    {
        ComplaintList.DeleteComplaint();
        _currentVisableCount--;
        gameData.ComplaintGameData.ComplaintCount--;
        _wait = 0.5f;
    }
}
