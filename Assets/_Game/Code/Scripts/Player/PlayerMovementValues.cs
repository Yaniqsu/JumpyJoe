using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Przechowuje dane związanie z poruszaniem się
    /// </summary>
    [CreateAssetMenu(fileName = "Player Movement Values", menuName = "Scriptable Objects/Player Movement Values")]
    public class PlayerMovementValues : ScriptableObject
    {
        public float minHeight;
        public float maxHeight;
        public float minSpeed;
        public float heightIncreaseSpeedRatio;
    }
}
