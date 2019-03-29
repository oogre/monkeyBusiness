using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{

    public GameObject occupiedBy;

    public bool isFree() {
        return !occupiedBy;
    }

    public void setOccupiedBy(GameObject value) {
        occupiedBy = value;
    }


}
