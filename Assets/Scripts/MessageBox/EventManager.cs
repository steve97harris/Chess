using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary <EventID, UnityEvent> eventDictionary;

        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

                    if (!eventManager)
                    {
                        Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init (); 
                    }
                }

                return eventManager;
            }
        }

        void Init ()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<EventID, UnityEvent>();
            }
        }

        public static void Register (EventID eventId, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue (eventId, out thisEvent))
            {
                thisEvent.AddListener (listener);
            } 
            else
            {
                thisEvent = new UnityEvent ();
                thisEvent.AddListener (listener);
                instance.eventDictionary.Add (eventId, thisEvent);
            }
        }

        public static void Unregister (EventID eventId, UnityAction listener)
        {
            if (eventManager == null) return;
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue (eventId, out thisEvent))
            {
                thisEvent.RemoveListener (listener);
            }
        }

        public static void SendEventMessage (EventID eventId)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue (eventId, out thisEvent))
            {
                thisEvent.Invoke ();
            }
        }
    }
}