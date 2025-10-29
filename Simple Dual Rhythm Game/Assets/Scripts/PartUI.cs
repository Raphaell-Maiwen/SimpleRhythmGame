using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartUI : MonoBehaviour
{
    [SerializeField] private PlayersManager _playersManager;
    [SerializeField] private Parameters _parameters;
    public GameObject trackerAnchor;
    public GameObject trackerAnchorEnd;

    public GameObject barPrefab;

    public float barLength = 14f;

    [HideInInspector]
    public GameObject currentTracker;

    //Maybe?
    public List<GameObject> trackersList;
    Queue<TrackerData> trackers;

    Vector3 Difference;
    float trackerSpeed;

    public List<GameObject> arrowIcons;
    public List<GameObject> controllerIcons;
    public List<GameObject> keyboardFIcons;
    List<GameObject> currentIcons;

    List<NoteIcon> iconsInPlay;

    bool firstTick;
    
    private bool player1 = true;

    public class TrackerData {
        public GameObject trackerGO;
        public float timer;
        public float percent;
        public Vector3 startingPos;

        public TrackerData(GameObject tgo, float thisTimer, float thisPercent, Vector3 thisStartingPos) {
            trackerGO = tgo;
            timer = thisTimer;
            percent = thisPercent;
            startingPos = thisStartingPos;
        }
    }

    private void Awake() {
        //bars = GameObject.Find("Metronome").GetComponent<Metronome>().bars;

        iconsInPlay = new List<NoteIcon>();

        GenerateBars();
        SetupAnchorsAndCamera();
        PlaceTrackers();
    }

    public void SetUp(float bpm, float beatPerBar) {
        trackerSpeed = 60 / bpm * beatPerBar * 3 * _parameters.bars;

        //Assign icons according to controller / keyboard style
        if (_parameters.inputMode == InputMode.keyboard)
        {
            currentIcons = arrowIcons;
        }
        else if (_parameters.inputMode == InputMode.keytar)
        {
            currentIcons = keyboardFIcons;
        }


        /*Vector3 lowestIconsPosition = trackerAnchor.transform.position;
        lowestIconsPosition.y -= 1.5f;

        for (int i = 0; i < currentIcons.Count; i++) {
            Vector3 iconPos = lowestIconsPosition;
            iconPos.y += 0.7f * i;
            currentIcons[i].transform.position = iconPos;
        }*/


        trackers = new Queue<TrackerData>();
        for (int i = trackersList.Count - 1; i > -1; i--) {
            GameObject newTracker = trackersList[i];
            if (i == 1) {
                //(bpm / 60) * beatPerBar * bars / 2
                trackers.Enqueue(new TrackerData(newTracker, 0, 
                    0, newTracker.transform.position));
            }
            else {
                trackers.Enqueue(new TrackerData(newTracker, 0, 0, newTracker.transform.position));
            }
        }
    }

    public void ChangeTempo(float bpm, float beatPerBar) {
        trackerSpeed = 60 / bpm * beatPerBar * 3 * _parameters.bars;
        foreach (TrackerData b in trackers) {
            b.timer /= 1.2f;
        }
    }

    public void NewBar(Metronome.GameState gameState)
    {
        Debug.Log("New bar");
        firstTick = true;

        TrackerData newTrackerData = trackers.Dequeue();
        newTrackerData.startingPos = trackerAnchor.transform.position;
        newTrackerData.trackerGO.transform.position = newTrackerData.startingPos;
        newTrackerData.timer = 0;
        newTrackerData.percent = 0;
        
        newTrackerData.trackerGO.GetComponent<Tracker>().AssignIcon(gameState, player1);

        trackers.Enqueue(newTrackerData);
    }

    public void TogglePlayer1()
    {
        player1 = !player1;
    }

    private void Update() {
        if (firstTick) {
            foreach (TrackerData b in trackers) {
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
        iconsInPlay.Add(Instantiate(currentIcons[i], notePos, Quaternion.identity).GetComponent<NoteIcon>());
    }

    public void PlayedNote(int index) {
        iconsInPlay[index].SetMissedIcon();
    }

    public void EraseAllNotes() {
        foreach (NoteIcon icon in iconsInPlay) {
            Destroy(icon.gameObject);
        }

        iconsInPlay.Clear();
        currentTracker = null;
    }

    public void UnPlayedAllNotes() {
        for (int i = 0; i < iconsInPlay.Count; i++) {
            iconsInPlay[i].ResetIcon();
        }
    }

    void GenerateBars() {
        for (int i = 1; i < _parameters.bars; i++) {
            Vector3 newBarPos = Vector3.zero;
            newBarPos.x += barLength * (i);
            Instantiate(barPrefab, newBarPos, Quaternion.identity);
        }
    }

    private void PlaceTrackers() {
        //trackersList[0].transform.position = trackerAnchor.transform.position;

        //Ce n'est qu'un au revoir
        Vector3 middleTrackerPos = trackerAnchor.transform.position;
        middleTrackerPos.x = barLength * _parameters.bars - 7;
        trackersList[1].transform.position = middleTrackerPos;

        trackersList[2].transform.position = trackerAnchorEnd.transform.position;
    }

    private void SetupAnchorsAndCamera() {
        Vector3 entryBarPos = GameObject.Find("EntryBar").transform.position;
        Vector3 exitBarPos = entryBarPos;
        exitBarPos.x = exitBarPos.x + barLength * _parameters.bars;

        float partLength = barLength * _parameters.bars;

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
        Camera.main.orthographicSize = 5 * _parameters.bars;
    }
}