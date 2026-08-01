using Assets.Scripts.Dialogue.Data;
using Assets.Scripts.Utilities;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Dialogue.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            EventBus.ShowDialogueEvent += ShowDialogue;
            EventBus.AfterSceneLoadEvent += OnAfterSceneLoad;
        }

        private void OnDisable()
        {
            EventBus.ShowDialogueEvent -= ShowDialogue;
            EventBus.AfterSceneLoadEvent -= OnAfterSceneLoad;
        }

        private void ShowDialogue(DialogueLine dialogueLine)
        {
            if (dialogueLine == null)
            {
                _panel.SetActive(false);
                return;
            }

            _panel.SetActive(true);
            _text.text = dialogueLine.text;
        }
        
        private void OnAfterSceneLoad()
        {
            _panel.SetActive(false);
        }
    }
}
