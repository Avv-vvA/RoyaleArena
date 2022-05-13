using System.Collections.Generic;
using Components;
using Interface;
using UnityEngine;

namespace _Scripts.Managers
{
    public class Battlefield : Singleton<Battlefield>
    {

        public List<IDamageable> playerUnits = new List<IDamageable>();
        public List<IDamageable> enemyUnits = new List<IDamageable>();
        
        public IDamageable GetClosestTarget(Vector3 position, Category category)
        {
            IDamageable closest = null;

            if (category == Category.Player)
            {
                foreach (IDamageable card in enemyUnits)
                {
                    if (card.IsDeath()) continue;
                    if (closest == null)
                    {
                        closest = card;
                    }

                    if (Vector3.Distance(position, card.GetPosition()) <
                        Vector3.Distance(position, closest.GetPosition()))
                    {
                        closest = card;
                    }
                }
            }

            else
            {
                foreach (IDamageable card in playerUnits)
                {
                    if (card.IsDeath()) continue;
                    if (closest == null)
                    {
                        closest = card;
                    }

                    if (Vector3.Distance(position, card.GetPosition()) <
                        Vector3.Distance(position, closest.GetPosition()))
                    {
                        closest = card;
                    }
                }
            }

            return closest;
        }
        
        public IDamageable GetRandomTarget(Category category)
        {
            IDamageable randomTarget = null;

            List<IDamageable> liveTarget = new List<IDamageable>();

            if (category == Category.Player)
            {
                foreach (IDamageable card in enemyUnits)
                {
                    if (card.IsDeath()) continue;

                    liveTarget.Add(card);
                }

                var rnd = Random.Range(0, liveTarget.Count);

                randomTarget = liveTarget[rnd];
            }

            else
            {
                foreach (IDamageable card in playerUnits)
                {
                    if (card.IsDeath()) continue;

                    liveTarget.Add(card);
                }

                var rnd = Random.Range(0, liveTarget.Count);

                randomTarget = liveTarget[rnd];
            }

            return randomTarget;
        }
        
        public bool HaveTarget(Category category)
        {
            if (category == Category.Player)
            {
                if (enemyUnits.Count > 0) return true;
            }
            else
            {
                if (playerUnits.Count > 0) return true;
            }

            return false;
        }

        
    }
}
