using System;
using UnityEngine.Events;

namespace Game.Scripts.Event
{
    [Serializable]
    public class ItemEvent : UnityEvent<int, string>
    {
        public static ItemEvent onAdd = new ItemEvent();
        public static ItemEvent onRemove = new ItemEvent();
    }
}
