using _Scripts.Scriptables;
using UnityEngine;
using UnityEngine.EventSystems;


namespace _Scripts.Managers.Board
{
    public class HeroOnBoard : HeroOnBoardComponents, IBeginDragHandler, IEndDragHandler, IDragHandler,
        IDropHandler
    {
        
        public void OnBeginDrag(PointerEventData eventData) // первый клик по карте
        {
            newPlace = false;
            _image.raycastTarget = false;
            SetAnim(State.Fly);
            _boardManager.CanUpdateCheck(this);
            _t.SetParent(selected);
        }

        public void OnEndDrag(PointerEventData eventData) // закончил переброс
        {
            _image.raycastTarget = true;
            _t.position = _startPos;
             SetAnim(State.Idle);
             _boardManager.ColoredCardInDesc();
             _t.SetParent(heroesTrs);
             
             
            if (!newPlace) return;
            _startPos = _t.position;
            newPlace = false;
          
        }

        public void OnDrag(PointerEventData eventData) // Во время движения карты 
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPosition = _cam.ScreenToWorldPoint(mousePos);
            _rectTransform.position = worldPosition;
        }


        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && eventData.pointerEnter != null)
            {
                HeroOnBoard dragCard = eventData.pointerDrag.GetComponent<HeroOnBoard>(); // Карта которую переносил
                HeroOnBoard enterCard = eventData.pointerEnter.GetComponent<HeroOnBoard>();
                
                if (CanMerge(dragCard)) // Когда перебросил на одинаковые карты
                {
                    if (UnitManager.Instance.MergeType == MergeType.basic)
                    {
                        
                        if (dragCard.cardPosTrs.GetSiblingIndex() < 5)
                        {
                            _boardManager.willSpawnHeroes.Remove(dragCard);
                        }
                        
                        _boardManager.AddFreePlace(dragCard.cardPosTrs);
                        
                        
                        dragCard.DestroyCard();
                        enterCard.SetMergeLevel(mergeLevel +1);
                        _image.raycastTarget = true;
                        _t.SetParent(heroesTrs);
                        _boardManager.ColoredCardInDesc();
                        SetAnim(State.Idle);
                    }
                    else
                    {
                        _boardManager.ColoredCardInDesc();
                        var obj = _boardManager.CreateRandomHero();
                        obj.cardPosTrs = dragCard.cardPosTrs;
                        obj.SetPlace(cardPosTrs);
                        obj.SetMergeLevel(mergeLevel + 1);
                        
                        if (dragCard.cardPosTrs.GetSiblingIndex() < 5)
                        {
                            _boardManager.willSpawnHeroes.Remove(dragCard);
                        }

                        if (cardPosTrs.GetSiblingIndex() < 5)
                        {
                            _boardManager.willSpawnHeroes.Remove(this);
                        }
                        _boardManager.AddFreePlace(dragCard.cardPosTrs);
                        DestroyCard();
                        dragCard.DestroyCard();
                    }
                }
            }
        }

        
        
        
        
        
        public bool CanMerge(HeroOnBoard dragCard)
        {
            if (dragCard.HeroType == HeroType || HeroType == HeroType.Joker || dragCard.HeroType == HeroType.Joker)
            {
                if (mergeLevel == dragCard.mergeLevel && mergeLevel < 5 && dragCard.mergeLevel < 5)
                {
                    return true;
                }
            }

            return false;
        }
        

        public void SetMergeLevel(int lvl)
        {
            mergeLevel = lvl;

            for (int i = 0; i < lvl; i++)
            {
                stars[i].SetActive(true);
            }
        }

        public void ChangePlace(Transform pos)
        {
            
            if (cardPosTrs.GetSiblingIndex() < 5)
            {
                _boardManager.willSpawnHeroes.Remove(this);
            }
            
            _boardManager.AddFreePlace(cardPosTrs);
            
            
            _boardManager.RemoveFreePlace(pos);
            SetPlace(pos);
        }

        public void SetPlace(Transform newPos)
        {
            if (newPos.GetSiblingIndex() < 5)
            {
                _boardManager.willSpawnHeroes.Add(this);
            }
            cardPosTrs = newPos;
            _t.position = newPos.position;
            _startPos = _t.position;
            newPlace = true;
        }

        public void DestroyCard()
        {
            _boardManager.RemoveCardInDesc(this);
            Destroy(gameObject);
        }
    }
}