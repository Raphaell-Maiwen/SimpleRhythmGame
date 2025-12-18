using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartUI : MonoBehaviour
{
    [SerializeField] private PlayersManager _playersManager;
    [SerializeField] private Parameters _parameters;
    public GameObject trackerAnchor;
    public GameObject trackerAnchorEnd;
    [SerializeField] private Transform _notesIconAnchor;
    [SerializeField] private Transform _entryBar;

    public GameObject barPrefab;

    public float barLength = 14f;

    [HideInInspector]
    public GameObject currentTracker;

    public List<GameObject> trackersList;
    Queue<TrackerData> trackers;

    Vector3 Difference;
    float trackerSpeed;

    [SerializeField] private List<NestedNotesList> _arrowNotesPool;
    [SerializeField] private List<NestedNotesList> _FKeysNotesPool;
    private List<NestedNotesList> _currentPool = new List<NestedNotesList>();

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
            _currentPool = _arrowNotesPool;
        }
        else if (_parameters.inputMode == InputMode.keytar)
        {
            _currentPool = _FKeysNotesPool;
        }

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
        var noteIconsList = _currentPool[i].noteIcons;

        Vector3 notePos = noteIconsList[0].transform.position;
        notePos.x = currentTracker.transform.position.x;

        var note = noteIconsList[noteIconsList.Count - 1];
        iconsInPlay.Add(note);
        noteIconsList.RemoveAt(noteIconsList.Count - 1);

        note.transform.position = notePos;
        note.SetIndex(i);
    }
    
    //TODO: Make more flexible? Remove entirely?
    public void ChangeNoteState(int index, bool isSuccessfullyPlayed) {
        if (isSuccessfullyPlayed)
        {
            iconsInPlay[index].SetPlayedIcon();
        }
        else {
            iconsInPlay[index].SetMissedIcon();
        }
    }

    public void EraseAllNotes() {
        foreach (NoteIcon icon in iconsInPlay) {
            _currentPool[icon.GetIndex()].noteIcons.Add(icon);
            icon.transform.position = _notesIconAnchor.transform.position;
            icon.ResetIcon();
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
        Vector3 middleTrackerPos = trackerAnchor.transform.position;
        middleTrackerPos.x = barLength * _parameters.bars - 7;
        trackersList[1].transform.position = middleTrackerPos;

        trackersList[2].transform.position = trackerAnchorEnd.transform.position;
    }

    private void SetupAnchorsAndCamera() {
        Vector3 entryBarPos = _entryBar.position;
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

    [System.Serializable]
    public class NestedNotesList {
        public List<NoteIcon> noteIcons;
    }
}