using _Scripts.Managers.Board;
using UnityEngine;
using UnityEngine.EventSystems;


    public class SlotInBoard : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
           eventData.pointerDrag.GetComponent<HeroOnBoard>().ChangePlace(gameObject.transform);
           
        }
        
    }
