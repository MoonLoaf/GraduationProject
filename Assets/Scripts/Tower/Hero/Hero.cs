using UnityEngine;

namespace Tower.Hero
{
    public class Hero : TowerBase
    {
        private HeroState State { get; set; }

        private void Start()
        {
            State = HeroState.Hybrid;
        }

        protected override void FixedUpdate()
        {
            switch (State)
            {
                case HeroState.Attacking:
                    base.FixedUpdate();
                    break;
                case HeroState.Fishing:
                    GoFish();
                    break;
                case HeroState.Hybrid:
                    if (GetNewTarget())
                    {
                        base.FixedUpdate();
                        break;
                    }
                    GoFish();
                    break;
            }
        }

        private void GoFish()
        {
            //TODO: implement economy
        }

        protected override void Attack()
        {
            base.Attack();
            Debug.Log("Hero Attack");
            //TODO: Hero attack cool stuff
        }

        public void SetState(HeroState newState)
        {
            State = newState;
        }
    }
}
