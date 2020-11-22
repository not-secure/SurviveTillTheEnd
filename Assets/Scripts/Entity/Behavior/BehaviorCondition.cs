namespace Entity.Behavior {
    public abstract class BehaviorCondition: Behavior {
        public override void Execute(EntityBase entity) {
            var condition = GetCondition(entity);

            if (condition) {
                OnTrue(entity);
                return;
            }

            OnFalse(entity);
        }

        protected abstract bool GetCondition(EntityBase entity);
        protected abstract void OnTrue(EntityBase entity);
        protected abstract void OnFalse(EntityBase entity);
    }
}