using Entity.Behavior;

namespace Entity {
    public abstract class EntityLiving: EntityBase {
        public PathPlanner pathPlanner;

        public EntityLiving(): base() {
            pathPlanner = new PathPlanner(this);
        }
        
        public override void OnTick() {
            pathPlanner.Update();
        }
    }
}