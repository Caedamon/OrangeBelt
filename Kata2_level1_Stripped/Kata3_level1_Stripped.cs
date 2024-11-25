// Kata 3 - Introduction to Templates (Generics)
// Level 1 (Striped Yellow Belt)
// Goal: Introduce generics (templates), without applying SOLID principles.
//
//     Requirements
//
//     Create a Generic Ability Container:
//
// Design a simple generic class called AbilityContainer<T> that can store abilities of different types.
//     Implement methods to add, remove, and retrieve abilities from the container.
//     Use generics to ensure type safety while allowing flexibility in the types of abilities stored.
//     Ability Implementation:
//
// Define a base IAbility interface with properties like Name and Effect (both can be strings).
//     Create at least two different ability classes inheriting from IAbility, such as AttackAbility and HealAbility.
//     Instantiate abilities and add them to the AbilityContainer<T>.
// Display Abilities:
//
// Implement a method to display all abilities stored in the container, showing their names and effects.
//     Expected Skill Outcome
//
//     Understand the basics of generics and how to define a simple generic class.
// Learn how generics provide type safety while allowing flexibility in the types of objects stored.
//     Gain experience in adding, removing, and retrieving items from a generic container.


namespace Kata3_1;
{
    public interface IAbility
    {
        string Name { get; set; }
        string Effect { get; set; }
    }

    public class AttackAbility : IAbility
    {
        public string Name { get; set; }
        public string Effect { get; set; }

        public AttackAbility(string name, string effect)
        {
            Name = name;
            Effect = effect;
        }
    }

    public class HealAbility : IAbility
    {
        public string Name { get; set; }
        public string Effect { get; set; }

        public HealAbility(string name, string effect)
        {
            Name = name;
            Effect = effect;
        }
    }

    public class AbilityContainer<T> where T : IAbility
    {
        private List<T> abilities = new List<T>();

        public void AddAbility(T ability)
        {
            abilities.Add(ability);
            Console.WriteLine($"Added ability: {ability.Name}");
        }

        public void RemoveAbility(T ability)
        {
            if (abilities.RemoveAbility(T ability))
            {
                Console.WriteLine($"Removed ability: {ability.Name}");
            }
            else
            {
                Console.WriteLine($"Ability not found: {ability.Name}");
            }
        }

        public List<T> GetAbilities()
        {
            return abilities;
        }

        public void DisplayAbilities()
        {
            Console.WriteLine("Abilities in the container:");
            foreach (var ability in abilities)
            {
                Console.WriteLine($"- {ability.Name}: {ability.Effect}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var abilityContainer = new AbilityContainer<>();
            
            var attackAbility = new AttackAbility("Fireball", "Deals 50 fire damage.");
            var healAbility = new HealAbility("Healing Light", "Restores 30 health.");
            
            abilityContainer.AddAbility(attackAbility);
            abilityContainer.AddAbility(healAbility);
            abilityContainer.DisplayAbilities();
            abilityContainer.RemoveAbility(healAbility);
            abilityContainer.DisplayAbilities();
        }
    }
}