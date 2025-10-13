using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventController : MonoBehaviour
{
    [Serializable] 
    private struct AnimatorEvent
    {
        public string eventName;
        public UnityEvent eventTrigger;
    }

    [SerializeField] private AnimatorEvent[] eventsList;

    public void TriggerEvent(string eventName)
    {
        eventsList
            .Where(e => e.eventName == eventName)
            .ToList()
            .ForEach(e => e.eventTrigger.Invoke());
    }
}
