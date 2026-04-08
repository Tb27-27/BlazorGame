namespace Product;

// Items
public enum EquipmentType { Weapon, Armor, Consumable }
public enum WeaponType { Melee, Ranged, Magic }
public enum ArmorType { Light, Medium, Heavy }
public enum ConsumableType { Trap, Lure, Potion }

// Vervanger voor ProductCategory
public class ItemCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EquipmentType MainType { get; set; }
    public string Description { get; set; }

    public ItemCategory(int id, string name, EquipmentType mainType, string description)
    {
        Id = id;
        Name = name;
        MainType = mainType;
        Description = description;
    }
}

// Vervanger voor Product
public class GuildItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ItemCategory Category { get; set; }

    public GuildItem(int id, string name, decimal price, ItemCategory category)
    {
        Id = id;
        Name = name;
        Price = price;
        Category = category;
    }
}

// Uitrusting Statistieken (om aan te geven welk type armor/weapon het is)
public class EquipmentStats
{
    public string StatName { get; set; } // Bijv. "Damage" of "Defense"
    public int StatValue { get; set; }
    public WeaponType? WType { get; set; }
    public ArmorType? AType { get; set; }

    public EquipmentStats(string statName, int statValue, WeaponType? wType, ArmorType? aType)
    {
        StatName = statName;
        StatValue = statValue;
        WType = wType;
        AType = aType;
    }
}

// Consumable Effecten (voor de potions, traps en lures)
public class ConsumableDetail
{
    public string UniqueEffect { get; set; }
    public ConsumableType CType { get; set; }
    // usable charges (bijv. een val kan 3 keer gebruikt worden voordat hij kapot gaat)
    public int Charges { get; set; }

    public ConsumableDetail(string uniqueEffect, ConsumableType cType, int charges, bool isOneOfAKind)
    {
        UniqueEffect = uniqueEffect;
        CType = cType;
        Charges = charges;
    }
}