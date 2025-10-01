using UnityEngine;

namespace YNQ.JumpyJoe
{
    [CreateAssetMenu(fileName = "MenuStrings", menuName = "Scriptable Objects/Menu Strings")]
    public class MenuStrings : ScriptableObject
    {
        public ValuePattern distanceText;
        public ValuePattern heightText;
        public ValuePattern bestSession;
        public ValuePattern bestGlobal;
    }
}