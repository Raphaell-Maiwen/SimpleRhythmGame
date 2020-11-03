﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartUI : MonoBehaviour
{
    //Maybe this variable is not relevant anymore?
    public GameObject tracker;
    public GameObject trackerAnchor;
    public GameObject trackerAnchorEnd;

    [HideInInspector]
    public GameObject currentTracker;

    //Maybe?
    public List<GameObject> trackersList;
    Queue<Tracker> trackers;

    Vector3 Difference;
    float trackerSpeed;

    public List<GameObject> arrowIcons;
    public List<GameObject> controllerIcons;
    public List<GameObject> keyboardFIcons;
    List<GameObject> currentIcons;

    List<GameObject> iconsInPlay;

    bool firstTick;

    public class Tracker {
        public GameObject trackerGO;
        public float timer;
        public float percent;
        public Vector3 startingPos;

        public Tracker(GameObject tgo, float thisTimer, float thisPercent, Vector3 thisStartingPos) {
            trackerGO = tgo;
            timer = thisTimer;
            percent = thisPercent;
            startingPos = thisStartingPos;
        }
    }

    private void Awake() {
        trackers = new Queue<Tracker>();

        PlaceTrackers();

        for (int i = trackersList.Count -1; i > -1; i--) {
            GameObject newTracker = trackersList[i];
            trackers.Enqueue(new Tracker(newTracker, 0, 0, newTracker.transform.position));
        }

        iconsInPlay = new List<GameObject>();
        Difference = trackerAnchorEnd.transform.position - trackerAnchor.transform.position;
    }

    public void SetUp(float bpm, float beatPerBar) {
        trackerSpeed = 60 / bpm * beatPerBar * 3;
        currentIcons = arrowIcons;
    }

    public void NewBar() {
        firstTick = true;

        Tracker newTracker = trackers.Dequeue();
        newTracker.startingPos = trackerAnchor.transform.position;
        newTracker.trackerGO.transform.position = newTracker.startingPos;
        newTracker.timer = 0;
        newTracker.percent = 0;

        trackers.Enqueue(newTracker);
    }

    private void Update() {
        if (firstTick) {
            foreach (Tracker b in trackers) {
                b.timer += Time.deltaTime;
                b.percent = b.timer / trackerSpeed;

                b.trackerGO.transform.position = b.startingPos + Difference * b.percent;
            }
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

    private void PlaceTrackers() {
        //trackersList[0].transform.position = trackerAnchor.transform.position;
        //TODO: Place-les sti
        //trackersList[1].transform.position;
        trackersList[2].transform.position = trackerAnchorEnd.transform.position;
        //trackersList[3].transform.position = trackerAnchorEnd.transform.position;
    }
}