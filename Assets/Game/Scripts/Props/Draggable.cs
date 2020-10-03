using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Props
{
    public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private float minGroundDistance = 0.5f;
        [SerializeField] private float zMovingVelocity = 5f;
        private const string BackpackTag = "Backpack";
        private Camera cam;
        private Rigidbody rb;
        private GameObject backpack;
        private Vector3 offset;
        private float zPosition;
        private float zDelta;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            cam = Camera.main;
            
            if (cam == null)
            {
                throw new MissingComponentException("Camera main not found");
            }
            
            backpack = GameObject.FindGameObjectWithTag(BackpackTag);
            var position = transform.position;
            var delta = position - backpack.transform.position;
            
            zPosition = cam.WorldToScreenPoint(position).z;
            zDelta = cam.WorldToScreenPoint(delta).z;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var position = new Vector3(eventData.position.x, eventData.position.y, zPosition);
            var newPosition = cam.ScreenToWorldPoint(position + new Vector3(offset.x, offset.y));

            if (newPosition.y < minGroundDistance)
            {
                if (newPosition.y < 0)
                {
                    newPosition.z += newPosition.y;
                }

                newPosition.y = minGroundDistance;
            }

            transform.position = newPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (rb)
            {
                rb.isKinematic = true;
            }

            offset = CalculateOffset(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (rb)
            {
                rb.isKinematic = false;
            }
            
            zPosition = cam.WorldToScreenPoint(transform.position).z;
        }

        private Vector3 CalculateOffset(PointerEventData eventData)
        {
            return cam.WorldToScreenPoint(transform.position) - new Vector3(eventData.position.x, eventData.position.y) + CalculateCameraRotationOffset();
        }

        private Vector3 CalculateCameraRotationOffset()
        {
            return new Vector3(0,0, zDelta * (zMovingVelocity + Mathf.Cos(cam.transform.rotation.x)));
        }
    }
}
