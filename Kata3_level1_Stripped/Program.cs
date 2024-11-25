namespace Kata3_1
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

        public void AddAbility(T ability) //added this to make sure null values wouldnt be called
        {
            if (ability == null)
            {
                Console.WriteLine("Cannot add a null ability.");
                return;
            }

            abilities.Add(ability);
            Console.WriteLine($"Added ability: {ability.Name}");
        }

        public void RemoveAbility(T ability)
        {
            if (abilities.Remove(ability))
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

        public void DisplayAbilities() //changed this, previous would call on an empty container and display nothing,
                                       //now itl show a message if the list is empty
        {
            if (abilities.Count == 0)
            {
                Console.WriteLine("No abilities in the container.");
                return;
            }

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
            var abilityContainer = new AbilityContainer<IAbility>();
            
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