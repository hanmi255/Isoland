using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Dialogue.Data;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Dialogue.Logic
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private SO_DialogueData _dialogueDataNormal;
        [SerializeField] private SO_DialogueData _dialogueDataFinish;

        private Stack<DialogueLine> _dialogueNormalStack;
        private Stack<DialogueLine> _dialogueFinishStack;
        private bool _isTalking;

        private void Awake()
        {
            FillDialogueStacks();
        }

        public void ShowDialogueNormal()
        {
            if (!_isTalking)
            {
                StartCoroutine(DialogueRoutine(_dialogueNormalStack));
            }
        }

        public void ShowDialogueFinish()
        {
            if (!_isTalking)
            {
                StartCoroutine(DialogueRoutine(_dialogueFinishStack));
            }
        }

        private void FillDialogueStacks()
        {
            _dialogueNormalStack = new Stack<DialogueLine>(_dialogueDataNormal.dialogueLines);
            _dialogueFinishStack = new Stack<DialogueLine>(_dialogueDataFinish.dialogueLines);
        }

        private IEnumerator DialogueRoutine(Stack<DialogueLine> data)
        {
            _isTalking = true;

            if (data.TryPop(out var result))
            {
                EventBus.CallShowDialogue(result);
                yield return null;
                _isTalking = false;
            }
            else
            {
                EventBus.CallShowDialogue(null);
                FillDialogueStacks();
                _isTalking = false;
            }
        }
    }
}
