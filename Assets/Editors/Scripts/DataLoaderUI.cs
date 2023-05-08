#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using Unity.EditorCoroutines.Editor;

public class DataLoaderUI : EditorWindow
{
    [MenuItem("Tools/Data Loader")]
    public static void OpenWindow(){
        EditorWindow window = GetWindow<DataLoaderUI>();
        window.titleContent = new GUIContent("Spreadsheet Data Loader");
    }

    private DataLoader _loader;

    private TextField _urlText;

    #region Variable about Log-Viewer
    private Label _logTitle;
    private Label _logType;
    private Label _logPath;

    private Label _logLabel;

    private Label _innerData;
    #endregion

    private Foldout _dataElementParent;

    private void CreateGUI(){
        string editorPath = "Assets/Editors/";
        string documentPath = $"{editorPath}UI/DataLoader.uxml";

        _loader = new DataLoader();
        VisualTreeAsset documentUI = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(documentPath);
        
        _loader.DataLoaderUI = this;

        VisualElement rootUI = documentUI.Instantiate();
        rootUI.style.height = new Length(100, LengthUnit.Percent);

        LoadUIElement(rootUI);
        AddEvent(rootUI);
        
        _dataElementParent.text = "Data";

        rootVisualElement.Add(rootUI);
    }

    private void LoadUIElement(VisualElement root){
        _logTitle = root.Q<Label>("title");
        _logType = root.Q<Label>("type");
        _logPath = root.Q<Label>("path");
        _logLabel = root.Q<Label>("text-log");
        _innerData = root.Q<Label>("code-text");

        _dataElementParent = root.Q<Foldout>(className: "fold-out");

        _urlText = root.Q<TextField>(className: "input-field");
    }

    private void AddEvent(VisualElement root){
        Button btnLoad = root.Q<Button>(className: "btn");

        btnLoad.RegisterCallback<ClickEvent>(evt => {
            //스크립트는 나중에 필요할 때 추가하기

            EditorCoroutineUtility.StartCoroutine(_loader.GetDataFromSheet(_urlText.text, "0", (sucess, data) => {
                int lineNum;
                
                if(sucess){
                    _loader.HandleData(data, DataLoadType.ScriptableObject, out lineNum);
                    AddLog($"{lineNum - 1}개의 데이터가 정상적으로 생성되었습니다.");
                }
                else{
                    AddLog(data);
                }
            }), this);
        });
    }

    public void CreateDataUI(DataLoadType type, string[] data, string line, string path){
        VisualTreeAsset dataContent = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editors/UI/DataContent.uxml");
        VisualElement rootData = dataContent.Instantiate().Q(className: "data");
        rootData.AddToClassList((type == DataLoadType.Script) ? "cs" : "so");
        rootData.RegisterCallback<ClickEvent>(evt => {
            List<VisualElement> otherData = rootData.parent.Query(className: "data").ToList();
            otherData.ForEach(data => data.RemoveFromClassList("on"));

            rootData.AddToClassList("on");
            LogDataSet(data[0], path, type, line);
        });
        
        Label nameText = rootData.Q<Label>("name-text");
        Label typeText = rootData.Q<Label>("type-text");

        nameText.text = data[0];
        typeText.text = type.ToString();

        _dataElementParent.Add(rootData);
    }

    private void LogDataSet(string name, string path, DataLoadType type, string innerData = ""){
        _logTitle.text = name;
        _logType.text = type.ToString();
        _logPath.text = path;
        _innerData.text = innerData;
    }

    public void AddLog(string log){
        Debug.Log(log);
        _logLabel.text += $"{log}\n";
    }
}

#endif