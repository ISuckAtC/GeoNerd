using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionLogic
{
    public IEnumerable<bool> Logic;
    public int progress;
    public static List<MissionLogic> MissionLogics = new List<MissionLogic>();
    public MissionLogic(IEnumerable<bool> logic, int progress)
    {
        Logic = logic;
        this.progress = progress;

        logic = logic.Skip(progress);
    }
    public static void Initialize()
    {
        MissionLogics.Add(new MissionLogic(Mission1(), 0));
    }
    public int GetProgress()
    {
        return progress;
    }
    public bool Progress()
    {
        bool success = Logic.GetEnumerator().MoveNext();
        if (!success)
        {
            Logic = Logic.Reverse().ToArray();
            Skip(1);
            Logic = Logic.Reverse().ToArray();
            progress--;
        }
        else
        {
            progress++;
        }
        return success;
    }
    public void Skip(int num)
    {
        progress += num;
        Logic = Logic.Skip(num);
    }
    public void Reset()
    {
        Logic.GetEnumerator().Reset();
    }
    private static IEnumerable<bool> Mission1()
    {
        GameManager.GameData.levelData[0].additionalFlags[0] = true;
        GameManager.GameData.levelData[1].additionalFlags[0] = true;
        yield return true;
        GameManager.GameData.levelData[1].additionalFlags[0] = false;
    }
}
