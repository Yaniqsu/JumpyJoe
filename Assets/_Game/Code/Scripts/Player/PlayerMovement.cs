using System;
using System.Collections;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Odpowiada za ruch i animację skoku gracza, w tym obliczanie trajektorii oraz wysokości.
    /// </summary>
    public class PlayerMovement
    {
        private PlayerController _playerController;
        private PlayerMovementValues _values;
        private bool _isJumping = false;
        public float CurrentHeight { get; private set; } = 1;

        public event Action OnJumpStart;
        public event Action OnJumpEnd = null;
        
        /// <summary>
        /// Tworzy nowy komponent ruchu gracza i ustawia jego parametry.
        /// </summary>
        /// <param name="playerController">Kontroler gracza zarządzający cyklem życia.</param>
        /// <param name="values">Zbiór wartości sterujących ruchem i skokiem.</param>
        public PlayerMovement(PlayerController playerController, PlayerMovementValues values)
        {
            _playerController = playerController;
            _values = values;
        }

        /// <summary>
        /// Uruchamia skok gracza, jeśli nie jest obecnie w trakcie innego skoku.
        /// </summary>
        /// <param name="startPos">Pozycja początkowa skoku.</param>
        /// <param name="endPos">Pozycja końcowa skoku.</param>
        public void Jump(Vector3 startPos, Vector3 endPos)
        {
            if (!_isJumping)
            {
                _playerController.StartCoroutine(HandleJump(startPos, endPos));
                OnJumpStart?.Invoke();
            }
        }

        /// <summary>
        /// Ustawia wysokość następnego skoku w oparciu o przekazany współczynnik (0–1).
        /// </summary>
        /// <param name="heightRatio">Wartość określająca względną wysokość skoku.</param>
        public void SetHeight(float heightRatio)
        {
            if (_isJumping)
                return;
            
            CurrentHeight = Mathf.Lerp(_values.minHeight, _values.maxHeight, heightRatio);
        }

        /// <summary>
        /// Obsługuje logikę skoku w czasie — interpoluje pozycję gracza po krzywej Béziera.
        /// </summary>
        /// <param name="startPos">Pozycja startowa.</param>
        /// <param name="endPos">Pozycja końcowa.</param>
        private IEnumerator HandleJump(Vector3 startPos, Vector3 endPos)
        {
            var time = _values.minSpeed + CurrentHeight * _values.heightIncreaseSpeedRatio;
            var elapsedTime = 0f;
            _isJumping = true;

            while (elapsedTime < time)
            {
                _playerController.transform.position = GetQuadraticBezierPoint(startPos, endPos, elapsedTime / time);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _isJumping = false;
            _playerController.transform.position = endPos;
            OnJumpEnd?.Invoke();
        }

        /// <summary>
        /// Zwraca punkt na krzywej kwadratowej Béziera na podstawie dwóch pozycji i współczynnika czasu.
        /// </summary>
        /// <param name="A">Pozycja początkowa.</param>
        /// <param name="B">Pozycja końcowa.</param>
        /// <param name="t">Ułamek czasu (0–1).</param>
        /// <returns>Wektor 3D określający pozycję gracza w danym momencie skoku.</returns>
        private Vector3 GetQuadraticBezierPoint(Vector3 A, Vector3 B, float t)
        {
            Vector3 M = (A + B) / 2f;
            Vector3 C = new Vector3(M.x, M.y + CurrentHeight, M.z);
            Vector3 P = (1 - t) * (1 - t) * A + 2 * (1 - t) * t * C + t * t * B;

            return P;
        }
    }
}
