using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePlayer : MonoBehaviour
{
    public List<Sprite> frames;
    [Tooltip("In seconds")]
    public float frameInterval;
    public bool playOnAwake;
    public float delay;
    public bool playing;
    private int index;
    private float currentInterval;
    private UnityEngine.UI.Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        image.sprite = frames[0];
        currentInterval = frameInterval;
        if (playOnAwake) Invoke("Play", delay);
    }

    void Play()
    {
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing) return;
        if (currentInterval <= 0)
        {
            currentInterval += frameInterval;
            if (++index == frames.Count)
            {
                index = 0;
            }
            image.sprite = frames[index];
        }
        else
        {
            currentInterval -= Time.deltaTime;
        }
    }
}
