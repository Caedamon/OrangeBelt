// Requirements
//
//     Refined Character Creation with SRP:
//
// Maintain Warrior and Healer characters, but separate each action into its own CharacterRole class (e.g., WarriorRole, HealerRole) that contains specific behavior properties, following SRP.
//     Encapsulate and Extend Actions Using OCP:
//
// Create specific Action properties within CharacterRole classes (e.g., AttackAction in WarriorRole and HealAction in HealerRole).
//     HealerRole should contain a method or lambda to find and heal the character with the lowest health.
//     Design the PrimaryAction as an Action that the character can perform independently based on health criteria, allowing new character roles to be added without modifying existing code.
//     Dynamic Action Execution:
//
// Dynamically execute the lambda in PrimaryAction, prioritizing actions based on health and role.
//     Expected Skill Outcome
//
//     See how SRP and OCP improve modularity and maintainability by splitting character behaviors into focused role classes.
//     Extend code functionality (e.g., adding new character roles) without modifying existing code, showcasing the power of OCP in real-world applications.

namespace Kata1_2
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Action PrimaryAction { get; set; }

        public Character(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void ExecutePrimaryAction()
        {
            PrimaryAction?.Invoke();
        }
    }

    public interface ICharacterRole
    {
        void AssignPrimaryAction(Character character, List<Character> characters);
    }

    public class WarriorRole : ICharacterRole
    {
        private Action<Character> AttackAction => character =>
        {
            Console.WriteLine($"{character.Name} charges with a fierce attack!");
        };

        public void AssignPrimaryAction(Character character, List<Character> characters)
        {
            character.PrimaryAction = () =>
            {
                if (character.Health < 50)
                {
                    Console.WriteLine($"{character.Name} is attacking first due to low health!");
                }

                AttackAction(character);
            };
        }
    }

    public class HealerRole : ICharacterRole
    {
        private Action<Character, Character> HealAction => (healer, target) =>
        {
            Console.WriteLine($"{healer.Name} heals {target.Name} for 15 health!");
            target.Health += 15;
        };
    }
    
    public void AssignPrimaryAction(Character character, List<Character> characters)
    {
        character.PrimaryAction = () =>
        {
            var target = characters.Where(c => c.Health > 0).OrderBy(c => c.Health).FirstOrDefault();
            if (target != null && target.Health < 50)
            {
                Console.WriteLine($"{character.Name} is prioritizing healing for {target.Name} who has the lowest health.");
                HealAction(character, target);
            }
            else
            {
                Console.WriteLine($"{character.Name} has no one to heal.");
            }
        };
    }

    class Program
    {
        static void Main(string[] args)
        {
            var characters = new List<Character>
            {
                new Character("Bran", 30),
                new Character("Arin", 90),
                new Character("Dalia", 70),
                new Character("Cara", 70)
            };
            
            var warriorRole = new WarriorRole();
            var healerRole = new HealerRole();
            
            warriorRole.AssignPrimaryAction(characters[0], characters);
            warriorRole.AssignPrimaryAction(characters[1], characters);
            healerRole.AssignPrimaryAction(characters[2], characters);
            warriorRole.AssignPrimaryAction(characters[3], characters);
            
            Console.WriteLine("Starting actions based on character health...\n");

            foreach (var character in characters)
            {
                character.ExecutePrimaryAction();
                Console.WriteLine($"{character.Name}'s health: {character.Health}");
                Console.WriteLine();
            }
        }
    }
}
