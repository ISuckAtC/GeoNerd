using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

public class Network : MonoBehaviour
{
    public static string baseUri = "http://ec2-3-139-101-253.us-east-2.compute.amazonaws.com:80";
    public static int libraryMessageLength = 8;
    public static HttpClient httpClient = new HttpClient();
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
