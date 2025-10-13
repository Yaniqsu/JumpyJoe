using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Klasa odpowiadająca za zbieranie sygnału z mikrofonu
    /// </summary>
    public class MicrophoneInputController : MonoBehaviour
    {
        [SerializeField] private int _samplesCount;
        [SerializeField] private float _volumeSmoothTime = 0.1f;

        private string _micName;
        private float[] _samples;

        private AudioClip _microphoneOutput;
        
        public float DbValue { get; private set; }
        public float RmsValue { get; private set; }
        public bool Enabled { get; set; } = false;
        
        

        /// <summary>
        /// Rozpoczyna nagrywanie sygnału
        /// </summary>
        public void StartRecording()
        {
            if (Microphone.devices.Length == 0)
            {
                Debug.LogError("Microphone not found");
                return;
            }
            
            _micName = Microphone.devices[0];
            _samples = new float[_samplesCount];
            
            _microphoneOutput = Microphone.Start(_micName, true, 1, AudioSettings.outputSampleRate);
            Enabled = true;
        }

        private void Update()
        {
            if (!Enabled)
            {
                RmsValue = DbValue = float.MinValue;
                return;
            }

            var volume = GetMicVolume();
            RmsValue = Mathf.Sqrt(volume / _samplesCount);
            DbValue = 20f * Mathf.Log10(RmsValue / 0.1f);
        }
        
        /// <summary>
        /// Zbiera średni wynik z próbki
        /// </summary>
        /// <returns>Średni wynik z próbki</returns>
        private float GetMicVolume()
        {
            var data = new float[_samplesCount];
            var micPosition = Microphone.GetPosition(_micName) - _samplesCount + 1;
            if (micPosition < 0) return 0;

            _microphoneOutput.GetData(data, micPosition);

            return data.Select(Mathf.Abs).Average();
        }
    }
}
