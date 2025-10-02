using UnityEngine;

namespace YNQ.JumpyJoe
{
    [CreateAssetMenu(fileName = "TimeSettings", menuName = "Scriptable Objects/Time Settings")]
    public class TimeSettings : ScriptableObject
    {
        public float maxTime;
        public float timeDecreaseOverTime;
        public float jumpTimeIncrease;
    }
}