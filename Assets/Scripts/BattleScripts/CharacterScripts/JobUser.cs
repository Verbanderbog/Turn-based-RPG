using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class JobUser : Character
{
    public int currentJobIndex = 0;
    public List<Job> JobList;
    private void OnValidate()
    {
        
        //TODO: Remove old job stat modifiers, apply job stat modifiers, remove old job moves from moveset, add job moves to moveset and remove duplicates
    }
}
