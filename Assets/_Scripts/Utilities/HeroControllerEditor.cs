
using _Scripts.Units.Hero;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(HeroController))]
public class HeroControllerEditor : Editor
{
    private string damage;
    private string attackRadius;
    private string maxHitPoints;
    private string currHitPoints;
    private string armor;
    private string baseAttackDelay;
    private string moveSpeed;
    private string skill1Desc;
    private string skill1Value;
    private string skill2Desc;
    private string skill2Value;
    
    
    public override void OnInspectorGUI()
    {

        
        HeroController tg = (HeroController)target;

        if (tg.HeroData == null)
        {
            
            DrawDefaultInspector ();
            return;
        }


        if (tg.Stats != null)
        {
            damage = "Damage = " + tg.Stats.FinalDamage;
            attackRadius = "Attack Radius = " + tg.HeroData.BaseStats.attackRadius;
            maxHitPoints = "MaxHp = " + tg.Stats.FinalMaxHitPoints;
            currHitPoints = "CurrHp = " + tg.Stats.currHitPoints;
            armor = "Armor = " + tg.Stats.FinalArmor;
            baseAttackDelay = "Base Attack Delay = " + tg.Stats.FinalAttackDelay;
            moveSpeed = "Move Speed = " + tg.HeroData.BaseStats.moveSpeed;
            skill1Desc = "Skill 1 Description " + tg.HeroData.BaseStats.skill1Description;
            skill1Value = "Skill 1 Value = " + tg.HeroData.BaseStats.skill1Value;
            skill2Desc = "Skill 2 Description " + tg.HeroData.BaseStats.skill2Description;
            skill2Value = "Skill 2 Value = " + tg.HeroData.BaseStats.skill2Value;
        }
     
        
        
        
        
        if (tg.HeroData.About.mainImage != null)
        {
            GUILayout.BeginVertical();
            GUILayout.Label(tg.HeroData.About.mainImage.texture, GUILayout.Height(100));
            GUILayout.Label("Stats");
            GUILayout.Label(damage);
            GUILayout.Label(attackRadius);
            GUILayout.Label(maxHitPoints);
            GUILayout.Label(currHitPoints);
            GUILayout.Label(armor);
            GUILayout.Label(baseAttackDelay);
            GUILayout.Label(moveSpeed);
            GUILayout.Label(skill1Desc);
            GUILayout.Label(skill1Value);
            GUILayout.Label(skill2Desc);
            GUILayout.Label(skill2Value);
            GUILayout.EndVertical();
        }
         
        // Show default inspector property editor
        DrawDefaultInspector ();
    }
}

#endif
