using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Event;
using Game.Scripts.Props;
using UnityEngine;

namespace Game.Scripts.Inventory
{
    [System.Serializable]
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();
        [SerializeField] private Backpack backpack;

        public void UpdateSlots()
        {
            var items = GetBackpackItems();
            FillSlots(items);
        }

        private void Start()
        {
            slots = GetComponentsInChildren<InventorySlot>().ToList();
            UpdateSlots();
        }

        private void OnEnable()
        {
            UpdateSlots();
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
            UpdateSlots();
        }

        private void FillSlots(List<Collectible> items)
        {
            foreach (var item in items)
            {
                foreach (var slot in slots)
                {
                    if (slot.Position != item.InventoryPosition) continue;
                    slot.TryAddItem(item);
                    break;
                }
            }
        }

        private List<Collectible> GetBackpackItems()
        {
            return backpack.Items;
        }
    }
}
