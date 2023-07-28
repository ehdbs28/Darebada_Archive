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

    private void AddData<T>(DataLoadType type, T asset, string[] dataArr, string line, string assetPath) where T : LoadableData{
        asset.AddData(dataArr);
        DataLoaderUI.CreateDataUI(type, dataArr, line, assetPath);
        AssetDatabase.SaveAssets();
    }

    public void HandleData<T>(string data, DataLoadType type, out int lineNum) where T : LoadableData{
        string assetPath = $"Assets/06.SO/SheetData/{type.ToString()}.asset";

        T asset = ScriptableObject.CreateInstance<T>();
        asset.Type = type;
        
        string[] lines = data.Split("\n");
        lineNum = 1;

        for(lineNum = 1; lineNum < lines.Length; lineNum++){
            string[] dataArr = lines[lineNum].Split("\t");
            switch(type){
                case DataLoadType.FishData:
                    AddData<FishDataTable>(type, asset as FishDataTable, dataArr, lines[lineNum], assetPath);
                    break;
                case DataLoadType.FishingUpgradeData:
                    AddData<FishingUpgradeTable>(type, asset as FishingUpgradeTable, dataArr, lines[lineNum], assetPath);
                    break;
                case DataLoadType.ShopItemData:
                    AddData<ShopItemDataTable>(type, asset as ShopItemDataTable, dataArr, lines[lineNum], assetPath);
                    break;
                case DataLoadType.BoatData:
                    AddData<BoatDataTable>(type, asset as BoatDataTable, dataArr, lines[lineNum], assetPath);
                    break;
            }
        }

        string fileName = AssetDatabase.GenerateUniqueAssetPath(assetPath);

        AssetDatabase.DeleteAsset(assetPath);
        AssetDatabase.CreateAsset(asset, fileName);

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