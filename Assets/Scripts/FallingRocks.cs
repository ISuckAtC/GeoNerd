using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallingRocks : MonoBehaviour
{
    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject rockObstaclePrefab;
    [SerializeField] GameObject ground;
    [SerializeField] float timeToChangeScene;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] string leavingScene;
    [SerializeField] bool leaving = false;
    bool spawning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRock());
        if(leaving) StartCoroutine(spawnObstacleRock());

    }

    private void Update()
    {
        timeToChangeScene -= Time.deltaTime;
        timeText.text = ((int)timeToChangeScene).ToString();

        if(timeToChangeScene <= 0)
        {
            GameManager.Instance.LoadScene(leavingScene);
        }
    }
    IEnumerator spawnRock()
    {
        while (spawning)
        {
            float xRandPos = Random.Range(-(Camera.main.aspect * Camera.main.orthographicSize) + rockPrefab.transform.localScale.x / 2, Camera.main.aspect * Camera.main.orthographicSize - rockPrefab.transform.localScale.x / 2);
            float yPos = Camera.main.orthographicSize + rockPrefab.transform.localScale.y / 2;
            Vector3 pos = new Vector3(xRandPos, yPos, 0);
            GameObject rock = Instantiate(rockPrefab);
            rock.transform.position = pos;
            Debug.Log("SpawnedRock");

            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
      
    }

    IEnumerator spawnObstacleRock()
    {
        while (leaving)
        {
            float xRandPos = -(Camera.main.aspect * Camera.main.orthographicSize) - rockPrefab.transform.localScale.x / 2;
            float yPos = ground.transform.position.y + ground.transform.localScale.y/2 + rockPrefab.transform.localScale.y / 2;
            Vector3 pos = new Vector3(xRandPos, yPos, 0);
            GameObject rock = Instantiate(rockObstaclePrefab);   
            rock.transform.position = pos;
            Debug.Log("SpawnedRock");

            yield return new WaitForSeconds(Random.Range(5f, 12f));
        }

    }
}
