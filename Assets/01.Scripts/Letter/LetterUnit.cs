[System.Serializable]
public class LetterUnit
{
    public LetterUnit(LetterType type, string title, string desc, string date, string from){
        Type = type;
        Title = title;
        Desc = desc;
        Date = date;
        From = from;
    }

    public LetterType Type { get; private set; }
    public string Title { get; private set; }
    public string Desc { get; private set; }
    public string Date { get; private set; }
    public string From { get; private set; }
    public bool IsChecked { get; set; }
}
