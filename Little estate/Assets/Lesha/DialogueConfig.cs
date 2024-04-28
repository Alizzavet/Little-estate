using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Configs/Monologue")]
public class DialogueConfig : ScriptableObject
{
    [SerializeField] private List<string> _allPhrases = new();

    public List<string> AllPhrases => _allPhrases;
}
