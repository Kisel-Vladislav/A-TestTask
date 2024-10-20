using CodeBase.GrabLogic;
using CodeBase.Infrastructure;
using CodeBase.Service;
using UnityEngine;
namespace CodeBase.Player
{
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] Transform GrabItemPlacement;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float radius;

        private GrabItem _grabItem;
        private IInputService _inputService;
        public bool HasGrabItem => _grabItem != null;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }
        private void Update()
        {
            if (_inputService.IsGrabButtonUp )
                Grab();
            else if (_inputService.IsDropButtonUp)
                Drop();
        }

        private void Drop()
        {
            if (!HasGrabItem)
                return;

            if (TryPlaceItemInContainer())
                ReleaseGrabbedItem();
        }
        private bool TryPlaceItemInContainer()
        {
            Vector3 spherePosition = GetSpherePosition();
            var colliders = Physics.OverlapSphere(spherePosition, radius);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<ItemContainer>(out var container))
                {
                    if (container.TryAdd(_grabItem))
                    {
                        return true; 
                    }
                }
            }

            return false; 
        }

        private void Grab()
        {
            if (HasGrabItem)
                return;

            if (TryGrabItemFromContainer() || TryGrabItemFromWorld())
                return;
        }
        private bool TryGrabItemFromContainer()
        {
            Vector3 spherePosition = GetSpherePosition();
            var colliders = Physics.OverlapSphere(spherePosition, radius);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<ItemContainer>(out var container) && !container.IsEmpty)
                {
                    var containerItem = container.Get();
                    GrabbedItem(containerItem);
                    return true;
                }
            }

            return false;
        }
        private bool TryGrabItemFromWorld()
        {
            Vector3 spherePosition = GetSpherePosition();
            var colliders = Physics.OverlapSphere(spherePosition, radius);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<GrabItem>(out var grabItem))
                {
                    GrabbedItem(grabItem);
                    return true;
                }
            }

            return false;
        }
        private void GrabbedItem(GrabItem grabItem)
        {
            grabItem.transform.SetParent(GrabItemPlacement);
            grabItem.gameObject.transform.localPosition = Vector3.zero;

            _grabItem = grabItem;
        }

        private Vector3 GetSpherePosition()
        {
            return transform.position + transform.forward * radius + offset;
        }
        private void ReleaseGrabbedItem()
        {
            if (_grabItem != null)
            {
                _grabItem.transform.SetParent(null);
                _grabItem = null;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(GetSpherePosition(), radius);
        }
    }

}
