//isnt this pretty much the same as the previous assignment?

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

    //So... this interface is for primary action, it decides each role's acction depending on, well, context xD
    public interface ICharacterRole
    {
        void AssignPrimaryAction(Character character, List<Character> characters);
    }

    
    public class WarriorRole : ICharacterRole
    {
        //Setting up class-specific role, in this case for warriors and for attacking based logic
        //wish id known this when i was making my text based arena xD
        private Action<Character> AttackAction => character =>
        {
            Console.WriteLine($"{character.Name} charges with a fierce attack!");
        };
        
        //same as befor, but more dynamic, ie its dependent on warr-health
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
        //same as for warrior, but healer based, do i really need to add this comment?
        //it feels a bitt, excessive. but seriously, i do not know how to make good commentary on my code or where to add it
        //Chastgtp said to "add commentary that describes why and not what"
        private Action<Character, Character> HealAction => (healer, target) =>
        {
            Console.WriteLine($"{healer.Name} heals {target.Name} for 15 health!");
            target.Health += 15;
        };

        public void AssignPrimaryAction(Character character, List<Character> characters)
        {
            character.PrimaryAction = () =>
            {
                //this code segment firstly finds who needs to get healed and then who to prioritize based on the health of the target
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

            //executing role's assigned primary action
            foreach (var character in characters)
            {
                character.ExecutePrimaryAction();
                Console.WriteLine($"{character.Name}'s health: {character.Health}");
                Console.WriteLine();
            }
        }
    }
}
