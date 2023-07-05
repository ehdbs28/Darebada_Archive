using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System;

public class DataLoader
{
    public DataLoaderUI DataLoaderUI;

    private void CreateDataTable<T>(DataLoadType type, string[] dataArr, string line) where T : LoadableData
    {
        string assetPath = $"Assets/Resources/{type.ToString()}.asset";
        T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

        if(asset == null)
        {
            asset = ScriptableObject.CreateInstance<T>();
            asset.Type = type;
            string fileName = AssetDatabase.GenerateUniqueAssetPath(assetPath);
            AssetDatabase.CreateAsset(asset, fileName);
        }
        else {
            asset.Clear();
        }
        
        asset.SetUp(dataArr);

        DataLoaderUI.CreateDataUI(type, dataArr, line, assetPath);
        
        AssetDatabase.SaveAssets();
    }

    public void HandleData(string data, DataLoadType type, out int lineNum){
        string[] lines = data.Split("\n");
        lineNum = 1;

        for(lineNum = 1; lineNum < lines.Length; lineNum++){
            string[] dataArr = lines[lineNum].Split("\t");
            switch(type){
                case DataLoadType.FishingUpgradeData:
                    CreateDataTable<FishingUpgradeTable>(type, dataArr, lines[lineNum]);
                    break;
                case DataLoadType.BoatData:
                    CreateDataTable<BoatDataTable>(type, dataArr, lines[lineNum]);
                    break;
            }
        }

        AssetDatabase.Refresh();
    }

    public IEnumerator GetDataFromSheet(string documentID, string sheetID, Action<bool, string> Process){
        if(documentID ==  "" || sheetID == "" || documentID == "sheet ID" || sheetID == "page name"){
            Process?.Invoke(false, "ERROR: 정확한 값을 입력하세요");
            yield break;
        }

        string url = $"https://docs.google.com/spreadsheets/d/{documentID}/export?format=tsv&gid={sheetID}";
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest(); 

        if(req.result == UnityWebRequest.Result.ConnectionError || req.responseCode != 200){
            Process?.Invoke(false, "ERROR: 서버 불러오기 중 오류 발생");
            yield break;
        }

        Process?.Invoke(true, req.downloadHandler.text);
    }
}