using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartUI : MonoBehaviour
{
    public GameObject tracker;
    public GameObject trackerAnchor;
    public GameObject trackerAnchorEnd;

    [HideInInspector]
    public GameObject currentTracker;

    Queue<Tracker> trackers;

    Vector3 Difference;
    float trackerSpeed;
    float seconds = 12;

    public List<GameObject> arrowIcons;
    public List<GameObject> controllerIcons;
    public List<GameObject> keyboardFIcons;
    List<GameObject> currentIcons;

    List<GameObject> iconsInPlay;

    private class Tracker {
        public GameObject trackerGO;
        public float timer;
        public float percent;

        public Tracker(GameObject tgo, float thisTimer, float thisPercent) {
            trackerGO = tgo;
            timer = thisTimer;
            percent = thisPercent;
        }
    }

    private void Awake() {
        trackers = new Queue<Tracker>();
        iconsInPlay = new List<GameObject>();
        Difference = trackerAnchorEnd.transform.position - trackerAnchor.transform.position;
    }

    public void SetUp(float bpm, float beatPerBar) {
        trackerSpeed = 60 / bpm * beatPerBar * 3;
        currentIcons = arrowIcons;
    }

    public void NewBar() {
        GameObject newTracker = Instantiate(tracker, trackerAnchor.transform.position, Quaternion.identity);
        trackers.Enqueue(new Tracker(newTracker, 0, 0));

        //trackers.Enqueue(trackers.Dequeue());
        //currentTracker = trackers.Peek().trackerGO;

        if (trackers.Count >= 4) {
            Destroy(trackers.Dequeue().trackerGO);
        }
    }

    private void Update() {
        foreach (Tracker b in trackers) {
            b.timer += Time.deltaTime;
            b.percent = b.timer / trackerSpeed;

            b.trackerGO.transform.position = trackerAnchor.transform.position + Difference * b.percent;
        }
    }

    public void DrawNewNote(int i) {
        Vector3 notePos = currentTracker.transform.position;
        notePos.z -= 0.5f;
        iconsInPlay.Add(Instantiate(currentIcons[i], notePos, Quaternion.identity));
    }

    public void EraseAllNotes() {
        foreach (GameObject icon in iconsInPlay) {
            Destroy(icon);
        }

        iconsInPlay.Clear();
        currentTracker = null;
    }
}