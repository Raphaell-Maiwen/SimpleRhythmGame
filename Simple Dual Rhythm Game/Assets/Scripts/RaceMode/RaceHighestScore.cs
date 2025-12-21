using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceHighestScore : ScriptableObject
{
    public List<int> _highestScoreShort = new List<int>();
    public List<int> _highestScoreMedium = new List<int>();
    public List<int> _highestScoreLong = new List<int>();

    public List<int> _highestScoreInfinite = new List<int>();
}
