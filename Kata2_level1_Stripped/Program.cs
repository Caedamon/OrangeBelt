// Level 1 (Striped Yellow Belt)
// Goal: Introduce basic C# features, specifically delegates and events, without applying SOLID principles.
//
//     Requirements
//
//     Character Creation:
//
// Create a Character class with properties for Name and Health.
//     Implement a simple delegate called CharacterAction that represents a basic character action, like Attack.
//     Define an event in the Character class called HealthChanged that triggers when the character's health changes.
//     Implement Basic Actions:
//
// Create a method Attack that reduces the health of a target character.
//     The Attack method should use the CharacterAction delegate to perform the action.
//     When a character is attacked, the HealthChanged event should be triggered.
//     Event Subscription:
//
// In your Program or Game class, create two characters and subscribe to their HealthChanged events.
//     When the event is triggered, display a message indicating the character's new health.
//     Expected Skill Outcome
//
//     Learn to define and use delegates to represent methods.
//     Understand how to declare and trigger events within a class.
// Practice subscribing to events and handling them to respond to changes in object state.

namespace Kata2_1
{
    public delegate void CharacterAction(Character target);

    public class Character
    {
        public string Name { get; set; }
        private int health;
        public event Action<Character> HealthChanged;
        public int Health
        {
            get => health;
            set
            {
                if (health != value)
                {
                    health = value;
                    OnHealthChanged();
                }
            }
        }

        public Character(string name, int initialHealth)
        {
            Name = name;
            Health = initialHealth;
        }
        private void OnHealthChanged()
        {
            HealthChanged?.Invoke(this);
        }

        public void Attack(Character target)
        {
            CharacterAction attackAction = targetCharacter =>
            {
                Console.WriteLine($"{Name} attacks {targetCharacter.Name}!");
                targetCharacter.Health -= 10;
            };
            attackAction(target);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var warrior = new Character("Warrior", 100);
            var healer = new Character("Healer", 80);
            
            warrior.HealthChanged += character =>
            {
                Console.WriteLine($"Health update: {character.Name}'s health is now {character.Health}.");
            };
            
            healer.HealthChanged += character =>
            {
                Console.WriteLine($"Health update: {character.Name}'s health is now {character.Health}.");
            };
            
            Console.WriteLine("Battle begins...\n");
            warrior.Attack(healer);
            healer.Attack(warrior);

            Console.WriteLine("\nBattle ends.");
        }
    }
}