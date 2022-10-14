using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

public class Network : MonoBehaviour
{
    public static int libraryMessageLength = 8;
    public static HttpClient httpClient = new HttpClient();
    public static async Task<byte[]> RequestLibraryMessage()
    {
        HttpResponseMessage response = await httpClient.GetAsync("http://ec2-3-139-101-253.us-east-2.compute.amazonaws.com:80/libraryMessage");
        return await response.Content.ReadAsByteArrayAsync();
    }
    public static async Task PostLibraryMessage(byte[] bytes)
    {
        if (bytes.Length != libraryMessageLength)
        {
            throw new System.FormatException("library message should be " + libraryMessageLength + " bytes");
        }
        HttpContent content = new ByteArrayContent(bytes);
        HttpResponseMessage response = await httpClient.PostAsync("http://ec2-3-139-101-253.us-east-2.compute.amazonaws.com:80/libraryMessage", content);
    }
}
