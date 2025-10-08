using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class StatsManager
    {
        private readonly float _distancePerJump;
        private readonly float _heightMultiplier;
        private readonly string _bestKey;

        private int _jumps;
        private float _rawHeight;
        private static float _best = 0;
        public float CurrentDistance => _jumps * _distancePerJump;
        public float HeightSum => _rawHeight * _heightMultiplier;

        public StatsManager(GameValues gameValues)
        {
            _distancePerJump = gameValues.DistancePerJump;
            _heightMultiplier = gameValues.HeightMultiplier;
            _bestKey = gameValues.BestKeyName;
            
            _jumps = 0;
            _rawHeight = 0;
        }
        
        public void OnPlayerJump(float height)
        {
            _jumps += 1;
            _rawHeight += height;
        }

        public float GetBestDistanceSession(float currentDistance)
        {
            _best = Mathf.Max(_best, currentDistance);
            
            return _best;
        }
        
        public float GetBestDistanceGlobal(float currentDistance)
        {
            var best = PlayerPrefs.GetFloat(_bestKey, 0);

            best = Mathf.Max(best, currentDistance);
            
            PlayerPrefs.SetFloat(_bestKey, best);
            PlayerPrefs.Save();

            return best;
        }
    }
}