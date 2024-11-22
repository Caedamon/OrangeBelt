namespace Kata1
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
            Character warrior = null;
            Character healer = null;
            Character enemy = null;

            warrior = new Character("Warrior", 100, targets =>
            {
                var enemyTarget = targets.FirstOrDefault(c => c != null && c.Health > 0);
                if (enemyTarget != null)
                {
                    Console.WriteLine($"{warrior.Name} attacks {enemyTarget.Name}!");
                    enemyTarget.Health -= 20;
                    Console.WriteLine($"{enemyTarget.Name} now has {enemyTarget.Health} health.");
                }
            });
            healer = new Character("Healer", 100, targets =>
            {
                var allyToHeal = targets.OrderBy(c => c.Health).FirstOrDefault(c => c.Health < 100);
                if (allyToHeal != null)
                {
                    Console.WriteLine($"{healer.Name} heals {allyToHeal.Name}!");
                    allyToHeal.Health += 15;
                    Console.WriteLine($"{allyToHeal.Name} now has {allyToHeal.Health} health.");
                }
            });
            enemy = new Character("Enemy", 80, _ =>
            {
                Console.WriteLine($"{enemy.Name} is preparing its next move...");
            });
            Character[] characters = { warrior, healer, enemy };
            
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