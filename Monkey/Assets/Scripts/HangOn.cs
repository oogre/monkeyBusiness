using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangOn : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject oldAttachOn;
    public GameObject attachOn;

    public bool enableTrigger = false;

    public float disableTriggerDuring = 1.0f;
    float t0DisableTriggerDuring = 0.0f;

    Rigidbody rb;

    RigidbodyConstraints fixe = RigidbodyConstraints.FreezeAll;
    RigidbodyConstraints free = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = free;
    }

    void Update()
    {
        if (attachOn) {
            this.gameObject.transform.position = attachOn.gameObject.transform.position;
        }

        if (attachOn && !oldAttachOn)
        {
            rb.constraints = fixe;
        }

        if(!attachOn && oldAttachOn)
        {
            rb.constraints = free;
            t0DisableTriggerDuring = Time.timeSinceLevelLoad;
            oldAttachOn.GetComponent<Handle>().setOccupiedBy(null);
        }
        enableTrigger = isAbleToReAttach();
        oldAttachOn = attachOn;
    }

    private bool isAbleToReAttach() {
        return Time.timeSinceLevelLoad - t0DisableTriggerDuring > disableTriggerDuring;
    }

    private void OnTriggerEnter(Collider other)
    {
        Handle handle = other.GetComponent<Handle>();
        if (enableTrigger && handle && handle.isFree()) {
            attachOn = handle.gameObject;
            handle.setOccupiedBy(this.gameObject);
        }
    }
}
