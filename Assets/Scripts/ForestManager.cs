using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ForestManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static ForestManager _instance;
    public static ForestManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ForestManager");
                _instance.popUpPanel = GameObject.FindGameObjectWithTag("RestartScreen");
                _instance.popUpPanel.SetActive(false);


                DontDestroyOnLoad(go);
                _instance = go.AddComponent<ForestManager>();
            }
            return _instance;
        }
    }
    public bool[] items;


    [System.Serializable]
    public enum Direction { Up, Down, Right, Left};

    private Vector2Int[] DirectionVector = { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0)};

    [SerializeField]
    private GameObject[][] gridOfPanels;
    [SerializeField]
    int forestDimension;

    public FMODUnity.EventReference ambience;

    [System.Serializable]
    public class ForestPosition
    {
        public GameObject panel;
        public Vector2Int position;
    }


    public GameObject popUpPanel;
   

    [SerializeField] ForestPosition[] forestPositions;
    [SerializeField] Vector2Int initialPos;

    ForestPosition playerPos;
    

    void Start()
    {
        GameManager.FMODPlayStatic(ambience, Vector3.zero, Vector3.zero);
        _instance = this;
        playerPos = new ForestPosition();
        //Array initialization
        gridOfPanels = new GameObject[forestDimension][];
        for(int x = 0; x < gridOfPanels.Length; x++)
        {
            gridOfPanels[x] = new GameObject[forestDimension];
        }

       for(int x = 0; x < forestPositions.Length; x++)
        {
            Vector2Int panelPos = forestPositions[x].position;
            if (gridOfPanels[panelPos.x][panelPos.y] != null || panelPos.x < 0 || panelPos.x >= forestDimension || panelPos.y < 0 || panelPos.y >= forestDimension)
            {
                Debug.Log("Forest array not created correctly, either index of panels are oyt of the array or some pnel was overriden");
            }
           

            //leave enabled ONLY the one th eplayer is
            gridOfPanels[(int)panelPos.x][(int)panelPos.y] = forestPositions[x].panel;
            if(initialPos != panelPos)
            {
                forestPositions[x].panel.SetActive(false);
            }
            else{
                playerPos.panel = forestPositions[x].panel;
                playerPos.position = initialPos;
                playerPos.panel.SetActive(true);
            }
        }



        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeAndShowPopUpData(PopUpForestBoxData restartData)
    {
        popUpPanel.GetComponentInChildren<TextMeshProUGUI>().text = restartData.text;
        popUpPanel.GetComponentsInChildren<Image>()[2].sprite = restartData.image;
        popUpPanel.SetActive(true);
    }


    public void GoToStartpoint(PopUpForestBoxData restartData)
    {
        for (int x = 0; x < forestPositions.Length; x++)
        {
            Vector2Int panelPos = forestPositions[x].position;
            if (initialPos != panelPos)
            {
                forestPositions[x].panel.SetActive(false);
            }
            else
            {
                playerPos.panel = forestPositions[x].panel;
                playerPos.position = initialPos;
                playerPos.panel.SetActive(true);
            }
        }


        ChangeAndShowPopUpData(restartData);

    }

    public void TraverseForest(string dirString)
    {
        //Direction dir = Direction.Up;
        //since the array looks like this:
        //   0 1 2 3 4 
        // 3
        // 2 
        // 1  
        // 0  x
        //If the player is on the x ([1][3]), going up is going to be [1][2]
        Direction dir;
        switch (dirString)
        {
            case "Up": dir = Direction.Up; break;
            case "Down": dir = Direction.Down; break;
            case "Right": dir = Direction.Right; break;
            case "Left": dir = Direction.Left; break;
            default:
                Debug.Log("Direction string not mathing on arrows");
                return; 
                break;
        }

        if (DirectionAvaible(dir))
        {
            playerPos.panel.SetActive(false);
            playerPos.position += DirectionVector[(int)dir];
            playerPos.panel = gridOfPanels[playerPos.position.x][playerPos.position.y];
            playerPos.panel.SetActive(true);

        }
        else
        {
            Debug.Log("Position not avaible");
        }
    }

    private bool DirectionAvaible(Direction dir)
    {
        Vector2Int newPos = playerPos.position + DirectionVector[(int)dir];
        return newPos.x < forestDimension && newPos.x >= 0 && newPos.y < forestDimension && newPos.y >= 0 && gridOfPanels[newPos.x][newPos.y];
    }

}


