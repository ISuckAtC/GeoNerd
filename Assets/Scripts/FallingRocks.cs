using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FallingRocks : MonoBehaviour
{
    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject rockObstaclePrefab;
    [SerializeField] GameObject ground;
    [SerializeField] GameObject troll;
    [SerializeField] float timeToChangeScene;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] string leavingScene;
    [SerializeField] bool leaving = false;
    bool spawning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRock());
        if (leaving)
        {
            StartCoroutine(spawnObstacleRock());
            troll.transform.DOMoveX(-Camera.main.aspect * Camera.main.orthographicSize / 2, timeToChangeScene * 1.25f);

        }

    }

    private void Update()
    {
        timeToChangeScene -= Time.deltaTime;
        timeText.text = ((int)timeToChangeScene).ToString();

        if (timeToChangeScene <= 0)
        {
            //if(leaving) GameManager.GameData.Flags[Flag.OSLO_FORESTDONE] = true;
            // GameManager.Instance.LoadScene(leavingScene);
            GameManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
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
            float yPos = ground.transform.position.y + ground.transform.localScale.y / 2 + rockPrefab.transform.localScale.y / 2;
            Vector3 pos = new Vector3(xRandPos, yPos, 0);
            GameObject rock = Instantiate(rockObstaclePrefab);
            rock.transform.position = pos;
            Debug.Log("SpawnedRock");

            yield return new WaitForSeconds(Random.Range(5f, 12f));
        }

    }
}
