namespace Guild.Models
{
    // Enums voor de types
    public enum ArmorType { Light, Medium, Heavy }
    public enum WeaponType { Melee, Ranged, Magic }
    public enum ConsumableType { Trap, Lure, Potion }

    // Basis classes
    public class Weapon
    {
        public string Name { get; set; }
        public WeaponType Type { get; set; }
        public decimal Price { get; set; }
    }

    public class Armor
    {
        public string Name { get; set; }
        public ArmorType Type { get; set; }
        public decimal Price { get; set; }
    }

    public class Consumable
    {
        public string Name { get; set; }
        public ConsumableType Type { get; set; }
        public string UniqueEffect { get; set; }
        public decimal Price { get; set; }
    }

    public class Loadout
    {
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public List<Consumable> Consumables { get; set; } = new List<Consumable>();

        // Voeg een consumable toe, check of het er niet meer dan 3 zijn
        public bool AddConsumable(Consumable item)
        {
            if (Consumables.Count < 3)
            {
                Consumables.Add(item);
                return true;
            }
            return false;
        }

        public decimal GetTotalCost()
        {
            decimal total = EquippedWeapon?.Price ?? 0;
            total += EquippedArmor?.Price ?? 0;
            foreach (var item in Consumables) total += item.Price;
            return total;
        }
    }

    public class Quest
    {
        public string Description { get; set; }
        public WeaponType RequiredWeapon { get; set; }
        public ArmorType RequiredArmor { get; set; }
        public ConsumableType? RequiredConsumable { get; set; }

        // Deze methode bepaalt of je de juiste spullen hebt verkocht!
        public bool EvaluateSuccess(Loadout loadout)
        {
            if (loadout.EquippedWeapon?.Type != RequiredWeapon) return false;
            if (loadout.EquippedArmor?.Type != RequiredArmor) return false;

            // Als de quest een specifieke consumable eist, check of deze in de lijst zit
            if (RequiredConsumable.HasValue)
            {
                bool hasConsumable = loadout.Consumables.Any(c => c.Type == RequiredConsumable.Value);
                if (!hasConsumable) return false;
            }

            return true;
        }
    }
}
