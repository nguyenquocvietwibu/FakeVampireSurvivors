using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
    Unknow,
    Health,
    MoveSpeed,
    Attack,
    ProjectileAmount,
    Size,
    ProjectileCoolDown,
    ProjectileDuration,
    Level,
    Experience,
    ReviveAmount,
    ProjectileSpeed,
}

[Serializable]
public class StatEntry
{
    [SerializeField] private Stat _statKey;
    [SerializeField] private float _statValue;

    public Stat statKey { get => _statKey; set => _statKey = value; }
    public float statValue { get => _statValue; set => _statValue = value;  }
}

public class StatManager : MonoBehaviour
{
    public List<StatEntry> statEntryList = new();

    private Dictionary<Stat, StatEntry> _statEntryDict = new();

    private void Awake()
    {
        foreach (StatEntry statEntry in statEntryList)
        {
            if (!_statEntryDict.ContainsKey(statEntry.statKey))
            {
                _statEntryDict.Add(statEntry.statKey, statEntry);
            }
        }
    }

    public void SetStatValue(Stat stat, float value)
    {
        if (_statEntryDict.ContainsKey(stat))
        {
            _statEntryDict[stat].statValue = value;
        }
    }

    public bool TryGetStatValue(Stat stat, out float statValueResult)
    {
        if (_statEntryDict.ContainsKey(stat))
        {
            statValueResult = _statEntryDict[stat].statValue;
            return true;
        }
        else
        {
            statValueResult = default;
            return false;
        }
    }

    public bool TryGetStatKey(Stat stat, out Stat statKeyResult)
    {
        if (_statEntryDict.ContainsKey(stat))
        {
            statKeyResult = _statEntryDict[stat].statKey;
            return true;
        }
        else
        {
            statKeyResult = Stat.Unknow;
            return false;
        }    
    }

}
