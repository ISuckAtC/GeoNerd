using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public FMODUnity.EventReference backgroundMusic;

    protected class CityStruct{
        public bool visited;
        public bool locked;
    }
    //Variables of the map
    public GameObject[] cities;

    Dictionary<GameObject, CityStruct> citiesInfo;

    //protected CityStruct[] citiesInfo;

    void Start()
    {
        GameManager.FMODPlayStatic(backgroundMusic, Vector3.zero, Vector3.zero);
        //citiesInfo = new CityStruct[cities.Length];
        for(int x = 0; x < cities.Length; x++)
        {
            CityStruct info = new CityStruct();
            info.locked = false;
            info.visited = false;
            citiesInfo.Add(cities[x], info);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetCityVisited(GameObject city, bool visited)
    {
        citiesInfo[city].visited = true;
    }

    public bool GetCityVisited(GameObject city)
    {
        return citiesInfo[city].visited;
    }


    public void SetCityLocked(GameObject city, bool locked)
    {
        citiesInfo[city].locked = true;
    }

    public bool GetCityLocked(GameObject city)
    {
        return citiesInfo[city].locked;
    }




}
