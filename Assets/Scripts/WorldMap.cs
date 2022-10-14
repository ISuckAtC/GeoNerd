using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    public static string WorldMapSceneName;
    public GameObject player;
    public bool loadMessages;
    public GameObject signPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (loadMessages) LoadMessages();
        WorldMapSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    async void LoadMessages()
    {
        Vector3 downDirection = Camera.main.transform.position - player.transform.position;
        downDirection.y = 0;
        downDirection.Normalize();
        int worldMessageCount = await Network.GetWorldMessageCount();
        Debug.Log("Found " + worldMessageCount + " world messages");
        List<string> words = player.GetComponent<WorldPoster>().words;
        for (int i = 0; i < worldMessageCount; ++i)
        {
            byte[] response = await Network.RequestWorldMessage(i);
            GameObject sign = Instantiate(signPrefab);
            sign.transform.localScale = new Vector3(3,3,3);
            sign.transform.position = new Vector3(System.BitConverter.ToSingle(response, 0), System.BitConverter.ToSingle(response, 4), System.BitConverter.ToSingle(response, 8));
            Vector3 lookPosition = transform.position + downDirection;
            sign.transform.LookAt(lookPosition);
            WorldPost post = sign.GetComponent<WorldPost>();
            
            post.textObject.text = "";

            for (int k = 0; k < Network.libraryMessageLength; ++k)
            {
                post.textObject.text += words[(int)response[12 + k]] + " ";
            }
        }
    }
}
