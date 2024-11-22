// Kata 1: Lambdas and Actions in Practice
//     Level 1 (Striped Yellow Belt)
// Goal: Focus on introducing basic C# features, specifically Actions and lambdas, without applying SOLID principles.
//
//     Requirements
//
//     Character Creation:
//
// Create two characters, Warrior and Healer, without using inheritance.
//     Each character should have a PrimaryAction property, an Action that holds a lambda representing a role-specific ability (e.g., attack for Warrior, heal for Healer).
// Prioritize Actions Based on Health:
//
// Characters with health below 50 should attack first.
//     The healer character should prioritize healing the character with the lowest health.
//     Invoke Actions Dynamically:
//
// Use each lambda to dynamically call specific actions based on character role and health status.
//     Expected Skill Outcome
//
//     Learn to use Actions and lambdas to encapsulate unique abilities, simulating polymorphism without inheritance.
//     Execute lambdas based on criteria such as character health and role, illustrating how lambdas create flexible and reusable code.

namespace Kata1
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Action<Character> PrimaryAction { get; set; }

        public Character(string name, int health, Action<Character> primaryAction)
        {
            Name = name;
            Health = health;
            PrimaryAction = primaryAction;
        }
        
        public override string ToString() => $"{Name} (Health: {Health})";
    }

    public class Program
    {
        static void Main()
        {
            var warrior = new Character("Warrior", health: 100, primaryAction: targets =>
            {
                var enemy = targets.FirstOrDefault(c => c != null);
                if (enemy != null)
                {
                    Console.WriteLine($"{warrior.Name} attacks {enemy.Name}!");
                    enemy.Health -= 20;
                    Console.WriteLine($"{enemy.Name} now has {enemy.Health} health");
                }
            });

            var healer = new Character("Healer", health: 100, primaryAction: targets =>
            {
                var allyToHeal = targets.OrderBy(c => c.Health).FirstOrDefault();
                if (allyToHeal != null && allyToHeal.Health < 100)
                {
                    Console.WriteLine($"{healer.Name} healsd {allyToHeal.Name}!");
                    allyToHeal.Health += 15;
                    Console.WriteLine($"{allyToHeal.Name} now has {allyToHeal.Health} health");
                }
            });

            var enemy = new Character("Enemy", health: 80, primaryAction: _ =>
            {
                Console.WriteLine($"{enemy.Name} is preparing its next move...");
            });

            Character[] characters = { warrior, healer, enemy };
            
            Console.WriteLine("Starting actions based on health priority...\n");
            foreach (var character in characters.OrderByDescending(ch => ch.Health < 50))
            {
                Console.WriteLine($"{character.Name}'s Turn:");
                character.PrimaryAction(characters.Where(c => c != character).ToArray());
                Console.WriteLine();
            };
        }
    }
}