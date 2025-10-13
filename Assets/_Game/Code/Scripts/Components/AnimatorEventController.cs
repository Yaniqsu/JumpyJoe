using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Klasa odpowiadająca za wysyłanie zdarzen bezposednio z animacji
/// </summary>
public class AnimatorEventController : MonoBehaviour
{
    [Serializable] 
    private struct AnimatorEvent
    {
        public string eventName;
        public UnityEvent eventTrigger;
    }

    [SerializeField] private AnimatorEvent[] eventsList;

    /// <summary>
    /// Znajduje obiekt typu AnimatorEvent o tej samej nazwie i wywołuje event eventTrigger
    /// </summary>
    /// <param name="eventName">Nazwa wywoływanego zdarzenia</param>
    public void TriggerEvent(string eventName)
    {
        eventsList
            .Where(e => e.eventName == eventName)
            .ToList()
            .ForEach(e => e.eventTrigger.Invoke());
    }
}
