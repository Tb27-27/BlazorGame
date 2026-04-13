using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class GameEngine
    {
        public GameState State { get; private set; }
        private Random _rng = new Random();
        private List<Quest> _questDatabase = new();
        private List<string> _adventurerNames = new();

        public int maxCardsInHand = 5;

        public GameEngine()
        {
            State = new GameState();
            SeedData();
            StartDay();
        }

        private void SeedData()
        {
            // adventurers
            _adventurerNames.AddRange(new[] {
                "Tom", "Shadeheart", "Lady Tude",
                "Sir Vival", "John Adventurer", "Jean Luc Piacanto",
                "Legolad", "Tay Loreswift"
            });

            // Quests
            _questDatabase.Add(new Quest
            {
                Title = "Goblin Burrow",
                Description = "Dark, narrow, and filled with quick enemies.",
                RequiredTags = new List<ItemTag> { ItemTag.LightSource, ItemTag.Short, ItemTag.Light },
                PenalizedTags = new List<ItemTag> { ItemTag.Heavy, ItemTag.Long }
            });
            _questDatabase.Add(new Quest
            {
                Title = "Troll Bridge",
                Description = "A massive troll blocking the way. Needs heavy hitting.",
                RequiredTags = new List<ItemTag> { ItemTag.Heavy, ItemTag.Healing },
                PenalizedTags = new List<ItemTag> { ItemTag.Short, ItemTag.Light }
            });
            _questDatabase.Add(new Quest
            {
                Title = "Haunted Crypt",
                Description = "Undead lurk in pitch blackness. Requires holy light and sturdy armor.",
                RequiredTags = new List<ItemTag> { ItemTag.LightSource, ItemTag.Medium, ItemTag.Buff },
                PenalizedTags = new List<ItemTag> { ItemTag.Short }
            });
            _questDatabase.Add(new Quest
            {
                Title = "Bandit Camp Outskirts",
                Description = "A stealthy approach is best. Keep it light and keep your distance.",
                RequiredTags = new List<ItemTag> { ItemTag.Light, ItemTag.Long },
                PenalizedTags = new List<ItemTag> { ItemTag.Heavy, ItemTag.LightSource } // A torch gives you away!
            });
            _questDatabase.Add(new Quest
            {
                Title = "Dragon's Lair",
                Description = "Absolute suicide without heavy protection and lots of healing.",
                RequiredTags = new List<ItemTag> { ItemTag.Heavy, ItemTag.Healing, ItemTag.Buff },
                PenalizedTags = new List<ItemTag> { ItemTag.Light, ItemTag.Short }
            });

            // Deck
            State.Deck.AddRange(new List<Card>
            {
                    // Consumables
                    new ItemCard { Name = "Torch", Type = SlotType.Consumable, Tags = new() { ItemTag.LightSource } },
                    new ItemCard { Name = "Oil Lantern", Type = SlotType.Consumable, Tags = new() { ItemTag.LightSource, ItemTag.Buff } },
                    new ItemCard { Name = "Health Potion", Type = SlotType.Consumable, Tags = new() { ItemTag.Healing } },
                    new ItemCard { Name = "Large Bandage", Type = SlotType.Consumable, Tags = new() { ItemTag.Healing } },
                    new ItemCard { Name = "Whetstone", Type = SlotType.Consumable, Tags = new() { ItemTag.Buff } },
        
                    // Weapons
                    new ItemCard { Name = "Short Sword", Type = SlotType.Weapon, Tags = new() { ItemTag.Short, ItemTag.Light } },
                    new ItemCard { Name = "Iron Dagger", Type = SlotType.Weapon, Tags = new() { ItemTag.Short } },
                    new ItemCard { Name = "Greatsword", Type = SlotType.Weapon, Tags = new() { ItemTag.Long, ItemTag.Heavy } },
                    new ItemCard { Name = "Spear", Type = SlotType.Weapon, Tags = new() { ItemTag.Long, ItemTag.Medium } },
                    new ItemCard { Name = "Warhammer", Type = SlotType.Weapon, Tags = new() { ItemTag.Short, ItemTag.Heavy } },

                    // Armor
                    new ItemCard { Name = "Leather Armor", Type = SlotType.Armor, Tags = new() { ItemTag.Light } },
                    new ItemCard { Name = "Chainmail", Type = SlotType.Armor, Tags = new() { ItemTag.Medium } },
                    new ItemCard { Name = "Plate Armor", Type = SlotType.Armor, Tags = new() { ItemTag.Heavy } },
                    new ItemCard { Name = "Tower Shield", Type = SlotType.Armor, Tags = new() { ItemTag.Heavy, ItemTag.Buff } },

                    // Actions
                    new ActionCard { Name = "Pot of Quark", Description = "Draw 2 Cards", CardsToDraw = 2, Type = SlotType.Action }
            });
        }

        public void StartDay()
        {
            State.CustomersServedToday = 0;
            State.DailyLog.Add($"--- Day {State.Day} Started ---");

            // Shuffle discard pile back into deck
            State.Deck.AddRange(State.DiscardPile);
            State.DiscardPile.Clear();

            // Simple shuffle
            State.Deck = State.Deck.OrderBy(x => _rng.Next()).ToList(); 

            NextCustomer();
        }

        private void NextCustomer()
        {
            if (State.CustomersServedToday >= 3)
            {
                State.DailyLog.Add("The day is over. Press 'Start Next Day'.");
                State.CurrentAdventurer = null;
                State.CurrentQuest = null;
                return;
            }

            //State.CurrentAdventurer = new Adventurer { Name = "Adventurer Bob" };
            State.CurrentAdventurer = new Adventurer { Name = _adventurerNames[_rng.Next(_adventurerNames.Count)] };
            State.CurrentQuest = _questDatabase[_rng.Next(_questDatabase.Count)];
            State.DailyLog.Add($"{State.CurrentAdventurer.Name} arrived looking for gear for: {State.CurrentQuest.Title}");

            // Clear previous equipment slots
            State.EquippedWeapon = null;
            State.EquippedArmor = null;
            State.EquippedConsumables.Clear();

            // Replenish hand up to 5 cards (keep existing cards)
            // Chnged to maxCardsInHand variable for easier tweaking and potential future upgrades
            int cardsNeeded = (maxCardsInHand - State.Hand.Count);
            if (cardsNeeded > 1)
            {
                DrawCards(cardsNeeded);
            }
        }

        public void DrawCards(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                // If the deck is empty, shuffle the discard pile back in
                if (State.Deck.Count == 0)
                {
                    if (State.DiscardPile.Count == 0)
                    {
                        // If the discard pile is also empty, we physically have no more cards to draw.
                        break;
                    }

                    State.Deck.AddRange(State.DiscardPile);
                    State.DiscardPile.Clear();

                    // Shuffle
                    State.Deck = State.Deck.OrderBy(x => _rng.Next()).ToList();

                    // Log the shuffle event
                    State.DailyLog.Add("Deck empty. Shuffled discard pile into a new deck.");
                }

                var card = State.Deck[0];
                State.Deck.RemoveAt(0);
                State.Hand.Add(card);
            }
        }

        // Moves a card from hand to the active slots
        public void PlayCard(Card card)
        {
            if (card is ActionCard actionCard)
            {
                DrawCards(actionCard.CardsToDraw);
                State.Hand.Remove(card);
                State.DiscardPile.Add(card);
            }
            else if (card is ItemCard itemCard)
            {
                if (itemCard.Type == SlotType.Weapon) { 
                    if (State.EquippedWeapon != null) {
                        UnequipCard(State.EquippedWeapon); 
                    }; 
                    State.EquippedWeapon = itemCard; 
                }
                if (itemCard.Type == SlotType.Armor) { 
                    if (State.EquippedArmor != null) { 
                        UnequipCard(State.EquippedArmor); 
                    }; 
                    State.EquippedArmor = itemCard; 
                }
                if (itemCard.Type == SlotType.Consumable && State.EquippedConsumables.Count < 3)
                    State.EquippedConsumables.Add(itemCard);

                State.Hand.Remove(card);
            }
        }

        public void UnequipCard(ItemCard card)
        {
            if (card == null) return;

            // Find where the card is equipped, remove it, and put it back in hand
            if (State.EquippedWeapon == card)
                State.EquippedWeapon = null;
            else if (State.EquippedArmor == card)
                State.EquippedArmor = null;
            else if (State.EquippedConsumables.Contains(card))
                State.EquippedConsumables.Remove(card);
            else
                return;

            State.Hand.Add(card);
        }

        public void SendAdventurer()
        {
            if (State.CurrentQuest == null) return;

            // Base chance
            int successChance = 25;
            
            
            var equippedTags = new List<ItemTag>();

            // Calculate Success Rate
            if (State.EquippedWeapon != null) equippedTags.AddRange(State.EquippedWeapon.Tags);
            if (State.EquippedArmor != null) equippedTags.AddRange(State.EquippedArmor.Tags);
            foreach (var cons in State.EquippedConsumables) equippedTags.AddRange(cons.Tags);

            foreach (var tag in State.CurrentQuest.RequiredTags)
                if (equippedTags.Contains(tag)) successChance += 25;

            foreach (var tag in State.CurrentQuest.PenalizedTags)
                if (equippedTags.Contains(tag)) successChance -= 15;

            successChance = Math.Clamp(successChance, 25, 100);

            // Roll the dice
            int roll = _rng.Next(1, 101);
            if (roll <= successChance)
            {
                State.DailyLog.Add($"> SUCCESS! ({successChance}% chance). Earned 50g.");
                State.Gold += 50;
                State.Experience += 10;
            }
            else
            {
                State.DailyLog.Add($"> FAILED! ({successChance}% chance). Bob died.");
            }

            // Cleanup and move to next
            if (State.EquippedWeapon != null) State.DiscardPile.Add(State.EquippedWeapon);
            if (State.EquippedArmor != null) State.DiscardPile.Add(State.EquippedArmor);
            State.DiscardPile.AddRange(State.EquippedConsumables);

            State.CustomersServedToday++;
            NextCustomer();
        }
    }
}