using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(RefreshLighting());
    }

    IEnumerator RefreshLighting()
    {
        yield return null; // Wait one frame
        DynamicGI.UpdateEnvironment();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Renderer>().enabled = true;
    }
}
