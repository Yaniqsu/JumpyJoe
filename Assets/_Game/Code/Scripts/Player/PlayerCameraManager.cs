using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    public enum CameraType
    {
        MainMenu = 0,
        Game = 1,
        GameOver = 2
    }
    
    /// <summary>
    /// Zarządza aktywną kamerą w grze, przełączając między różnymi widokami (menu, rozgrywka, ekran końcowy).
    /// </summary>
    public class PlayerCameraManager : MonoBehaviour
    {
        [SerializeField] private TableRow<CameraType, CinemachineCamera>[] _cameras;

        /// <summary>
        /// Przełącza aktywną kamerę na wybraną, dezaktywując pozostałe.
        /// </summary>
        /// <param name="camera">Typ kamery, na którą należy się przełączyć.</param>
        public void SwitchCamera(CameraType camera)
        {
            foreach (var c in _cameras)
                c.value.Priority = 0;

            _cameras.First(c => c.key == camera).value.Priority = 1;
        }
    }
}