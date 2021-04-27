﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartUI : MonoBehaviour
{
    //Maybe this variable is not relevant anymore?
    public GameObject tracker;
    public GameObject trackerAnchor;
    public GameObject trackerAnchorEnd;

    public GameObject barPrefab;

    public float barLength = 14f;

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
        //bars = GameObject.Find("Metronome").GetComponent<Metronome>().bars;

        iconsInPlay = new List<GameObject>();

        GenerateBars();
        SetupAnchorsAndCamera();
        PlaceTrackers();
    }

    public void SetUp(float bpm, float beatPerBar) {
        trackerSpeed = 60 / bpm * beatPerBar * 3 * Parameters.instance.bars;

        //Assign icons according to controller / keyboard style
        currentIcons = arrowIcons;

        Vector3 lowestIconsPosition = trackerAnchor.transform.position;
        lowestIconsPosition.y -= 1.5f;

        for (int i = 0; i < currentIcons.Count; i++) {
            Vector3 iconPos = lowestIconsPosition;
            iconPos.y += 0.7f * i;
            currentIcons[i].transform.position = iconPos;
        }


        trackers = new Queue<Tracker>();
        for (int i = trackersList.Count - 1; i > -1; i--) {
            GameObject newTracker = trackersList[i];
            if (i == 1) {
                //(bpm / 60) * beatPerBar * bars / 2
                trackers.Enqueue(new Tracker(newTracker, 0, 
                    0, newTracker.transform.position));
            }
            else {
                trackers.Enqueue(new Tracker(newTracker, 0, 0, newTracker.transform.position));
            }
        }
    }

    public void ChangeTempo(float bpm, float beatPerBar) {
        trackerSpeed = 60 / bpm * beatPerBar * 3 * Parameters.instance.bars;
        foreach (Tracker b in trackers) {
            b.timer /= 1.2f;
        }
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
        Vector3 notePos = currentIcons[i].transform.position;
        notePos.x = currentTracker.transform.position.x;
        notePos.z -= 0.5f;
        iconsInPlay.Add(Instantiate(currentIcons[i], notePos, Quaternion.identity));
    }

    public void PlayedNote(int index) {
        iconsInPlay[index].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(128,128,128, 255);
    }

    public void EraseAllNotes() {
        foreach (GameObject icon in iconsInPlay) {
            Destroy(icon);
        }

        iconsInPlay.Clear();
        currentTracker = null;
    }

    public void UnPlayedAllNotes() {
        for (int i = 0; i < iconsInPlay.Count; i++) {
            iconsInPlay[i].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 255);
        }
    }

    void GenerateBars() {
        for (int i = 1; i < Parameters.instance.bars; i++) {
            Vector3 newBarPos = Vector3.zero;
            newBarPos.x += barLength * (i);
            Instantiate(barPrefab, newBarPos, Quaternion.identity);
        }
    }

    private void PlaceTrackers() {
        //trackersList[0].transform.position = trackerAnchor.transform.position;

        //Ce n'est qu'un au revoir
        Vector3 middleTrackerPos = trackerAnchor.transform.position;
        middleTrackerPos.x = barLength * Parameters.instance.bars - 7;
        trackersList[1].transform.position = middleTrackerPos;

        trackersList[2].transform.position = trackerAnchorEnd.transform.position;
    }

    private void SetupAnchorsAndCamera() {
        Vector3 entryBarPos = GameObject.Find("EntryBar").transform.position;
        Vector3 exitBarPos = entryBarPos;
        exitBarPos.x = exitBarPos.x + barLength * Parameters.instance.bars;

        float partLength = barLength * Parameters.instance.bars;

        Vector3 trackerAnchorPos = entryBarPos;
        trackerAnchorPos.x -= partLength;
        trackerAnchor.transform.position = trackerAnchorPos;

        Vector3 trackerAnchorEndPos = exitBarPos;
        trackerAnchorEndPos.x += partLength;
        trackerAnchorEnd.transform.position = trackerAnchorEndPos;

        Difference = trackerAnchorEnd.transform.position - trackerAnchor.transform.position;

        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.x = (entryBarPos.x + exitBarPos.x) / 2;
        Camera.main.transform.position = cameraPos;
        Camera.main.orthographicSize = 5 * Parameters.instance.bars;
    }
}