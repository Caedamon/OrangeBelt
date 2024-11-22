namespace Kata1_1
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Action<Character[]> PrimaryAction { get; set; }

        public Character(string name, int health, Action<Character[]> primaryAction)
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
        // had to put theese variables here so i wouldnt get "undecleared" errors o_O
        // i mean, i get it, cant use a local variable before its declared (got it for each class)
        // but still! xD
            Character warrior = null;
            Character healer = null;
            Character enemy = null;
            
            //Basic warrior settup
            warrior = new Character("Warrior", 100, targets =>
            {
                var enemyTarget = targets.FirstOrDefault(c => c != null && c.Health > 0); //attack first av avilable enemy
                if (enemyTarget != null)
                {
                    Console.WriteLine($"{warrior.Name} attacks {enemyTarget.Name}!");
                    enemyTarget.Health -= 20;
                    Console.WriteLine($"{enemyTarget.Name} now has {enemyTarget.Health} health.");
                }
            });
            
            //basic healer settup
            healer = new Character("Healer", 100, targets =>
            {
                var allyToHeal = targets.OrderBy(c => c.Health).FirstOrDefault(c => c.Health < 100); //heal ally with lowest health
                if (allyToHeal != null)
                {
                    Console.WriteLine($"{healer.Name} heals {allyToHeal.Name}!");
                    allyToHeal.Health += 15;
                    Console.WriteLine($"{allyToHeal.Name} now has {allyToHeal.Health} health.");
                }
            });
            
            //enemy setup, not even basic! its a placeholder xD
            enemy = new Character("Enemy", 80, _ =>
            {
                Console.WriteLine($"{enemy.Name} is preparing its next move...");
            });
            
            
            Character[] characters = { warrior, healer, enemy };
            
            //Turn priority, low health = act first
            Console.WriteLine("Starting actions based on health priority...\n");
            foreach (var character in characters.OrderBy(ch => ch.Health < 50 ? 0 : 1))
            {
                Console.WriteLine($"{character.Name}'s Turn:");
                character.PrimaryAction(characters.Where(c => c != character).ToArray());
                Console.WriteLine();
            }
        }
    }
}