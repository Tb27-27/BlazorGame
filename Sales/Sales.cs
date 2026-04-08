namespace Sales;
using Product;

{

    // Vervanger voor Customer
    public class Adventurer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public decimal GoldBudget { get; set; }

        public Adventurer(int id, string name, int level, decimal goldBudget)
        {
            Id = id;
            Name = name;
            Level = level;
            GoldBudget = goldBudget;
        }
    }

    // Vervanger voor DeliveryMethod (Hoe de avonturier reist: Te paard, lopend, portaal)
    public class TravelMethod
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public decimal TravelCost { get; set; }
        public int DangerLevel { get; set; } // Hoger gevaar = kans op mislukken

        public TravelMethod(int id, string methodName, decimal travelCost, int dangerLevel)
        {
            Id = id;
            MethodName = methodName;
            TravelCost = travelCost;
            DangerLevel = dangerLevel;
        }
    }

    // Vervanger voor SalesOrderLine (Een slot in hun uitrusting, bijv. "Weapon Slot")
    public class LoadoutSlot
    {
        public int Id { get; set; }
        public GuildItem EquippedItem { get; set; }
        public int Quantity { get; set; } // Voor consumables max 3, voor de rest 1
        public string SlotType { get; set; } // "Weapon", "Armor", of "Consumable"

        public LoadoutSlot(int id, GuildItem equippedItem, int quantity, string slotType)
        {
            Id = id;
            EquippedItem = equippedItem;
            Quantity = quantity;
            SlotType = slotType;
        }
    }

    // Vervanger voor SalesOrder (De complete gekochte uitrusting voor de queeste)
    public class QuestLoadout
    {
        public int LoadoutId { get; set; }
        public Adventurer Client { get; set; }
        public List<LoadoutSlot> Slots { get; set; }
        public decimal TotalCost { get; set; }

        public QuestLoadout(int loadoutId, Adventurer client, List<LoadoutSlot> slots, decimal totalCost)
        {
            LoadoutId = loadoutId;
            Client = client;
            Slots = slots;
            TotalCost = totalCost;
        }
    }

    // Vervanger voor Shipment (De daadwerkelijke expeditie waar de avonturier op gaat)
    public class Expedition
    {
        public int ExpeditionId { get; set; }
        public List<QuestLoadout> DispatchedLoadouts { get; set; } // Meerdere avonturiers per expeditie mogelijk!
        public TravelMethod TravelRoute { get; set; }
        public bool MissionSuccess { get; set; } // Hebben ze de juiste spullen gekocht en het overleefd?

        public Expedition(int expeditionId, List<QuestLoadout> dispatchedLoadouts, TravelMethod travelRoute, bool missionSuccess)
        {
            ExpeditionId = expeditionId;
            DispatchedLoadouts = dispatchedLoadouts;
            TravelRoute = travelRoute;
            MissionSuccess = missionSuccess;
        }
    }
}
