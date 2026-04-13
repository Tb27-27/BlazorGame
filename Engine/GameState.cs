using Core;
using System.Collections.Generic;

namespace Engine
{
    public class GameState
    {
        public int Day { get; set; } = 1;
        public int CustomersServedToday { get; set; } = 0;
        public int Gold { get; set; } = 0;
        public int Experience { get; set; } = 0;

        public List<Card> Deck { get; set; } = new();
        public List<Card> Hand { get; set; } = new();
        public List<Card> DiscardPile { get; set; } = new();
        public List<string> DailyLog { get; set; } = new();

        public Adventurer? CurrentAdventurer { get; set; }
        public Quest? CurrentQuest { get; set; }

        // Equipment Slots
        public ItemCard? EquippedWeapon { get; set; }
        public ItemCard? EquippedArmor { get; set; }
        public List<ItemCard> EquippedConsumables { get; set; } = new();

        // hand size
        public int maxCardsInHand { get; set; } = 5;

        // Shop inventory
        public List<ShopListing> ShopInventory { get; set; } = new();
    }
}