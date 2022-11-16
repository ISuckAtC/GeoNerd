using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq;

public class Network : MonoBehaviour
{
    public static string baseUri = "http://ec2-3-139-101-253.us-east-2.compute.amazonaws.com:80";
    public static int libraryMessageLength = 8;
    public static HttpClient httpClient = new HttpClient();
    public static string CreateUserHash()
    {
        System.Security.Cryptography.SHA256 sha = System.Security.Cryptography.SHA256.Create();
        List<byte> input = new List<byte>();
        input.AddRange(System.BitConverter.GetBytes(System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
        byte[] rawHash = sha.ComputeHash(input.ToArray());
        return System.Convert.ToBase64String(rawHash);
    }
    public static async Task<(int statusCode, string userId)> CreateUser()
    {
        Debug.Log("Attempting to request user creation");
        string userId = CreateUserHash();
        HttpResponseMessage response = await httpClient.GetAsync(baseUri + "/create?userId=" + userId);
        Debug.Log("Recieved message from server: " + (await response.Content.ReadAsStringAsync()));
        return ((int)response.StatusCode, userId);
    }
    public static async Task SaveUser(string id)
    {
        Debug.Log("Attempting to post user save");
        byte[] saveData = GameManager.GameData.GetSerializedData();
        HttpContent content = new ByteArrayContent(saveData);
        HttpResponseMessage response = await httpClient.PostAsync(baseUri + "/save?userId=" + id, content);
        Debug.Log("Recieved message from server: " + (await response.Content.ReadAsStringAsync()));
    }
    public static async Task<byte[]> LoadUser(string id)
    {
        HttpResponseMessage response = await httpClient.GetAsync(baseUri + "/load?userId=" + id);
        Debug.Log("Recieved message from server: " + (await response.Content.ReadAsStringAsync()));
        return await response.Content.ReadAsByteArrayAsync();
    }
    public static async Task<byte[]> RequestLibraryMessage()
    {
        HttpResponseMessage response = await httpClient.GetAsync(baseUri + "/libraryMessage");
        return await response.Content.ReadAsByteArrayAsync();
    }
    public static async Task PostLibraryMessage(byte[] bytes)
    {
        if (bytes.Length != libraryMessageLength)
        {
            throw new System.FormatException("library message should be " + libraryMessageLength + " bytes");
        }
        HttpContent content = new ByteArrayContent(bytes);
        HttpResponseMessage response = await httpClient.PostAsync(baseUri + "/libraryMessage", content);
    }
    public static async Task<byte[]> RequestWorldMessage(int index = -1)
    {
        HttpResponseMessage response = await httpClient.GetAsync(baseUri + "/worldMessage" + (index >= 0 ? ("?" + index) : ""));
        return await response.Content.ReadAsByteArrayAsync();
    }
    public static async Task PostWorldMessage(byte[] bytes)
    {
        if (bytes.Length != libraryMessageLength + 12)
        {
            throw new System.FormatException("world message should be " + (libraryMessageLength + 12) + " bytes");
        }
        HttpContent content = new ByteArrayContent(bytes);
        HttpResponseMessage response = await httpClient.PostAsync(baseUri + "/worldMessage", content);
    }
    public static async Task<int> GetWorldMessageCount()
    {
        HttpResponseMessage response = await httpClient.GetAsync(baseUri + "/worldMessageCount");
        return System.BitConverter.ToInt32(await response.Content.ReadAsByteArrayAsync());
    }
}
