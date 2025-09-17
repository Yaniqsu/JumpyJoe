using UnityEngine;

namespace YNQ.JumpyJoe
{
    [CreateAssetMenu(fileName = "Player Movement Values", menuName = "Scriptable Objects/Player Movement Values")]
    public class PlayerMovementValues : ScriptableObject
    {
        public float minHeight;
        public float maxHeight;
        public float minSpeed;
        public float heightIncreaseSpeedRatio;
    }
}
