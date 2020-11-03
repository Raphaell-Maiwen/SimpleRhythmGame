using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    PartUI partUIScript;

    private void Start() {
        partUIScript = GameObject.Find("Part").GetComponent<PartUI>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "TrackerTrigger") {
            if(partUIScript.currentTracker == null)
                partUIScript.currentTracker = this.gameObject;
        }
    }
}