using Entity.Behavior;
using UnityEngine;

namespace Entity {
    public abstract class EntityLiving: EntityBase {
        public PathPlanner pathPlanner;
        public Animation animation;

        public EntityLiving(): base() {
            pathPlanner = new PathPlanner(this);
        }

        public override void OnInit() {
            base.OnInit();
            this.animation = this.entity.GetComponent<Animation>();
        }
        
        public override void OnTick() {
            base.OnTick();
            pathPlanner.Update();
        }
        
        public override bool MoveTowards(Vector2Int target) {
            var result = base.MoveTowards(target);
            if (!result) {
                // animation.Play("AnimationMove");
            }

            return result;
        }
    }
}