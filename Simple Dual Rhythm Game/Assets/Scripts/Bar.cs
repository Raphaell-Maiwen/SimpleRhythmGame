using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    BarsUI barsUIScript;

    private void Start() {
        barsUIScript = GameObject.Find("Bar").GetComponent<BarsUI>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "BarTrigger") {
            if(barsUIScript.currentBar == null)
                barsUIScript.currentBar = this.gameObject;
        }
    }
}