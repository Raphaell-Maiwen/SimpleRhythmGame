using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelMode : GameLoop
{
    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex) 
    {
        Debug.Log("noteIndex");
        /*if (playerIndex != currentPlayerIndex) return;

        //Maybe modify with timing, or collision or something
        if (currentState == GameState.Silence && nextState == GameState.Recording) return;
        
        if (currentState == GameState.Recording) {
            riffLength++;
            UIScript.DrawNewNote(noteIndex);

            PlayNoteSound(noteIndex);
        }
        else if (currentState == GameState.Playing || nextState == GameState.Playing) {
            if (IsRightNote(noteIndex)) {
                notesSucceeded++;
                int points = notesSucceeded * 10;
                playersScript.MakePoints(points);

                if (currentState == GameState.Silence)
                {
                    Debug.Log("Made points in silence");
                }

                //Bonus points for a perfect solo
                if (notesSucceeded == riffLength && !madeMistake) {
                    //Should be 200 for composer and 400 for the other, will change at some point
                    playersScript.MakePoints(400);
                }

                PlayNoteSound(noteIndex);
            }
            else {
                madeMistake = true;
                //int penalty = ((riffLength * (riffLength + 1)) / 2 * 10) / riffLength;

                int penalty = 0;
                switch (riffLength)
                {
                    case < 4: penalty = 10;
                        break;
                    case < 10: penalty = 20;
                        break;
                    case < 25: penalty = 50;
                        break;
                    default: penalty = 100;
                        break;
                }
                
                playersScript.MakePoints(-penalty);
                _audioManager.PlaySound("WrongNote");
            }
        }*/
    }
}

public enum GameState
{
    Recording,
    Playing,
    Silence,
    ChangePlayer
};