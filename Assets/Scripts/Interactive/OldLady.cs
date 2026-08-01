using Assets.Scripts.Dialogue.Logic;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    [RequireComponent(typeof(DialogueController))]
    public class OldLady : InteractiveBase
    {
        private DialogueController _dialogueController;

        private void Awake()
        {
            _dialogueController = GetComponent<DialogueController>();
        }

        protected override void UseItem()
        {
            _dialogueController.ShowDialogueFinish();
        }
        
        public override void EmptyClick()
        {
            if(_isInteracted)
            {
                _dialogueController.ShowDialogueFinish();
            }
            else
            {
                _dialogueController.ShowDialogueNormal();
            }
        }
    }
}
