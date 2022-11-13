using System;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    using Worker.Buffs;

    public class GAgentStats : MonoBehaviour
    {
        // properties of character
        [SerializeField] private float health;
        [SerializeField] private float happiness;
        [SerializeField] private float energy;
        [SerializeField] private float water;
        [SerializeField] private float food;

        public float Health { get => health; }
        public float Happiness { get => happiness; }
        public float Energy { get => energy; }
        public float Water { get => water; }
        public float Food { get => food; }

        public class OnPreformActionEventArgs : EventArgs
        {
            public float energy;
            public float water;
            public float food;
            public float happiness;
            public float health;
        }

        public static event EventHandler<OnPreformActionEventArgs> OnPerformAction;

        public class OnStatChangedEventArgs : EventArgs
        {
            public float value;
        }

        public event EventHandler<OnStatChangedEventArgs> OnHealthIncrease; // event to notify damage
        public event EventHandler<OnStatChangedEventArgs> OnHealthDecrease; // event to notify healing
        public event EventHandler<OnStatChangedEventArgs> OnHealthChanged; // event to notify health change
        public event EventHandler<OnStatChangedEventArgs> OnHappinessIncrease; // event to notify happiness increase
        public event EventHandler<OnStatChangedEventArgs> OnHappinessDecrease; // event to notify happiness decrease
        public event EventHandler<OnStatChangedEventArgs> OnHappinessChanged; // event to notify happiness change
        public event EventHandler<OnStatChangedEventArgs> OnEnergyIncrease; // event to notify energy increase
        public event EventHandler<OnStatChangedEventArgs> OnEnergyDecrease; // event to notify energy decrease
        public event EventHandler<OnStatChangedEventArgs> OnEnergyChanged; // event to notify energy change
        public event EventHandler<OnStatChangedEventArgs> OnWaterIncrease; // event to notify water increase
        public event EventHandler<OnStatChangedEventArgs> OnWaterDecrease; // event to notify water decrease
        public event EventHandler<OnStatChangedEventArgs> OnWaterChanged; // event to notify water change
        public event EventHandler<OnStatChangedEventArgs> OnFoodIncrease; // event to notify food increase
        public event EventHandler<OnStatChangedEventArgs> OnFoodDecrease; // event to notify food decrease
        public event EventHandler<OnStatChangedEventArgs> OnFoodChanged; // event to notify food change


        public List<BaseBuff> workerBuffs;

        public void TiggerAllStatsEvents()
        {
            if (OnHealthChanged != null) OnHealthChanged(this, new OnStatChangedEventArgs { value = this.health });
            if (OnHappinessChanged != null) OnHappinessChanged(this, new OnStatChangedEventArgs { value = this.happiness });
            if (OnEnergyChanged != null) OnEnergyChanged(this, new OnStatChangedEventArgs { value = this.energy });
            if (OnWaterChanged != null) OnWaterChanged(this, new OnStatChangedEventArgs { value = this.water });
            if (OnFoodChanged != null) OnFoodChanged(this, new OnStatChangedEventArgs { value = this.food });
        }

        public void HealthIncrease(float health)
        {
            // modify healing according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                health = buff.ModifyHealing(health);
            }

            // heal health
            this.health += health;
            if (OnHealthIncrease != null) OnHealthIncrease(this, new OnStatChangedEventArgs { value = Health });
            if (OnHealthChanged != null) OnHealthChanged(this, new OnStatChangedEventArgs { value = Health });
        }

        // Deal damage, damage will be reduced by armour
        public void HealthDecrease(float health)
        {
            // modify damage according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                health = buff.ModifyDamage(health);
            }

            // damage health
            this.health -= health;
            if (OnHealthDecrease != null) OnHealthDecrease(this, new OnStatChangedEventArgs { value = Health });
            if (OnHealthChanged != null) OnHealthChanged(this, new OnStatChangedEventArgs { value = Health });
        }

        public void HappinessIncrease(float happiness)
        {
            // modify happiness according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                happiness = buff.ModifyHappiness(happiness);
            }

            // modify happiness
            this.happiness += happiness;
            if (OnHappinessIncrease != null) OnHappinessIncrease(this, new OnStatChangedEventArgs { value = this.happiness });
            if (OnHappinessChanged != null) OnHappinessChanged(this, new OnStatChangedEventArgs { value = this.happiness });
        }

        public void HappinessDecrease(float happiness)
        {
            // modify happiness according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                happiness = buff.ModifyHappiness(happiness);
            }

            // modify happiness
            this.happiness -= happiness;
            if (OnHappinessDecrease != null) OnHappinessDecrease(this, new OnStatChangedEventArgs { value = this.happiness });
            if (OnHappinessChanged != null) OnHappinessChanged(this, new OnStatChangedEventArgs { value = this.happiness });
        }

        public void EnergyIncrease(float energy)
        {
            // modify energy according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                energy = buff.ModifyEnergy(energy);
            }

            // modify energy
            this.energy += energy;
            if (OnEnergyIncrease != null) OnEnergyIncrease(this, new OnStatChangedEventArgs { value = this.energy });
            if (OnEnergyChanged != null) OnEnergyChanged(this, new OnStatChangedEventArgs { value = this.energy });
        }

        public void EnergyDecrease(float energy)
        {
            // modify energy according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                energy = buff.ModifyEnergy(energy);
            }

            // modify energy
            this.energy -= energy;
            if (OnEnergyDecrease != null) OnEnergyDecrease(this, new OnStatChangedEventArgs { value = this.energy });
            if (OnEnergyChanged != null) OnEnergyChanged(this, new OnStatChangedEventArgs { value = this.energy });
        }

        public void WaterIncrease(float water)
        {
            // modify water according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                water = buff.ModifyWater(water);
            }

            // modify water
            this.water += water;
            if (OnWaterIncrease != null) OnWaterIncrease(this, new OnStatChangedEventArgs { value = this.water });
            if (OnWaterChanged != null) OnWaterChanged(this, new OnStatChangedEventArgs { value = this.water });
        }

        public void WaterDecrease(float water)
        {
            // modify water according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                water = buff.ModifyWater(water);
            }

            // modify water
            this.water -= water;
            if (OnWaterDecrease != null) OnWaterDecrease(this, new OnStatChangedEventArgs { value = this.water });
            if (OnWaterChanged != null) OnWaterChanged(this, new OnStatChangedEventArgs { value = this.water });
        }

        public void FoodIncrease(float food)
        {
            // modify food according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                food = buff.ModifyFood(food);
            }

            // modify food
            this.food += food;
            if (OnFoodIncrease != null) OnFoodIncrease(this, new OnStatChangedEventArgs { value = this.food });
            if (OnFoodChanged != null) OnFoodChanged(this, new OnStatChangedEventArgs { value = this.food });
        }

        public void FoodDecrease(float food)
        {
            // modify food according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                food = buff.ModifyFood(food);
            }

            // modify food
            this.food -= food;
            if (OnFoodDecrease != null) OnFoodDecrease(this, new OnStatChangedEventArgs { value = this.food });
            if (OnFoodChanged != null) OnFoodChanged(this, new OnStatChangedEventArgs { value = this.food });
        }

        public void PerformAction(float energy = 0, float water = 0, float food = 0, float happiness = 0, float health = 0)
        {
            HealthDecrease(health);
            EnergyDecrease(energy);
            FoodDecrease(food);
            WaterDecrease(water);
            HappinessDecrease(happiness);

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
    }
}
