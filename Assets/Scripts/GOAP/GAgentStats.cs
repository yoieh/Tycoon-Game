using System;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP.Stats
{
    using Buffs;

    public class GAgentStats : MonoBehaviour
    {
        // properties of character
        [SerializeField] private int health;
        [SerializeField] private int happiness;
        [SerializeField] private int energy;
        [SerializeField] private int water;
        [SerializeField] private int food;

        public int Health { get => health; }
        public int Happiness { get => happiness; }
        public int Energy { get => energy; }
        public int Water { get => water; }
        public int Food { get => food; }

        public class OnPreformActionEventArgs : EventArgs
        {
            public int energy;
            public int water;
            public int food;
            public int happiness;
            public int health;
        }

        public static event EventHandler<OnPreformActionEventArgs> OnPerformAction;

        public class OnStatChangedEventArgs : EventArgs
        {
            public float value;
        }

        public event EventHandler<OnStatChangedEventArgs> OnHealthIncrease; // event to notify damage
        public event EventHandler<OnStatChangedEventArgs> OnHealthChanged; // event to notify health change
        public event EventHandler<OnStatChangedEventArgs> OnHappinessIncrease; // event to notify happiness increase
        public event EventHandler<OnStatChangedEventArgs> OnHappinessChanged; // event to notify happiness change
        public event EventHandler<OnStatChangedEventArgs> OnEnergyIncrease; // event to notify energy increase
        public event EventHandler<OnStatChangedEventArgs> OnEnergyChanged; // event to notify energy change
        public event EventHandler<OnStatChangedEventArgs> OnWaterIncrease; // event to notify water increase
        public event EventHandler<OnStatChangedEventArgs> OnWaterChanged; // event to notify water change
        public event EventHandler<OnStatChangedEventArgs> OnFoodIncrease; // event to notify food increase
        public event EventHandler<OnStatChangedEventArgs> OnFoodChanged; // event to notify food change


        public List<GBaseBuff> workerBuffs;

        public void TiggerAllStatsEvents()
        {
            if (OnHealthChanged != null) OnHealthChanged(this, new OnStatChangedEventArgs { value = this.health });
            if (OnHappinessChanged != null) OnHappinessChanged(this, new OnStatChangedEventArgs { value = this.happiness });
            if (OnEnergyChanged != null) OnEnergyChanged(this, new OnStatChangedEventArgs { value = this.energy });
            if (OnWaterChanged != null) OnWaterChanged(this, new OnStatChangedEventArgs { value = this.water });
            if (OnFoodChanged != null) OnFoodChanged(this, new OnStatChangedEventArgs { value = this.food });
        }

        public void HealthIncrease(int health)
        {
            if (this.health <= 0)
            {
                this.health = 0;
                return;
            }

            // modify healing according to each buff
            foreach (GBaseBuff buff in workerBuffs)
            {
                health = buff.ModifyHealing(health);
            }



            // heal health
            this.health += health;
            if (OnHealthIncrease != null) OnHealthIncrease(this, new OnStatChangedEventArgs { value = Health });
            if (OnHealthChanged != null) OnHealthChanged(this, new OnStatChangedEventArgs { value = Health });
        }


        public void HappinessIncrease(int happiness)
        {
            if (this.happiness <= 0)
            {
                this.happiness = 0;
                return;
            }

            // modify happiness according to each buff
            foreach (GBaseBuff buff in workerBuffs)
            {
                happiness = buff.ModifyHappiness(happiness);
            }

            // modify happiness
            this.happiness += happiness;
            if (OnHappinessIncrease != null) OnHappinessIncrease(this, new OnStatChangedEventArgs { value = this.happiness });
            if (OnHappinessChanged != null) OnHappinessChanged(this, new OnStatChangedEventArgs { value = this.happiness });
        }

        public void EnergyIncrease(int energy)
        {
            if (this.energy <= 0)
            {
                this.energy = 0;
                return;
            }

            // modify energy according to each buff
            foreach (GBaseBuff buff in workerBuffs)
            {
                energy = buff.ModifyEnergy(energy);
            }

            // modify energy
            this.energy += energy;
            if (OnEnergyIncrease != null) OnEnergyIncrease(this, new OnStatChangedEventArgs { value = this.energy });
            if (OnEnergyChanged != null) OnEnergyChanged(this, new OnStatChangedEventArgs { value = this.energy });
        }

        public void WaterIncrease(int water)
        {
            if (this.water <= 0)
            {
                this.water = 0;
                return;
            }

            // modify water according to each buff
            foreach (GBaseBuff buff in workerBuffs)
            {
                water = buff.ModifyWater(water);
            }

            // modify water
            this.water += water;
            if (OnWaterIncrease != null) OnWaterIncrease(this, new OnStatChangedEventArgs { value = this.water });
            if (OnWaterChanged != null) OnWaterChanged(this, new OnStatChangedEventArgs { value = this.water });
        }

        public void FoodIncrease(int food)
        {
            if (this.food <= 0)
            {
                this.food = 0;
                return;
            }

            // modify food according to each buff
            foreach (GBaseBuff buff in workerBuffs)
            {
                food = buff.ModifyFood(food);
            }

            // modify food
            this.food += food;
            if (OnFoodIncrease != null) OnFoodIncrease(this, new OnStatChangedEventArgs { value = this.food });
            if (OnFoodChanged != null) OnFoodChanged(this, new OnStatChangedEventArgs { value = this.food });
        }

        public void PerformAction(int energy = 0, int water = 0, int food = 0, int happiness = 0, int health = 0)
        {
            EnergyIncrease(energy);
            WaterIncrease(water);
            FoodIncrease(food);
            HappinessIncrease(happiness);
            HealthIncrease(health);

            if (OnPerformAction != null)
                OnPerformAction(this, new OnPreformActionEventArgs
                {
                    energy = energy,
                    water = water,
                    food = food,
                    happiness = happiness,
                    health = health
                });
        }

        public WorldStates ToWorldStates()
        {
            WorldStates ws = new WorldStates();
            ws.SetState(WorldStateTypes.Health, Health);
            ws.SetState(WorldStateTypes.Energy, Energy);
            ws.SetState(WorldStateTypes.Food, Food);
            ws.SetState(WorldStateTypes.Water, Water);
            ws.SetState(WorldStateTypes.Happiness, Happiness);
            return ws;
        }
    }
}
