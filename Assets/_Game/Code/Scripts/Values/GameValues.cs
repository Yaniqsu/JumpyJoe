using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Przechowuje zmienne globalne – takie jak mnożniki skoku i dystansu, klucz do zapisywania globalnego rekordu
    /// </summary>
    [CreateAssetMenu(fileName = "GameValues", menuName = "Scriptable Objects/Game Values")]
    public class GameValues : ScriptableObject
    {
        [SerializeField] private float _distancePerJump;
        [SerializeField] private float _heightMultiplier;
        [SerializeField] private string _bestKeyName;
        
        public float DistancePerJump => _distancePerJump;
        public float HeightMultiplier => _heightMultiplier;
        public string BestKeyName => _bestKeyName;
    }
}