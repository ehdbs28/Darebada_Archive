using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Unity.VisualScripting;

public class LetterManager : MonoBehaviour,IManager
{
    [SerializeField]private List<LetterUnit> _letters;
    [SerializeField] private LetterTemplateSO ThanksTemplate;
    [SerializeField] private LetterTemplateSO RequestTemplate;
    public LetterManager(){
        ResetManager();
    }

    public void InitManager()
    {

    }

    public void UpdateManager()
    {
    }
    public void SendRequestLetter(int requestId)
    {
        Debug.Log("�ο� �߰���-�̽�-");

        string title = RequestTemplate.titles[requestId];
        string desc = RequestTemplate.descs[requestId];
        string date = $"{GameManager.Instance.GetManager<TimeManager>().DateTime.Month}M {GameManager.Instance.GetManager<TimeManager>().DateTime.Day}D";
        string from = "AquariumManager";
        LetterUnit letter = new LetterUnit();
        letter.Setup(LetterType.REQUEST, title, desc, date, from);
        AddLetter(letter);
    }
    public void SendReviewLetter()
    {
        Debug.Log("���� �߰���-�̽�-");
        int id = Random.Range(0, ThanksTemplate.titles.Count);
        string title = ThanksTemplate.titles[id];
        string desc = ThanksTemplate.descs[id];
        string date = $"{GameManager.Instance.GetManager<TimeManager>().DateTime.Month}M {GameManager.Instance.GetManager<TimeManager>().DateTime.Day}D";
        string from = ThanksTemplate.froms[Random.Range(0,ThanksTemplate.froms.Count)];
        LetterUnit letter = new LetterUnit();
        letter.Setup(LetterType.THANKS,title, desc, date, from);
        AddLetter(letter);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {

            SendReviewLetter();
        }else if(Input.GetKeyDown(KeyCode.J))
        {
            SendRequestLetter(0);
        }else if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(_letters[0].Date);
            Debug.Log(_letters[0].Title);
            Debug.Log(_letters[0].Desc);
            Debug.Log(_letters[0].From);
            _letters.RemoveAt(0);
        }
    }
    public void SendReportLetter(int entranceRevenue, int etcRevenue, int managerSalary, int employeeSalary, int manageCost)
    {

        string date = $"{GameManager.Instance.GetManager<TimeManager>().DateTime.Month}M {GameManager.Instance.GetManager<TimeManager>().DateTime.Day}D";
        string title = date + "";//InsertTitle
        string desc = $"{entranceRevenue}\n{etcRevenue}\n\n{managerSalary}\n{employeeSalary}\n{manageCost}";//InsertDesc
        string from = "AquariumManager";
        LetterUnit letter = new LetterUnit();
        letter.Setup(LetterType.THANKS, title, desc, date, from);
        AddLetter(letter);
    }

    private void AddLetter(LetterUnit data){
        _letters.Add(data);
    }

    public void ResetManager()
    {
        _letters = new List<LetterUnit>();
    }
}
