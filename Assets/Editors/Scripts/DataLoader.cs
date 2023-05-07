using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System;

public enum DataLoadType{
    Script,
    ScriptableObject
}

public class DataLoader
{
    public DataLoaderUI DataLoaderUI;

    private void CreateSourceCode(string[] dataArr, string line){
        // string code = string.Format(CodeFormat.CharacterFormat, dataArr[1], dataArr[0], dataArr[2]);
        // string path = $"{Application.dataPath}/01.Scripts/Characters/";
        // statusLabel.text += $"ClassName : {dataArr[1]}.cs\n";
        // File.WriteAllText($"{path}{dataArr[1]}.cs", code);
    }

    private void CreateScriptableObject<T>(string[] dataArr, string line) where T : LoadableData
    {
        string assetPath = $"Assets/Resources/{dataArr[0]}Data.asset";
        T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

        if(asset == null)
        {
            asset = ScriptableObject.CreateInstance<T>();
            string fileName = AssetDatabase.GenerateUniqueAssetPath(assetPath);
            AssetDatabase.CreateAsset(asset, fileName);
        }

        asset.SetUp(dataArr);

        DataLoaderUI.CreateDataUI(DataLoadType.ScriptableObject, dataArr, line, assetPath);
        
        AssetDatabase.SaveAssets();
    }

    public void HandleData(string data, DataLoadType type, out int lineNum){
        string[] lines = data.Split("\n"); //라인별로 나눠서 배열에 담기
        lineNum = 1;

        for(lineNum = 1; lineNum < lines.Length; lineNum++){
            string[] dataArr = lines[lineNum].Split("\t");
            switch(type){
                case DataLoadType.Script:
                    CreateSourceCode(dataArr, lines[lineNum]);
                    break;
                case DataLoadType.ScriptableObject:
                    CreateScriptableObject<FishSO>(dataArr, lines[lineNum]);
                    break;
            }
        }

        AssetDatabase.Refresh();
    }

    public IEnumerator GetDataFromSheet(string documentID, string sheetID, Action<bool, string> Process){
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