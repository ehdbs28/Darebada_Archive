using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HighlightHierarchy : MonoBehaviour
{
#if UNITY_EDITOR
    private static Dictionary<Object, HighlightHierarchy> coloredObjects = new Dictionary<Object, HighlightHierarchy>();

    static HighlightHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceId, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceId);

        if(obj != null && coloredObjects.ContainsKey(obj))
        {
            GameObject gObj = obj as GameObject;
            HighlightHierarchy ch = gObj.GetComponent<HighlightHierarchy>();
            if(ch != null)
            {
                PaintObject(obj, selectionRect, ch);
            }
            else
            {
                coloredObjects.Remove(obj); //이제 없으니 제거
            }
        }
            
    }

    private static void PaintObject(Object obj, Rect selectionRect, HighlightHierarchy ch)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        if (Selection.activeObject != obj)
        {
            EditorGUI.DrawRect(bgRect, ch.backColor);

            string name = $"{ch.prefix}  {obj.name}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = ch.fontColor },
                fontStyle = FontStyle.Bold
            });
        }
    }

    public string prefix;
    public Color backColor;
    public Color fontColor;

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (false == coloredObjects.ContainsKey(this.gameObject))
        {
            coloredObjects.Add(this.gameObject, this);
        }
    }
#endif
}
