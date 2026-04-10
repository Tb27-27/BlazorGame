using System;
using System.Collections.Generic;

namespace Core
{
    public enum SlotType { Weapon, Armor, Consumable, Action }
    public enum ItemTag { LightSource, Short, Long, Heavy, Light, Medium, Healing, Buff }

    public abstract class Card
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public SlotType Type { get; set; }
    }

    public class ItemCard : Card
    {
        public List<ItemTag> Tags { get; set; } = new();
    }

    public class ActionCard : Card
    {
        public int CardsToDraw { get; set; }
    }

    public class Quest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ItemTag> RequiredTags { get; set; } = new();
        public List<ItemTag> PenalizedTags { get; set; } = new();
    }

    public class Adventurer
    {
        public string Name { get; set; } = string.Empty;
    }
}