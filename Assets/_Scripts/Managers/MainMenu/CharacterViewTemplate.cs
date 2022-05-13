using System;
using _Scripts.Scriptables;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterViewTemplate : MonoBehaviour
{
    private HeroData heroData;
    [SerializeField] private Image mainImage;
    [SerializeField] private TextMeshProUGUI mainName;
    [SerializeField] private TextMeshProUGUI currLevel;
    [SerializeField] private TextMeshProUGUI fragmentsCount;
    [SerializeField] private GameObject simpleShow;
    private HeroesPf heroesPf;
    private GameObject currentWdn;
    private SelectCharUIManager selectCharUiManager;
    private bool isAnimated;
    private Animations animations;
    private GameObject heroImage;


    public bool isSelected;
    public HeroData HeroData => heroData;


    private void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(ShowSelected);
    }

    public void Initialize(HeroData heroData, SelectCharUIManager selectCharUiManager, bool isAnimated)
    {
        this.isAnimated = isAnimated;
        this.heroData = heroData;
        this.selectCharUiManager = selectCharUiManager;
        mainName.text = "" + this.heroData.About.unitName;
        currLevel.text = "LvL " + this.heroData.cardLevel;

        if (this.isAnimated)
        {
            SpawnAnimated();
        }
        else
        {
            mainImage.sprite = this.heroData.About.mainImage;
        }
    }


    public void SetNewChar(HeroData newHero)
    {
        heroData = newHero;
        Destroy(heroImage);
        heroImage = null;
        mainName.text = "" + heroData.About.unitName;
        currLevel.text = "LvL " + heroData.cardLevel;
        SpawnAnimated();
        HideSelected();
    }


    private void ShowSelected()
    {
        if (selectCharUiManager.selectedOne)
        {
            selectCharUiManager.SetNewSelectChar(this);
            return;
        }
        
        selectCharUiManager.SetPressedChar(this);
        simpleShow.SetActive(false);

        if (isSelected)
        {
            _selected.selectedCantSet.SetActive(true);
            currentWdn = _selected.selectedCantSet;
        }
        else
        {
            _selected.selectedCanSet.SetActive(true);
            currentWdn = _selected.selectedCanSet;
        }
        
        if (isAnimated)
        {
            heroesPf.SkeletonGraphic.AnimationState.SetAnimation(0, animations.move, true);
        }
    }

    public void HideSelected()
    {
        if (currentWdn != null)
        {
            currentWdn.SetActive(false);
            currentWdn = null;
        }

        simpleShow.SetActive(true);
        if (isAnimated)
        {
            heroesPf.SkeletonGraphic.AnimationState.SetAnimation(0, animations.idle, true);
        }
    }

    public void SetSelectHero() // Внешнее назначение
    {
        selectCharUiManager.HideAllHeroes(this);
    }

    private void SpawnAnimated()
    {
        mainImage.gameObject.SetActive(false);
        heroImage = Instantiate(heroData.UnitsPf.uiPf, mainImage.transform.parent);
        heroImage.transform.localScale = new Vector3(30, 30, 30);
        heroesPf = heroImage.GetComponent<HeroesPf>();
        animations = heroesPf.HeroData.Animations;
    }

    #region Test

    [SerializeField] private Update _update;

    [Serializable]
    private class Update
    {
        public GameObject updateWdn;
        public TextMeshProUGUI goldPriceText;
    }


    [SerializeField] private Selected _selected;

    [Serializable]
    private class Selected
    {
        public GameObject selectedCanSet;
        public GameObject selectedCantSet;
    }

    #endregion
}