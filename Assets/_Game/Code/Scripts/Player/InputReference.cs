using UnityEngine;
using UnityEngine.InputSystem;

namespace YNQ.JumpyJoe
{
    [CreateAssetMenu(fileName = "Input Reference", menuName = "Scriptable Objects/Input Reference")]
    public class InputReference : ScriptableObject
    {
        public InputActionAsset action;
        public InputActionReference jumpAction;
        public InputActionReference alterHeightAction;
    }
}
