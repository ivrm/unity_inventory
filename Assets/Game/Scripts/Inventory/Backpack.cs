using System.Collections.Generic;
using Game.Scripts.Event;
using Game.Scripts.Props;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Inventory
{
    public class Backpack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public List<Collectible> Items => items;
        
        [SerializeField] private GameObject inventoryObject;
        [SerializeField] private List<Collectible> items = new List<Collectible>();
        
        private Inventory inventory;

        private void Start()
        {
            inventory = inventoryObject.GetComponent<Inventory>();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            ShowInvertoryUI();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            var slot = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>();

            if (slot)
            {
                RemoveItem(slot.Item);
                slot.RemoveItem();
            }

            HideInvertoryUI();
        }

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<Collectible>();

            if (!item) return;
            
            AddItem(item);
        }

        private void RemoveItem(Collectible item)
        {
            if (!items.Contains(item)) return;
            
            items.Remove(item);
            UnMountItem(item);
            SendItemRemovedEvent(item);
        }

        private void AddItem(Collectible item)
        {
            items.Add(item);
            MountItem(item);
            SendItemAddedEvent(item);
        }

        private void MountItem(Collectible item)
        {
            item.MountToBackpack();
        }
        
        private void UnMountItem(Collectible item)
        {
            item.UnMountFromBackpack();
        }
        
        private void SendItemAddedEvent(Collectible item)
        {
            ItemEvent.onAdd.Invoke(item.Id, "add");
        }   
        
        private void SendItemRemovedEvent(Collectible item)
        {
            ItemEvent.onRemove.Invoke(item.Id, "remove");
        }

        private void ShowInvertoryUI()
        {
            inventory.GetComponentInParent<Canvas>().enabled = true;
        }

        private void HideInvertoryUI()
        {
            inventory.GetComponentInParent<Canvas>().enabled = false;
        }
    }
}
