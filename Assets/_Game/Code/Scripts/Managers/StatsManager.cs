using System;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Klasa odpowiada za przechowywanie i aktualizację statystyk rozgrywki.
    /// </summary>
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
        
        public event Action<float> OnDistanceChanged;
        public event Action<float> OnHeightChanged;

        /// <summary>
        /// Tworzy nową instancję klasy StatsManager i ustawia wartości startowe.
        /// </summary>
        /// <param name="gameValues">Obiekt zawierający dane konfiguracyjne gry, takie jak dystans na skok i mnożnik wysokości.</param>
        public StatsManager(GameValues gameValues)
        {
            _distancePerJump = gameValues.DistancePerJump;
            _heightMultiplier = gameValues.HeightMultiplier;
            _bestKey = gameValues.BestKeyName;
            
            _jumps = 0;
            _rawHeight = 0;
        }
        
        /// <summary>
        /// Metoda wywoływana po każdym skoku gracza.
        /// Zwiększa liczbę skoków i sumę wysokości, a następnie aktualizuje statystyki.
        /// </summary>
        /// <param name="height">Wysokość ostatniego skoku.</param>
        public void OnPlayerJump(float height)
        {
            _jumps += 1;
            _rawHeight += height;
            
            OnDistanceChanged?.Invoke(CurrentDistance);
            OnHeightChanged?.Invoke(HeightSum);
        }

        /// <summary>
        /// Zwraca najlepszy dystans w bieżącej sesji.
        /// </summary>
        /// <param name="currentDistance">Aktualny dystans gracza.</param>
        /// <returns>Najlepszy dystans osiągnięty w tej sesji.</returns>
        public float GetBestDistanceSession(float currentDistance)
        {
            _best = Mathf.Max(_best, currentDistance);
            return _best;
        }
        
        /// <summary>
        /// Zwraca i aktualizuje najlepszy globalny dystans (zapisany w PlayerPrefs).
        /// </summary>
        /// <param name="currentDistance">Aktualny dystans gracza.</param>
        /// <returns>Najlepszy dystans zapisany w pamięci gry.</returns>
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
