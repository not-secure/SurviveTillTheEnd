namespace Entity.Behavior {
    public abstract class BehaviorCondition: Behavior {
        public override void Execute(EntityBase entity) {
            var condition = this.GetCondition(entity);

            if (condition) {
                this.OnTrue(entity);
                return;
            }

            this.OnFalse(entity);
        }

        protected abstract bool GetCondition(EntityBase entity);
        protected abstract void OnTrue(EntityBase entity);
        protected abstract void OnFalse(EntityBase entity);
    }
}