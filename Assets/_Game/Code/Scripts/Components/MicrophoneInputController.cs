using System;
using UnityEngine;
using UnityEngine.Events;

namespace YNQ.JumpyJoe
{
    
    public class MicrophoneInputController : MonoBehaviour
    {
        [SerializeField] private int _samplesCount;

        private string _micName;
        private float[] _samples;

        private AudioClip _microphoneOutput;
        
        public float DbValue { get; private set; }
        public float RmsValue { get; private set; }
        public bool Enabled { get; set; } = false;
        
        

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
                return;
            
            var startPos = Mathf.Max(0, Microphone.GetPosition(_micName) - _samplesCount);
            _microphoneOutput.GetData(_samples, startPos);

            var sum = 0f;
            foreach (var sample in _samples)
            {
                sum += Mathf.Pow(sample, 2);
            }            
            RmsValue = Mathf.Sqrt(sum / _samples.Length);
            DbValue = 20f * Mathf.Log10(Mathf.Max(RmsValue, 1e-6f));
        }
    }
}
