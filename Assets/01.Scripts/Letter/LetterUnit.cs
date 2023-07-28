using Core;

[System.Serializable]
public class LetterUnit
{
    public void Setup(LetterType type, string title, string desc, GameDate date, string from){
        Type = type;
        Title = title;
        Desc = desc;
        Date = date;
        From = from;
    }

    public LetterType Type { get; private set; }
    public string Title { get; private set; }
    public string Desc { get; private set; }
    public GameDate Date { get; private set; }
    public string From { get; private set; }
    public bool IsChecked { get; set; }
}
