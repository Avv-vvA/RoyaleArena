using Spine.Unity;
using UnityEngine;
// ReSharper disable All

namespace Components
{
   public class Anim
   {
      private readonly SkeletonAnimation skeletonAnimation;
      private readonly Stats stats; // Возможно в будущем поменять на Stats для упрощения кода
      private readonly Animations animations;
      private int rnd;
      private readonly float mainAnimSpeed = 1;
      private AnimationReferenceAsset currState;

      
      
      public Anim(Stats stats, Animations animations, SkeletonAnimation skeletonAnimation)
      {
         this.stats = stats;
         this.animations = animations;
         this.skeletonAnimation = skeletonAnimation;
         
      }
   
        
      public void PlayIdle()
      {
         if (currState == animations.idle || currState == animations.death) return;
         currState = animations.idle;
         skeletonAnimation.AnimationState.SetAnimation(0, animations.idle, true).TimeScale = mainAnimSpeed;
      
      }

      public void PlayMove()
      {
         if (currState == animations.death) return;
         currState = animations.move;
         skeletonAnimation.AnimationState.SetAnimation(0, animations.move, true).TimeScale = mainAnimSpeed;
      }

      public void PlayAttack()
      {
         var rnd = Random.Range(0, 2);
         if (rnd == 1)
         {
            skeletonAnimation.AnimationState.SetAnimation(0, animations.attack1, false).TimeScale = stats.FinalAttackAnimationSpeed;
       
         }
         else
         {
            skeletonAnimation.AnimationState.SetAnimation(0, animations.attack2, false).TimeScale = stats.FinalAttackAnimationSpeed;
         }
      
      }

      public void PlaySkill()
      {
         if (currState == animations.death) return;
         currState = animations.skill;
         skeletonAnimation.AnimationState.SetAnimation(0, animations.skill, false).TimeScale = stats.FinalAttackAnimationSpeed;
      }

      public void PlayDeath()
      {
         currState = animations.death;
         skeletonAnimation.state.ClearTracks();
         skeletonAnimation.AnimationState.SetAnimation(0, animations.death, false).TimeScale = mainAnimSpeed + 0.3f;
      }

  
      public void PlayStun()
      {
         if (currState == animations.stun || currState == animations.death) return;
         currState = animations.stun;
         skeletonAnimation.state.ClearTracks();
         skeletonAnimation.AnimationState.SetAnimation(0, animations.stun, true).TimeScale = mainAnimSpeed;
      }

      public void PlayFreeze()
      {
         currState = null;
         skeletonAnimation.state.ClearTracks();
      }

      public void PlayFly()
      {
         if (currState == animations.fly || currState == animations.death) return;
         currState = animations.fly;
         skeletonAnimation.state.ClearTracks();
         skeletonAnimation.AnimationState.SetAnimation(0, animations.fly, true).TimeScale = 1;
      }

   }
}

        