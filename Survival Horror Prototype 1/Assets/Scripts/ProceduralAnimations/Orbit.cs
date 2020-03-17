using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public List<GameObject> affected = new List<GameObject>();
    [SerializeField] float gravityMultiplier = 2;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.tag != "Projectile")
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            affected.Add(other.gameObject);
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (affected.IndexOf(other.gameObject) != -1)
        {
            other.GetComponent<Rigidbody>().useGravity = true;
            affected.Remove(other.gameObject);
        }
    }


    void FixedUpdate()
    {
        foreach (GameObject cur in affected)
        {
            float distance = Vector3.Distance(transform.position, cur.transform.position);
            cur.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.position - cur.transform.position) * (1 / distance * distance) * gravityMultiplier);
            //this is basically newton's gravity law with the two masses each being one
        }

    }
}
