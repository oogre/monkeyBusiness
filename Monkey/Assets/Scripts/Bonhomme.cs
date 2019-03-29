using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonhomme : MonoBehaviour
{
    public GameObject[] handles;
    public HangOn[] hangOns;

    // Start is called before the first frame update
    void Start()
    {
        handles = GameObject.FindGameObjectsWithTag("handle");
        hangOns = this.GetComponentsInChildren<HangOn>();
        foreach (HangOn hangOn in hangOns) {
            Handle closestHandle = null;
            float closestDist = Mathf.Infinity;
            foreach (GameObject handle in handles) {
                if(!handle.GetComponent<Handle>().isFree()) continue;
                float dist = Vector3.Distance(handle.transform.position, hangOn.gameObject.transform.position);
                if (dist < closestDist)
                {
                    closestHandle = handle.GetComponent<Handle>();
                    closestDist = dist;
                }
            }
            if (closestHandle) {
                Debug.Log(closestHandle);
                closestHandle.setOccupiedBy(hangOn.gameObject);
                hangOn.gameObject.transform.position = closestHandle.gameObject.transform.position;
                hangOn.attachOn = closestHandle.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
