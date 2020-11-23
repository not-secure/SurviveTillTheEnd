using Entity.Behavior;
using UnityEngine;

namespace Entity {
    public abstract class EntityLiving: EntityBase {
        public readonly PathPlanner PathPlanner;
        public Animation Animation;

        public EntityLiving(): base() {
            PathPlanner = new PathPlanner(this);
        }

        public override void OnInit() {
            base.OnInit();
            this.Animation = this.Entity.GetComponent<Animation>();
        }
        
        public override void OnTick() {
            base.OnTick();
            PathPlanner.Update();
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