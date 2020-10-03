using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Props
{
    [Serializable]
    public class Collectible : MonoBehaviour
    {
        public int Id => id;
        public string ItemName => itemName;
        public int InventoryPosition => inventoryPosition;
        public float Weight => weight;
        public Sprite Icon => icon;

        public enum Type
        {
            Type1 = 1,
            Type2 = 2,
            Type3 = 3
        }

        [SerializeField] private int id;
        [SerializeField] private string itemName;
        [SerializeField] private float weight;
        [SerializeField] private Sprite icon;
        [SerializeField] private float mountVelocity = 0.001f;
        [SerializeField] private float unmountVelocity = 0.002f;
        [SerializeField] private float locoMountDistance = 0.2f;
        [SerializeField] private float locoUnMountDistance = 1f;
        [SerializeField] private Transform backpackPosition;
        [SerializeField] private int inventoryPosition;
        
        private Draggable draggable;
        private Rigidbody rb;
        private Collider col;

        private void Start()
        {
            draggable = GetComponent<Draggable>();
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            rb.mass = weight;
        }

        public bool MountToBackpack()
        {
            StartCoroutine(MountToBackpackCoroutine());
            return true;
        }
        
        public bool UnMountFromBackpack()
        {
            StartCoroutine(UnMountFromBackpackCoroutine());

            return true;
        }

        private IEnumerator MountToBackpackCoroutine()
        {
            col.enabled = false;
            draggable.enabled = false;
            rb.isKinematic = true;
            var distance = CalculateDistance(transform.position, backpackPosition.position);
            
            while (distance > mountVelocity)
            {
                var endPosition = backpackPosition.position;

                transform.position = Vector3.Lerp(transform.position, endPosition, mountVelocity);
                distance = CalculateDistance(transform.position, endPosition);

                if (!(distance <= locoMountDistance)) continue;
                
                yield return new WaitForFixedUpdate();
                yield return new WaitForSeconds(0.25f - mountVelocity);
            }
            
            transform.position = backpackPosition.position;
            
            yield return null;
        }

        private IEnumerator UnMountFromBackpackCoroutine()
        {
            col.enabled = false;
            var distance = 0f;
            var startPosition = transform.position;
            var endPosition = startPosition + Vector3.back;
            
            while (distance <= locoUnMountDistance)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, unmountVelocity);
                distance = CalculateDistance(transform.position, endPosition);

                yield return new WaitForFixedUpdate();
                yield return new WaitForSeconds(0.25f - unmountVelocity);
            }
            
            transform.position = endPosition;
            rb.isKinematic = false;
            col.enabled = true;
            draggable.enabled = true;

            yield return null;
        }

        private float CalculateDistance(Vector3 start, Vector3 end)
        {
            return Vector3.Distance(start, end);
        }
    }
}
