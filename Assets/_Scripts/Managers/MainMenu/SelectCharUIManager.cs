using System.Collections.Generic;
using _Scripts.Systems;
using UnityEngine;

public class SelectCharUIManager : MonoBehaviour
{
    [SerializeField] private Transform ContentPlace;
    [SerializeField] private List<CharacterViewTemplate> selectedPlayersPosesList = new List<CharacterViewTemplate>();
    [SerializeField] private GameObject emptyObj;

    private ResourceSystem resourceSystem;

    private CharacterViewTemplate _currentChar;

    private List<CharacterViewTemplate> allHeroes = new List<CharacterViewTemplate>();
    public bool selectedOne;


    public void SetNewSelectChar(CharacterViewTemplate oldHero)
    {
        
        for (int j = 0; j < allHeroes.Count; j++)
        {
            if (allHeroes[j].HeroData.HeroType == oldHero.HeroData.HeroType)
            {
                allHeroes[j].isSelected = false;
            }
        }


        for (int i = 0; i < resourceSystem.PlayerSpawnCardsList.Count; i++)
        {
            if (resourceSystem.PlayerSpawnCardsList[i] == oldHero.HeroData.HeroType)
            {
                resourceSystem.PlayerSpawnCardsList[i] = _currentChar.HeroData.HeroType;
            }
        }

        _currentChar.isSelected = true;

        oldHero.SetNewChar(_currentChar.HeroData);
        SetPressedChar(oldHero);


        SaveSystem.Instance.SaveData(resourceSystem.PlayersData);
    }


    public void SetPressedChar(CharacterViewTemplate currentBtn)
    {
        if (selectedOne)
        {
            selectedOne = false;
            foreach (var t in allHeroes)
            {
                t.gameObject.SetActive(true);
            }

            emptyObj.SetActive(false);
            //return;
        }


        if (_currentChar == null)
        {
            _currentChar = currentBtn;
        }
        else
        {
            _currentChar.HideSelected();
            _currentChar = currentBtn;
        }
    }

    public void HideAllHeroes(CharacterViewTemplate cm)
    {
        selectedOne = true;
        foreach (var t in allHeroes)
        {
            if (cm != t)
            {
                t.gameObject.SetActive(false);
            }
        }

        emptyObj.SetActive(true);
    }

    private void Start()
    {
        resourceSystem = ResourceSystem.Instance;

        for (int i = 0; i < selectedPlayersPosesList.Count; i++) // спавним выбранных героев
        {
            selectedPlayersPosesList[i]
                .Initialize(resourceSystem.GetHero(resourceSystem.PlayerSpawnCardsList[i]), this, true);
        }


        for (int i = 0; i < resourceSystem.Heroes.Count; i++)
        {
            var obj = Instantiate(resourceSystem.assetsPf.CharacterViewTemplateUi, ContentPlace);
            obj.Initialize(resourceSystem.Heroes[i], this, false);
            allHeroes.Add(obj);
            obj.transform.name = resourceSystem.Heroes[i].About.unitName + "UI";

            for (int j = 0; j < selectedPlayersPosesList.Count; j++)
            {
                if (selectedPlayersPosesList[j].HeroData.HeroType == obj.HeroData.HeroType)
                {
                    obj.isSelected = true;
                }
            }
        }
    }
}