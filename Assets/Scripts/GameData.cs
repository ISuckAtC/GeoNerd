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
        byte[] serialized = new byte[8 + (levelData.Length * 12)];

        BitConverter.GetBytes(money).CopyTo(serialized, 0);

        for (int i = 0; i < levelData.Length; ++i)
        {
            levelData[i].Serialized.CopyTo(serialized, 8 + (12 * i));
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

            levelData = new LevelData[(serialized.Length - 8) / 12];

            for (int i = 0; i < levelData.Length; ++i)
            {
                byte[] sLevel = new byte[12];
                serialized.AsSpan().Slice(8 + (i * 12), 12).ToArray().CopyTo(sLevel, 0);
                levelData[i] = LevelData.Deserialize(sLevel);
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
