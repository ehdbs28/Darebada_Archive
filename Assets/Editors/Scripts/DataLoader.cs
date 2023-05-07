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

    private void CreateSourceCode(string[] dataArr){
        // string code = string.Format(CodeFormat.CharacterFormat, dataArr[1], dataArr[0], dataArr[2]);
        // string path = $"{Application.dataPath}/01.Scripts/Characters/";
        // statusLabel.text += $"ClassName : {dataArr[1]}.cs\n";
        // File.WriteAllText($"{path}{dataArr[1]}.cs", code);
    }

    private void CreateScriptableObject(string[] dataArr)
    {
        // string assetPath = $"Assets/Resources/WeaponData/{dataArr[0]}Data.asset";
        // WeaponData asset = AssetDatabase.LoadAssetAtPath<WeaponData>(assetPath);

        // if(asset == null)
        // {
        //     asset = ScriptableObject.CreateInstance<WeaponData>();
        //     string fileName = AssetDatabase.GenerateUniqueAssetPath(assetPath);
        //     AssetDatabase.CreateAsset(asset, fileName);
        // }

        // asset.attackSpeed = float.Parse(dataArr[1]);
        // asset.damage = float.Parse(dataArr[2]);
        // asset.weight = float.Parse(dataArr[3]);
        // asset.combo = int.Parse(dataArr[4]);

        // DataLoaderUI.CreateDataUI(DataType.ScriptableObject, dataArr, assetPath);
        
        // AssetDatabase.SaveAssets();
    }

    public void HandleData(string data, DataLoadType type, out int lineNum){
        string[] lines = data.Split("\n"); //라인별로 나눠서 배열에 담기
        lineNum = 1;

        for(lineNum = 1; lineNum < lines.Length; lineNum++){
            string[] dataArr = lines[lineNum].Split("\t");
            switch(type){
                case DataLoadType.Script:
                    CreateSourceCode(dataArr);
                    break;
                case DataLoadType.ScriptableObject:
                    CreateScriptableObject(dataArr);
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