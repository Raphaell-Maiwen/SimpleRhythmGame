using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsUI : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject progressBarAnchor;
    public GameObject progressBarAnchorEnd;

    [HideInInspector]
    public GameObject currentBar;

    Queue<Bar> bars;

    Vector3 Difference;
    float barSpeed;
    float seconds = 12;

    public List<GameObject> arrowIcons;
    public List<GameObject> controllerIcons;
    public List<GameObject> keyboardFIcons;
    List<GameObject> currentIcons;

    List<GameObject> iconsInPlay;

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
        iconsInPlay = new List<GameObject>();
        Difference = progressBarAnchorEnd.transform.position - progressBarAnchor.transform.position;
    }

    public void SetUp(float bpm, float beatPerBar) {
        barSpeed = 60 / bpm * beatPerBar * 3;
        currentIcons = arrowIcons;
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

    public void DrawNewNote(int i) {
        Vector3 notePos = currentBar.transform.position;
        notePos.z -= 0.5f;
        iconsInPlay.Add(Instantiate(currentIcons[i], notePos, Quaternion.identity));
    }

    public void EraseAllNotes() {
        foreach (GameObject icon in iconsInPlay) {
            Destroy(icon);
        }

        iconsInPlay.Clear();
        currentBar = null;
    }
}