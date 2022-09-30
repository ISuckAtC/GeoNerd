using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct LevelData
{
    // Level data should be 12 bytes
    public int id; // unique identifier for each level


    // FLAGS
    public bool completed; // whether or not the mission has been completed by the player
    public bool visited; // whether or not the mission has been visited by the player before
    public bool unlocked; // whether or not the mission has been unlocked by the player

    // Array should not have more entries than 32 minus the flags above
    public bool[] additionalFlags; // slots for custom flags, such as level specific ones
    public int score; // the highscore the player has on this level (dont know if we will use this)


    /// <summary>
    ///     Serializes the current object
    /// </summary>
    /// <returns>
    ///     A serialized array of bytes containing the data of this object, the size is 8 byte
    /// </returns>
    public byte[] Serialized
    {
        get
        {
            byte[] bytes = new byte[12];

            int flags = 0;
            flags |= completed ? 1 : 0;
            flags |= visited ? 1 : 0 << 1;
            flags |= unlocked ? 1 : 0 << 2;

            if (additionalFlags.Length > 29) throw new System.Exception("additionalFlags cannot exceed 29 entires");

            for (int i = 0; i < additionalFlags.Length; ++i)
            {
                flags |= additionalFlags[i] ? 1 : 0 << (i + 3);
            }
            BitConverter.GetBytes(id).CopyTo(bytes, 0);
            BitConverter.GetBytes(flags).CopyTo(bytes, 4);
            BitConverter.GetBytes(score).CopyTo(bytes, 8);


            return bytes;
        }
    }

    // Returns a new LevelData object based on the serialized byte array thats passed
    public static LevelData Deserialize(byte[] serialized)
    {
        LevelData data;

        data.id = BitConverter.ToInt32(serialized, 0);
        int flags = BitConverter.ToInt32(serialized, 4);
        data.score = BitConverter.ToInt32(serialized, 8);

        data.completed = ((flags & 1 << 0) == flags);
        data.visited = ((flags & 1 << 1) == flags);
        data.unlocked = ((flags & 1 << 2) == flags);

        data.additionalFlags = new bool[29];

        for (int i = 0; i < 29; ++i)
        {
            data.additionalFlags[i] = ((flags & 1 << (3 + i)) == flags);
        }

        return data;
    }
}
public class GameData
{
    public string playerName; // username of the player, also whats used to store the save data file
    public long money; // the total money of the player
    public LevelData[] levelData; // data for every level the player has savedata for

    public Dictionary<int,int> missionProgress = new Dictionary<int, int>();

    public LevelData GetById(int id)
    {
        foreach (LevelData level in levelData)
        {
            if (id == level.id) return level;
        }
        return new LevelData();
    }

    // Saves the current data of the player to binary file
    public void SaveData()
    {
        byte[] serialized = new byte[16 + (levelData.Length * 12)];

        BitConverter.GetBytes(money).CopyTo(serialized, 0);
        BitConverter.GetBytes(levelData.Length).CopyTo(serialized, 8);

        for (int i = 0; i < levelData.Length; ++i)
        {
            levelData[i].Serialized.CopyTo(serialized, 12 + (12 * i));
        }

        BitConverter.GetBytes(MissionLogic.MissionLogics.Count).CopyTo(serialized, 12 + (12 * levelData.Length));

        for (int i = 0; i < MissionLogic.MissionLogics.Count; ++i)
        {
            BitConverter.GetBytes(i).CopyTo(serialized, 16 + (12 * i));
            BitConverter.GetBytes(MissionLogic.MissionLogics[i].GetProgress()).CopyTo(serialized, 20 + (12 * i));
        }

        System.IO.File.WriteAllBytes("./" + playerName, serialized);
    }

    // Finds binary file with matching username, and loads the data into this object if it finds a file. 
    // If not, creates blank save data with the given username.
    public void LoadData(string name = "")
    {
        if (System.IO.File.Exists("./" + name))
        {
            byte[] serialized = System.IO.File.ReadAllBytes("./" + name);

            playerName = name;

            money = BitConverter.ToInt64(serialized, 0);

            int levelCount = BitConverter.ToInt32(serialized, 8);

            levelData = new LevelData[levelCount];

            for (int i = 0; i < levelData.Length; ++i)
            {
                byte[] sLevel = new byte[12];
                serialized.AsSpan().Slice(8 + (i * 12), 12).ToArray().CopyTo(sLevel, 0);
                levelData[i] = LevelData.Deserialize(sLevel);
            }

            int missionCount = BitConverter.ToInt32(serialized, 12 + (levelCount * 12));

            for (int i = 0; i < missionCount; ++i)
            {
                int missionId = BitConverter.ToInt32(serialized, 16 + (levelCount * 12) + (8 * i));
                int progress = BitConverter.ToInt32(serialized, 20 + (levelCount * 12) + (8 * i));
                MissionLogic.MissionLogics[missionId].Skip(progress);
            }
        }
        else
        {
            Debug.Log("No name found in local files, assuming new player");
            playerName = name;
            money = 0;
            levelData = new LevelData[16]; // number of levels
        }
    }
}
