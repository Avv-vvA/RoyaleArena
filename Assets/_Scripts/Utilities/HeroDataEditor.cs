
using _Scripts.Scriptables;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace _Scripts.Utilities
{
   [CustomEditor(typeof(HeroData))]
   public class HeroDataEditor : Editor
   {
      
      public override void OnInspectorGUI()
      {
         HeroData tg = (HeroData)target;

         if (tg.About.mainImage != null)
         {
            GUILayout.BeginVertical();
            GUILayout.Label(tg.About.mainImage.texture, GUILayout.Height(100));
            GUILayout.EndVertical();
         }
         
         // Show default inspector property editor
         DrawDefaultInspector ();
      }
   }
}


#endif


