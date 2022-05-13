using Interface;
using UnityEngine;

namespace Components
{
    public class ProjectileArrow : MonoBehaviour
    {
        private IDamageable target;
        private float dmg;
        private float finalDmgRatio;
        private Transform t;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private bool isRotate;
        [SerializeField] private float rotateSpeed;
        public void Setup(IDamageable targetCm, float dmgCm, float dmgRatio)
        {
            t = GetComponent<Transform>();
            target = targetCm;
            finalDmgRatio = dmgRatio;
            dmg = dmgCm;

           
        }

        private void Update()
        {
            if (target != null)
            {
            
                if (isRotate)
                {
                        t.Rotate(new Vector3(0,0,rotateSpeed * Time.deltaTime));
                }
                
                Vector3 moveDir = (target.GetPosition() - t.position).normalized;
          
                float moveSpeed = 55;
                t.position += moveDir * (moveSpeed * Time.deltaTime);

                if (!isRotate)
                {
                    float angle = GetAngle(moveDir);
                    t.eulerAngles = new Vector3(0, 0, angle);
                }
             
                float destroyselfDistance = 1f;

                if (Vector3.Distance(t.position, target.GetPosition()) < 10)
                {
                    spriteRenderer.sortingOrder = 150;
                }
            
            
                if (Vector3.Distance(t.position, target.GetPosition()) < destroyselfDistance)
                {
                    if (target.ApplyDamage(dmg, finalDmgRatio))
                    {
                       
                    }

                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private float GetAngle(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            return n;
        }
    }
}