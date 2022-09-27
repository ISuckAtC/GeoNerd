using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntry : MonoBehaviour
{
    [Header("Shared")]
    public GameObject levelInfoScreen;
    public TMPro.TextMeshPro levelNameText;
    public TMPro.TextMeshPro levelScoreText;
    public GameObject lockedOverlay;
    public GameObject completedOverlay;
    public float popUpTime;
    public float popDownTime;
    public float interactionRange;

    [Header("Individual")]
    public int levelId;
    public string levelName;
    public string sceneToLoad;

    [SerializeField] private bool entry;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CapsuleCollider>().radius = interactionRange;
        levelInfoScreen.SetActive(false);
        levelNameText.text = levelName;
    }

    // Update is called once per frame
    void Update()
    {
        if (entry && Input.GetButton("Confirm"))
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            levelInfoScreen.SetActive(true);
            entry = true;
            LevelData latest = GameManager.GameData.GetById(levelId);
            levelScoreText.text = latest.score.ToString();
            lockedOverlay.SetActive(!latest.unlocked);
            completedOverlay.SetActive(latest.completed);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            levelInfoScreen.SetActive(true);
        }
    }
}
