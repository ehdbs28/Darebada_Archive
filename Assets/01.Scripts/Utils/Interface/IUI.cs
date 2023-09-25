using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public interface IUI
{
    public void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true);
    public void FindElement();
    public void GenerateRoot();
    public void RemoveRoot();
    public void AddEvent();
    public void RemoveEvent();
}
