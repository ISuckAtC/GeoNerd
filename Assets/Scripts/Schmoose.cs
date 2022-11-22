using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Schmoose : MonoBehaviour
{
    public string eatAnimationName;
    public string walkAnimationName;
    public string jumpAnimationName;
    
    public Animator[] schmeese;
    private string[] actions;

    public float speedMultiplierWalk;
    public float speedMultiplierJump;
    
    //[HideInInspector]
    public List<Transform> walkingSchmoose = new List<Transform>();
    //[HideInInspector]
    public List<Transform> jumpingSchmoose = new List<Transform>();
    
    void Awake()
    {
            actions = new [] { eatAnimationName, eatAnimationName, eatAnimationName, walkAnimationName, walkAnimationName, jumpAnimationName };
    }
    void Start()
    {
        Debug.Log("HELLOWORLD");
        GameObject map = GameObject.Find("GeoNerdMap_NewVersion");
        MeshFilter filter = map.GetComponent<MeshFilter>();
        Mesh mesh = Instantiate(filter.mesh);
        //DestroyImmediate(filter.mesh);
        List<Vector3> newVerts = new List<Vector3>();
        for (int i = 0; i < mesh.vertices.Length; ++i)
        {
            Vector3 vert = mesh.vertices[i];
            vert += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            newVerts.Add(vert);
        }

        mesh.vertices = newVerts.ToArray();
        mesh.SetTriangles(mesh.triangles, 0);
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
        mesh.MarkModified();

        filter.mesh = mesh;

        MeshRenderer renderer = map.GetComponent<MeshRenderer>();
        
        
        foreach (Animator s in schmeese) s.GetComponent<MooseAnimationScript>().parent = this;
        InvokeRepeating("PlayRandomAnimation", 1f, 1.5f / schmeese.Length);
        InvokeRepeating("MoveSchmoose", 1f, 0.02f);
    }

    void PlayRandomAnimation()
    {
        int youInParticular = 0;
        int timeout = 0;
        do
        {
            if (timeout++ > 1000000) return;
            youInParticular = Random.Range(0, schmeese.Length);
        } while (walkingSchmoose.Contains(schmeese[youInParticular].transform) || jumpingSchmoose.Contains((schmeese[youInParticular].transform)));
            
        
        RaycastHit hit;
        Transform curr = schmeese[youInParticular].transform;
        Ray ray = new Ray(curr.position + new Vector3(0f,100f, 0f), Vector3.down);
            
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << 3))
        {
            curr.position = new Vector3(curr.position.x, hit.point.y, curr.position.z);
            int triangleIndex = hit.triangleIndex;
            MeshFilter f = hit.transform.GetComponent<MeshFilter>();
            Mesh m = f.mesh;
            int vert = m.triangles[triangleIndex];

            string bitPrint = "";
            //for (int i = 0; i < 32; ++i) bitPrint += (vert & (1 << i)) == (1 << i) ? "1" : "0";
            //Debug.Log(bitPrint);
            
                
            //f.mesh = m;
        }
        
        int action = Random.Range(0, 6);

        Animator current = schmeese[youInParticular];

        if (action == 4 || action == 3)    // Walking
        {
            walkingSchmoose.Add(current.gameObject.transform);
        }
        else if (action == 5)   // Jumping
        {
            jumpingSchmoose.Add(current.gameObject.transform);
        }

        current.gameObject.transform.eulerAngles = new Vector3(0f, Random.Range(current.gameObject.transform.eulerAngles.y - 60f, current.gameObject.transform.eulerAngles.y + 60f), 0f);

        
        current.Play(actions[action]);
    }

    private void MoveSchmoose()
    {
        for (int i = 0; i < walkingSchmoose.Count; i++)
        {
            walkingSchmoose[i].transform.position += -walkingSchmoose[i].transform.forward * 0.02f * speedMultiplierWalk;
        }
        
        for (int i = 0; i < jumpingSchmoose.Count; i++)
        {
            jumpingSchmoose[i].transform.position += -jumpingSchmoose[i].transform.forward * 0.02f * speedMultiplierJump;
        }
    }
}
