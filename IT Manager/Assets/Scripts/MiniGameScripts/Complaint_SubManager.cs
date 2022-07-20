using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Complaint_SubManager : MonoBehaviour
{

    [SerializeField] public GameStatusData gameData;
    [SerializeField] private RectTransform complaintPrefab;
    [SerializeField] private RectTransform complaintContainer;
    public List<RectTransform> complaints;
    private float _height, _start = 121.415f;
    private int ticketNumber = 203236;
    private int _visableMaxCount = 4, _currentVisableCount = 0;
    
    void Start()
    {
        _height  = (complaintContainer.sizeDelta.y)/2;
    }

    private void OnEnable() => StartCoroutine(AddTickets());

    private void OnDisable() =>  StopAllCoroutines();
    
    private void Update()
    {
        if(_currentVisableCount<_visableMaxCount && _currentVisableCount<gameData.ComplaintGameData.ComplaintCount)
        {
            _currentVisableCount++;
            InstanceComplaint();
        }
    }

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

    public void DestroyTicket()
    {
        DeleteComplaint();
        
    }

    public void InstanceComplaint()
    {
        RectTransform complaintObject = Instantiate(complaintPrefab,complaintContainer);
        complaintObject.localPosition = new Vector3(0,_start,0);
        complaintObject.localScale = Vector3.zero;
        complaints.Add(complaintObject);
        // tweenPosition(complaintObject);
        LeanTween.scale(complaintObject,new Vector3(0.5f,0.5f,0.5f),0.1f).setOnComplete(
            ()=>
            {
                float newPosition = -_height+ (((complaintPrefab.sizeDelta.y/2)+10)*(complaints.FindIndex(a=>a==complaintObject)));
                LeanTween.moveLocal(complaintObject.gameObject,new Vector3(0,newPosition,0),0.5f).setEaseOutBounce();
            }
        );
    }

    public void DeleteComplaint()
    {
        if(complaints.Count==0){return;}
        
        LeanTween.scale(complaints[0].gameObject,Vector3.zero,0.1f).setOnComplete(
            ()=>
            {
                Destroy(complaints[0].gameObject);
                complaints.RemoveAt(0);
                int i =0;
                foreach(RectTransform complaint in complaints)
                {
                    i++;
                    float newPosition = -_height+ (((complaintPrefab.sizeDelta.y/2)+10)*(complaints.FindIndex(a=>a==complaint)));
                    if(i<complaints.Count) LeanTween.moveLocal(complaint.gameObject,new Vector3(0,newPosition,0),0.5f).setEaseOutBounce();
                    else LeanTween.moveLocal(complaint.gameObject,new Vector3(0,newPosition,0),0.5f).setEaseOutBounce().setOnComplete
                    (
                        ()=>
                        {
                            _currentVisableCount--;
                            gameData.ComplaintGameData.ComplaintCount--;
                        }
                    );
                }
            }
        );
    }  
}
