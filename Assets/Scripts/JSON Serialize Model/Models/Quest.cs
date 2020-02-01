using UnityEngine;

[System.Serializable]
public class Quest
{
    public string id;
    public string title;
    public string description;
    public string topic;
    public int reward;
    public int level;
    public string npc;
    public Task[] tasks;
}