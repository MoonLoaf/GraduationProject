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

        protected override void Update()
        {
            switch (State)
            {
                case HeroState.Attacking:
                    base.Update();
                    break;
                case HeroState.Fishing:
                    GoFish();
                    break;
                case HeroState.Hybrid:
                    if (_enemiesInRange.Count > 0)
                    {
                        //Make stats worse
                        base.Update();
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

        protected override void Attack(Vector3 targetPos)
        {
            base.Attack(targetPos);
            Debug.Log("Hero Attack");
            //TODO: Hero attack cool stuff
        }

        public void SetState(HeroState newState)
        {
            State = newState;
        }
    }
}
