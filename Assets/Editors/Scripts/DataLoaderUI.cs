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

    private TextField _documentID;
    private TextField _sheetID;
    private DropdownField _typeSetting;

    #region Variable about Log-Viewer
    private Label _logTitle;
    private Label _logType;
    private Label _logPath;

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

        _typeSetting.choices.Clear();
        foreach(var type in Enum.GetValues(typeof(DataLoadType))){
            _typeSetting.choices.Add(type.ToString());
        }
        _typeSetting.value = _typeSetting.choices[0];
        
        _dataElementParent.text = "Data";

        rootVisualElement.Add(rootUI);
    }

    private void LoadUIElement(VisualElement root){
        _logTitle = root.Q<Label>("title");
        _logType = root.Q<Label>("type");
        _logPath = root.Q<Label>("path");
        _innerData = root.Q<Label>("code-text");

        _dataElementParent = root.Q<Foldout>(className: "fold-out");

        _documentID = root.Q<TextField>("input_ID_field", className: "input-field");
        _sheetID = root.Q<TextField>("input_page_field", className: "input-field");
        _typeSetting = root.Q<DropdownField>("type_setting", className: "dropdown");
    }

    private void AddEvent(VisualElement root){
        Button btnLoad = root.Q<Button>(className: "btn");

        btnLoad.RegisterCallback<ClickEvent>(evt => {
            EditorCoroutineUtility.StartCoroutine(_loader.GetDataFromSheet(_documentID.text, _sheetID.text, (sucess, data) => {
                _dataElementParent.Clear();
                int lineNum = 0;
                
                if(sucess){
                    switch((DataLoadType)_typeSetting.choices.IndexOf(_typeSetting.value)){
                        case DataLoadType.FishData:
                            _loader.HandleData<FishDataTable>(data, DataLoadType.FishData, out lineNum);
                        break;
                        case DataLoadType.FishingUpgradeData:
                            _loader.HandleData<FishingUpgradeTable>(data, DataLoadType.FishingUpgradeData, out lineNum);
                        break;
                        case DataLoadType.ShopItemData:
                            _loader.HandleData<ShopItemDataTable>(data, DataLoadType.ShopItemData, out lineNum);
                        break;
                        case DataLoadType.BoatData:
                            _loader.HandleData<BoatDataTable>(data, DataLoadType.BoatData, out lineNum);
                        break;
                        case DataLoadType.ChallengeData:
                            _loader.HandleData<ChallengeDataTable>(data, DataLoadType.ChallengeData, out lineNum);
                            break;
                    }
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
        rootData.AddToClassList("so");
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
    }
}

#endif