using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsUI : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject progressBarAnchor;
    public GameObject progressBarAnchorEnd;

    Vector3 Difference;

    Queue<Bar> bars;

    float barSpeed;
    float seconds = 12;

    private class Bar {
        public GameObject barGO;
        public float timer;
        public float percent;

        public Bar(GameObject bgo, float thisTimer, float thisPercent) {
            barGO = bgo;
            timer = thisTimer;
            percent = thisPercent;
        }
    }

    private void Awake() {
        bars = new Queue<Bar>();
        Difference = progressBarAnchorEnd.transform.position - progressBarAnchor.transform.position;
    }

    public void SetBarSpeed(float bpm, float beatPerBar) {
        barSpeed = 60 / bpm * beatPerBar * 3;
        print(barSpeed);
        //Hardcode pas ça sti
        //float distanceToCross = 14 * 3;
        //barSpeed = distanceToCross / timeToTravel;
    }

    public void NewBar() {
        GameObject newBar = Instantiate(progressBar, progressBarAnchor.transform.position, Quaternion.identity);
        bars.Enqueue(new Bar(newBar, 0, 0));

        if (bars.Count >= 4) {
            Destroy(bars.Dequeue().barGO);
        }
    }

    private void Update() {
        foreach (Bar b in bars) {
            b.timer += Time.deltaTime;
            b.percent = b.timer / barSpeed;

            b.barGO.transform.position = progressBarAnchor.transform.position + Difference * b.percent;
        }
    }
}