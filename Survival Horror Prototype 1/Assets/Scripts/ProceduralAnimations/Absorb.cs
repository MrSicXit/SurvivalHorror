using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    public List<GameObject> affected = new List<GameObject>();

    public List<Transform> rig = new List<Transform>();

    void Start()
    {
        TraverseHierarchy();
    }

    void Pull(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.tag != "Projectile")
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            affected.Add(other.gameObject);

        }
    }

    void ManipulationPoints()
    {
        


    }

    void TraverseHierarchy()
    {
        List<Transform> childs = new List<Transform>();

        Utilities.GetAllChildren(transform.parent, ref childs);

        rig = childs;
        foreach (Transform t in childs)
        {

            Debug.Log(t.name);

        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (affected.IndexOf(other.gameObject) != -1)
    //    {
    //        other.GetComponent<Rigidbody>().useGravity = true;
    //        affected.Remove(other.gameObject);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}

static class Utilities
{

    public static void GetAllChildren(Transform parent, ref List<Transform> transforms)
    {
        foreach (Transform t in parent)
        {
            transforms.Add(t);

            GetAllChildren(t, ref transforms);
        }
    }
}