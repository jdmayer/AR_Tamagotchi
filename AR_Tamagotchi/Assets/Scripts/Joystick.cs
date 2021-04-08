using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Monobehaviours
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public RectTransform Pad;
        public Transform Character;
        public float Speed;

        private Vector3 _move;

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
            transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)Pad.position, Pad.rect.width * 0.3f);

            _move = new Vector3((transform.localPosition.x * -1), 0, (transform.localPosition.y * -1)).normalized;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine(Constants.CharacterMove);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            _move = Vector3.zero;

            StopCoroutine(Constants.CharacterMove);
        }

        IEnumerator CharacterMove()
        {
            while (true)
            {
                Character.Translate(_move * Speed * Time.deltaTime, Space.World);
                
                if (_move != Vector3.zero)
                {
                    Character.rotation = Quaternion.Slerp(Character.rotation, Quaternion.LookRotation(_move), 5 * Time.deltaTime);
                }

                yield return null;
            }
        }
    }
}
