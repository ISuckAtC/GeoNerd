using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

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
    public string playerId;
    public string playerName; // username of the player, also whats used to store the save data file
    public long money; // the total money of the player
    public LevelData[] levelData; // data for every level the player has savedata for
    public Dictionary<Flag, bool> Flags;

    public Vector3 overWorldPosition;

    public bool globalFlags = true;

    public LevelData GetById(int id)
    {
        foreach (LevelData level in levelData)
        {
            if (id == level.id) return level;
        }
        return new LevelData();
    }

    public byte[] GetSerializedData()
    {
        int flagMaps = Flags.Count / 8;
        int rest = Flags.Count % 8;

        int offset = 0;

        playerName = new string(playerName);

        Debug.Log(playerName);
        byte[] nameBytes = playerName.Select(x => (byte)x).ToArray();

        byte[] serialized = new byte[nameBytes.Length + 1 + 32 + flagMaps + (rest > 0 ? 1 : 0)];

        serialized[0] = (byte)nameBytes.Length;
        offset += 1;

        nameBytes.CopyTo(serialized, offset);
        offset += nameBytes.Length;

        BitConverter.GetBytes(DateTime.Now.ToFileTimeUtc()).CopyTo(serialized, offset);
        offset += 8;

        BitConverter.GetBytes(money).CopyTo(serialized, offset);
        offset += 8;

        BitConverter.GetBytes(overWorldPosition.x).CopyTo(serialized, offset);
        offset += 4;
        BitConverter.GetBytes(overWorldPosition.y).CopyTo(serialized, offset);
        offset += 4;
        BitConverter.GetBytes(overWorldPosition.z).CopyTo(serialized, offset);
        offset += 4;

        BitConverter.GetBytes(Flags.Count).CopyTo(serialized, offset);
        offset += 4;

        bool[] flagValues = Flags.Values.ToArray();


        for (int i = 0; i < flagMaps; ++i)
        {
            byte flagByte = 0;
            for (int k = 0; k < 8; ++k)
            {
                if (flagValues[(i * 8) + k]) flagByte = (byte)(flagByte | 1 << k);
            }
            serialized[offset + i] = flagByte;
        }
        if (rest > 0)
        {
            byte flagByte = 0;
            for (int k = 0; k < rest; ++k)
            {
                if (flagValues[(flagMaps * 8) + k]) flagByte = (byte)(flagByte | 1 << k);
            }
            serialized[offset + flagMaps] = flagByte;
        }

        return serialized;
    }

    // Saves the current data of the player to binary file
    public async Task SaveData()
    {
        if (GameManager.Instance.online)
        {
            await OnlineSaveData();
        }
        else
        {
            try
            {
            Debug.Log("Writing bytes locally");
            await File.WriteAllBytesAsync("./saves/" + playerId, GetSerializedData());
            Debug.Log("Written");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + "\n\n" + e.StackTrace);
            }
        }

        /*
        byte[] serialized = new byte[8 + (levelData.Length * 12)];

        BitConverter.GetBytes(money).CopyTo(serialized, 0);

        for (int i = 0; i < levelData.Length; ++i)
        {
            levelData[i].Serialized.CopyTo(serialized, 8 + (12 * i));
        }

        System.IO.File.WriteAllBytes("./" + playerName, serialized);
        */
    }

    public void DeleteData(string name)
    {
        if (System.IO.File.Exists("./" + name))
        {
            File.Delete("./" + name);
        }
    }

    public async Task OnlineSaveData()
    {
        await Network.SaveUser(playerId);
    }

    public async Task OnlineLoadData()
    {
        try
        {
            Console.WriteLine("Attempting to load user with id=" + playerId);

            Flags = new Dictionary<Flag, bool>();
            Flag[] flagArray = Enum.GetValues(typeof(Flag)) as Flag[];

            for (int i = 0; i < flagArray.Length; ++i)
            {
                Flags.Add(flagArray[i], false);
            }

            byte[] serialized = await Network.LoadUser(playerId);

            File.WriteAllBytes("./TESTLOAD", serialized);

            Debug.Log("User loaded");
            Debug.Log("Data length is " + serialized.Length);

            byte[] nameBytes = serialized.Skip(1).Take((int)serialized[0]).ToArray();

            playerName = new string(nameBytes.Select(x => (char)x).ToArray());

            money = BitConverter.ToInt64(serialized, nameBytes.Length + 1);

            overWorldPosition.x = BitConverter.ToSingle(serialized, nameBytes.Length + 1 + 8);
            overWorldPosition.y = BitConverter.ToSingle(serialized, nameBytes.Length + 1 + 12);
            overWorldPosition.z = BitConverter.ToSingle(serialized, nameBytes.Length + 1 + 16);

            Debug.Log("Loaded overworld position at: " + overWorldPosition);

            int flagCount = BitConverter.ToInt32(serialized, nameBytes.Length + 1 + 20);

            int flagMaps = flagCount / 8;
            int rest = flagCount % 8;

            Debug.Log("Loading data from file\nFlagCount: " + flagCount + "\nFlagMaps: " + flagMaps + "\nRest: " + rest);

            for (int i = 0; i < flagMaps; ++i)
            {
                byte flagByte = serialized[nameBytes.Length + 1 + 24 + i];
                for (int k = 0; k < 8; ++k)
                {
                    bool flag = (flagByte & (1 << k)) == (1 << k);
                    Flags[(Flag)((i * 8) + k)] = flag;
                }
            }
            if (rest > 0)
            {
                byte flagByte = serialized[nameBytes.Length + 1 + 24 + flagMaps];
                Debug.Log(flagByte);
                for (int k = 0; k < rest; ++k)
                {

                    bool flag = (flagByte & (1 << k)) == (1 << k);
                    Flags[(Flag)((flagMaps * 8) + k)] = flag;
                }
            }

            Console.WriteLine("Loaded online data");
            Console.WriteLine("Name is " + playerName);
            Console.WriteLine("Money is " + money);

            foreach (KeyValuePair<Flag, bool> flag in Flags)
            {
                Debug.Log("Flag \"" + flag.Key.ToString() + "\" is " + (flag.Value ? "TRUE" : "FALSE"));
            }
        }
        catch (Exception e) { Debug.Log(e.Message + "\n\n" + e.StackTrace); }
    }

    public async Task OnlineCreateNewUser(string name)
    {
        Flags = new Dictionary<Flag, bool>();
        Flag[] flagArray = Enum.GetValues(typeof(Flag)) as Flag[];

        for (int i = 0; i < flagArray.Length; ++i)
        {
            Flags.Add(flagArray[i], false);
        }

        (int statusCode, string userId) ret = await Network.CreateUser();
        Debug.Log("Created user, code is " + ret.statusCode);
        if (ret.statusCode != 200)
        {
            throw new System.Net.WebException();
        }

        playerId = ret.userId;
        playerName = name;
        money = 0;
        overWorldPosition = new Vector3(932.2f, 41.08f, 500.61f);

        Flags[Flag.OSLO_ARROW] = true;
        Flags[Flag.FOREST_ARROW] = true;

        Debug.Log("Created new online user");
        Debug.Log("Id is " + playerId);
        Debug.Log("Name is " + playerName);

        await OnlineSaveData();
        await OnlineLoadData();
        return;
    }

    public (string, DateTime) LoadSaveInfo(string file)
    {
        byte[] serialized = System.IO.File.ReadAllBytes(file);
        byte[] nameBytes = serialized.Skip(1).Take((int)serialized[0]).ToArray();

        string pName = new string(nameBytes.Select(x => (char)x).ToArray());

        DateTime time = DateTime.FromFileTimeUtc(BitConverter.ToInt64(serialized, 1 + nameBytes.Length));

        return (pName, time);
    }

    // Finds binary file with matching username, and loads the data into this object if it finds a file. 
    // If not, creates blank save data with the given username.
    public async Task LoadData(string name = "TESTPLAYER")
    {
        if (GameManager.Instance.online)
        {
            await OnlineLoadData();
            return;
        }
        if (globalFlags)
        {
            Debug.Log("Loading data using global flags");
            Flags = new Dictionary<Flag, bool>();
            Flag[] flagArray = Enum.GetValues(typeof(Flag)) as Flag[];

            for (int i = 0; i < flagArray.Length; ++i)
            {
                Flags.Add(flagArray[i], false);
            }

            if (System.IO.File.Exists("./" + name))
            {
                byte[] serialized = System.IO.File.ReadAllBytes("./" + name);

                playerId = name;

                int offset = 0;

                byte[] nameBytes = serialized.Skip(1).Take((int)serialized[0]).ToArray();

                playerName = new string(nameBytes.Select(x => (char)x).ToArray());

                offset += 1 + nameBytes.Length;



                money = BitConverter.ToInt64(serialized, offset);
                offset += 8;

                overWorldPosition.x = BitConverter.ToSingle(serialized, offset);
                offset += 4;
                overWorldPosition.y = BitConverter.ToSingle(serialized, offset);
                offset += 4;
                overWorldPosition.z = BitConverter.ToSingle(serialized, offset);
                offset += 4;

                Debug.Log("Loaded overworld position at: " + overWorldPosition);

                int flagCount = BitConverter.ToInt32(serialized, offset);
                offset += 4;

                int flagMaps = flagCount / 8;
                int rest = flagCount % 8;

                Debug.Log("Loading data from file\nFlagCount: " + flagCount + "\nFlagMaps: " + flagMaps + "\nRest: " + rest);

                for (int i = 0; i < flagMaps; ++i)
                {
                    byte flagByte = serialized[offset + i];
                    for (int k = 0; k < 8; ++k)
                    {
                        bool flag = (flagByte & (1 << k)) == (1 << k);
                        Flags[(Flag)((i * 8) + k)] = flag;
                    }
                }
                if (rest > 0)
                {
                    byte flagByte = serialized[offset + flagMaps];
                    Debug.Log(flagByte);
                    for (int k = 0; k < rest; ++k)
                    {

                        bool flag = (flagByte & (1 << k)) == (1 << k);
                        Flags[(Flag)((flagMaps * 8) + k)] = flag;
                    }
                }

                Console.WriteLine("Loaded local data");
                Console.WriteLine("Name is " + playerName);
                Console.WriteLine("Money is " + money);

                foreach (KeyValuePair<Flag, bool> flag in Flags)
                {
                    Debug.Log("Flag \"" + flag.Key.ToString() + "\" is " + (flag.Value ? "TRUE" : "FALSE"));
                }
            }
            else
            {
                Debug.Log("No name found in local files, assuming new player");
                playerName = name;
                playerId = name;
                money = 0;
                overWorldPosition = new Vector3(932.2f, 41.08f, 500.61f);

                Flags[Flag.OSLO_ARROW] = true;
                Flags[Flag.FOREST_ARROW] = true;

                SaveData();
            }

            foreach (KeyValuePair<Flag, bool> flag in Flags)
            {
                Debug.Log("Flag \"" + flag.Key.ToString() + "\" is " + (flag.Value ? "TRUE" : "FALSE"));
            }

            return;
        }
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
