using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dialogue.Data
{
    [CreateAssetMenu(fileName = "SO_DialogueData", menuName = "SO/SO_DialogueData", order = 1)]
    public class SO_DialogueData : ScriptableObject
    {
        public List<DialogueLine> dialogueLines;
    }

    [System.Serializable]
    public class DialogueLine
    {
        public string text;
    }
}