using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Units
{
    public class HeroesBar : MonoBehaviour
    {
        [SerializeField] private Image hpFill;
        [SerializeField] private GameObject stunObj;
        [SerializeField] private Image stunFill;
        [SerializeField] private List<GameObject> mergeLevels = new List<GameObject>();
        [SerializeField] private Text cardsLevel;
        [SerializeField] private Text cardsLevel2;


        public void SetMergeLevel(int level)
        {
            for (int i = 0; i < level; i++)
            {
                mergeLevels[i].SetActive(true);
            }
        }
        
        public void HpChanger(float hpCount, float maxHp)
        {
            hpFill.fillAmount = hpCount / maxHp;
        }

        public void SetHeroLevel(int lvl)
        {
            cardsLevel.text = "" + lvl;
            cardsLevel2.text = "" + lvl;
        }

        public void StunBarChanger(float currStun, float startStun)
        {
            if (!stunObj.activeSelf)
            {
                stunObj.SetActive(true);
            }

            stunFill.fillAmount = currStun / startStun;

            if (currStun <= 0)
            {
                stunObj.SetActive(false);
            }
        }

        public void HideBar()
        {
            gameObject.SetActive(false);
        }
    }
}