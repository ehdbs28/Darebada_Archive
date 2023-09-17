using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ChallengeDataUnit
{
    public string Title;
    public string Info;
    public int Target;
    public int Compensation;
    public ChallengeType Type;
    public string TargetFishName;
}

public class ChallengeDataTable : LoadableData
{
    public DataTable<ChallengeDataUnit> DataTable = new DataTable<ChallengeDataUnit>();

    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new ChallengeDataUnit());

        DataTable[Size].Title = dataArr[0];
        DataTable[Size].Info = dataArr[1];
        DataTable[Size].Target = int.Parse(dataArr[2]);
        DataTable[Size].Compensation = int.Parse(dataArr[3]);
        DataTable[Size].Type = (ChallengeType)Enum.Parse(typeof(ChallengeType), $"{dataArr[4]}");
        DataTable[Size].TargetFishName = dataArr[5];

        ++Size;
    }
}
