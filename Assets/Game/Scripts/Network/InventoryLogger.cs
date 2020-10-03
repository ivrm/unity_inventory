using System;
using System.Collections.Generic;
using Game.Scripts.Config;
using Game.Scripts.Event;
using UnityEngine;

namespace Game.Scripts.Network
{
    public class InventoryLogger : MonoBehaviour
    {
        private GameConfig config;
        private HttpClient client;
        
        void Start()
        {
            config = GetComponent<GameConfig>();
            client = GetComponent<HttpClient>();
        }

        private void OnEnable()
        {
            ItemEvent.onAdd.AddListener(ItemListener);
            ItemEvent.onRemove.AddListener(ItemListener);
        }

        private void OnDisable()
        {
            ItemEvent.onAdd.RemoveListener(ItemListener);
            ItemEvent.onRemove.RemoveListener(ItemListener);
        }

        private void ItemListener(int id, string action)
        {
            Send(id, action);
        }

        private void Send(int id, string action)
        {
            var data = new Dictionary<string, string>();
            data.Add(id.ToString(), action);
            
            client.SendPost(config.ApiInventoryStatusUrl, data);
        }
    }
}
