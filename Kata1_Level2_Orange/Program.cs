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
    
}
