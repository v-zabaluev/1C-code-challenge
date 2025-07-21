using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Codebase.Gameplay
{
    public class ShapeDragHandler : MonoBehaviour
    {
        private Vector3 _originalPosition;
        private bool _dragging;
        private Camera _camera;
        private int _activeTouchId = -1;

        public event Action OnBeginDragAction;
        public event Action OnMissedDragAction;
        public Action<SorterSlot> OnEndDragAction;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
        HandleTouchInput();
#endif
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mouseWorld2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

                Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorld2D);

                foreach (var hit in hits)
                {
                    if (hit == GetComponent<Collider2D>())
                    {
                        _originalPosition = transform.position;
                        _dragging = true;
                        OnBeginDragAction?.Invoke();

                        break;
                    }
                }
            }

            if (_dragging && Input.GetMouseButton(0))
            {
                Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, _originalPosition.z);
            }

            if (_dragging && Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount == 0)
                return;

            foreach (Touch touch in Input.touches)
            {
                Vector3 touchWorldPos = _camera.ScreenToWorldPoint(touch.position);
                Vector2 touchWorld2D = new Vector2(touchWorldPos.x, touchWorldPos.y);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (_activeTouchId == -1)
                        {
                            Collider2D[] hits = Physics2D.OverlapPointAll(touchWorld2D);

                            foreach (var hit in hits)
                            {
                                if (hit == GetComponent<Collider2D>())
                                {
                                    _originalPosition = transform.position;
                                    _dragging = true;
                                    _activeTouchId = touch.fingerId;
                                    OnBeginDragAction?.Invoke();

                                    break;
                                }
                            }
                        }

                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if (_dragging && touch.fingerId == _activeTouchId)
                        {
                            transform.position = new Vector3(touchWorldPos.x, touchWorldPos.y, _originalPosition.z);
                        }

                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (_dragging && touch.fingerId == _activeTouchId)
                        {
                            EndDrag();
                            _activeTouchId = -1;
                        }

                        break;
                }
            }
        }

        private void EndDrag()
        {
            _dragging = false;

            Collider2D hit = Physics2D.OverlapPoint(transform.position);

            if (hit && hit.TryGetComponent(out SorterSlot slot))
            {
                OnEndDragAction?.Invoke(slot);
            }
            else
            {
                transform.position = _originalPosition;
                OnMissedDragAction?.Invoke();
            }
        }
    }
}