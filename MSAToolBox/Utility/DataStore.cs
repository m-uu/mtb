﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAToolBox.Utility
{
    public class DB
    {
        private static LegacyWorldEntities _LEGACY;
        private static LegacyDataEntities _DATA;

        public static LegacyWorldEntities LEGACY
        {
            get
            {
                if (_LEGACY == null)
                    _LEGACY = new LegacyWorldEntities();
                return _LEGACY;
            }
            set
            {
                _LEGACY = value;
            }
        }

        public static LegacyDataEntities DATA
        {
            get
            {
                if (_DATA == null)
                    _DATA = new LegacyDataEntities();
                return _DATA;
            }
            set
            {
                _DATA = value;
            }
        }

        public static void Refresh()
        {
            _LEGACY = new LegacyWorldEntities();
            _DATA = new LegacyDataEntities();
        }
    }

    // preload defines
    public class DataDefine
    {
        private static string DBC_ROOT_PATH = MainWindow.SERVER_PATH + "dbc/";
        private static int DBC_COLUMN_SIZE = 4;

        private static Dictionary<int, string> LoadDBDefine(string type)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            switch (type)
            {
                case "ItemQuality":
                    var qualityDefines = from d in DB.DATA.define_item_quality select d;
                    foreach (var define in qualityDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemAmmoType":
                    var ammoTypeDefines = from d in DB.DATA.define_item_ammo_type select d;
                    foreach (var define in ammoTypeDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemBonding":
                    var bondingDefines = from d in DB.DATA.define_item_bonding select d;
                    foreach (var define in bondingDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemDamageSchool":
                    var damageSchoolDefines = from d in DB.DATA.define_item_damage_type select d;
                    foreach (var define in damageSchoolDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemInventoryType":
                    var inventoryTypeDefines = from d in DB.DATA.define_item_inventory_type select d;
                    foreach (var define in inventoryTypeDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemMaterial":
                    var materialDefines = from d in DB.DATA.define_item_material select d;
                    foreach (var define in materialDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemSheath":
                    var sheathDefines = from d in DB.DATA.define_item_sheath select d;
                    foreach (var define in sheathDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemSocketColor":
                    var socketColorDefines = from d in DB.DATA.define_item_socket_color select d;
                    foreach (var define in socketColorDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemSpellTrigger":
                    var spellTriggerDefines = from d in DB.DATA.define_item_spell_trigger select d;
                    foreach (var define in spellTriggerDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemStatType":
                    var statTypeDefines = from d in DB.DATA.define_item_stat_type select d;
                    foreach (var define in statTypeDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ReputationRank":
                    var reputationRankDefines = from d in DB.DATA.define_reputation_rank select d;
                    foreach (var define in reputationRankDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "GossipIcon":
                    var gossipIconDefines = from d in DB.DATA.define_gossip_icon select d;
                    foreach (var define in gossipIconDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "SpellEffect":
                    var spellEffectDefines = from d in DB.DATA.define_spell_effect select d;
                    foreach (var define in spellEffectDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "SpellAura":
                    var spellAuraDefines = from d in DB.DATA.define_spell_aura select d;
                    foreach (var define in spellAuraDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "SpellEffectTarget":
                    var spellEffectTargetDefines = from d in DB.DATA.define_spell_implicit_target select d;
                    foreach (var define in spellEffectTargetDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "SpellAuraState":
                    var spellAuraStateDefines = from d in DB.DATA.define_spell_aura_state select d;
                    foreach (var define in spellAuraStateDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "SpellFamily":
                    var spellFamilyDefines = from d in DB.DATA.define_spell_family select d;
                    foreach (var define in spellFamilyDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "SpellDamageClass":
                    var spellDamageClassDefines = from d in DB.DATA.define_spell_damage_class select d;
                    foreach (var define in spellDamageClassDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "SpellPowerType":
                    var spellPowerTypeDefines = from d in DB.DATA.define_spell_power_type select d;
                    foreach (var define in spellPowerTypeDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                case "ItemGroupSound":
                    var groupSoundDefines = from d in DB.DATA.define_item_group_sound select d;
                    foreach (var define in groupSoundDefines)
                        dict.Add(define.id, define.id + " - " + define.define);
                    break;
                default:
                    break;
            }
            return dict;
        }

        private static Dictionary<int, string> LoadDBCDefine(string type)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            FileStream stream;
            BinaryReader r;
            int records = 0;
            switch (type)
            {
                case "SpellRadius":
                    // 4 columns - 16 bytes per row
                    // 1 for id
                    // 2 for radius
                    // 3 for radius for level
                    // 4 for max radius
                    stream = File.OpenRead(DBC_ROOT_PATH + "SpellRadius.dbc");
                    r = new BinaryReader(stream);
                    stream.Position = 4;
                    records = r.ReadInt32();
                    stream.Position = 20;
                    // no dbc record starts at id 0 so define one.
                    dict.Add(0, "0 Yard");
                    for (int i = 0; i != records; ++i)
                    {
                        int id = r.ReadInt32();
                        float radius = r.ReadSingle();
                        float radiusPerLevel = r.ReadSingle();
                        float radiusMax = r.ReadSingle();
                        string s = String.Format("{3} - {0}Y / +{1} per Level / Max: {2}", radius, radiusPerLevel, radiusMax, id);
                        dict.Add(id, s);
                    }
                    r.Close();
                    stream.Close();
                    break;
                case "SpellDuration":
                    // 4 columns - 16 bytes per row
                    // 1 for id
                    // 2 for base duration
                    // 3 for duration per level
                    // 4 for max duration
                    stream = File.OpenRead(DBC_ROOT_PATH + "SpellDuration.dbc");
                    r = new BinaryReader(stream);
                    stream.Position = 4;
                    records = r.ReadInt32();
                    stream.Position = 20;
                    // no dbc record starts at id 0 so define one.
                    dict.Add(0, "0ms");
                    for (int i = 0; i != records; ++i)
                    {
                        int id = r.ReadInt32();
                        float duration = r.ReadInt32();
                        float durationPerLevel = r.ReadInt32();
                        float durationMax = r.ReadInt32();
                        string s = String.Format("{3} - {0:F1}ms(base) / {1:F1}ms(per level) / {2:F1}ms(max)", duration, durationPerLevel, durationMax, id);
                        dict.Add(id, s);
                    }
                    r.Close();
                    stream.Close();
                    break;
                case "SpellRange":
                    // 40 columns - 160 bytes per row
                    // 1 for id
                    // 2 for min enemy range
                    // 3 for min ally range
                    // 4 for max enemy range
                    // 5 for max ally range
                    stream = File.OpenRead(DBC_ROOT_PATH + "SpellRange.dbc");
                    r = new BinaryReader(stream);
                    stream.Position = 4;
                    records = r.ReadInt32();
                    stream.Position = 20;
                    // no dbc record starts at id 0 so define one.
                    dict.Add(0, "0 - default self only");
                    for (int i = 0; i != records; ++i)
                    {
                        int id = r.ReadInt32();
                        float minEnemyRange = r.ReadSingle();
                        float minAllyRange = r.ReadSingle();
                        float maxEnemyRange = r.ReadSingle();
                        float maxAllyRange = r.ReadSingle();
                        string s = String.Format("{4} - Hostile:{0}~{1} / Friend:{2}~{3}", minAllyRange, maxEnemyRange, minAllyRange, maxAllyRange, id);
                        dict.Add(id, s);
                        stream.Position += 140;
                    }
                    r.Close();
                    stream.Close();
                    break;
                case "SpellCastTime":
                    // 4 columns - 16 bytes per row
                    // 1 for id
                    // 2 for casttime per level
                    // 3 for min casttime
                    stream = File.OpenRead(DBC_ROOT_PATH + "SpellCastTimes.dbc");
                    r = new BinaryReader(stream);
                    stream.Position = 4;
                    records = r.ReadInt32();
                    stream.Position = 20;
                    // no dbc record starts at id 0 so define one.
                    dict.Add(0, "0ms");
                    for (int i = 0; i != records; ++i)
                    {
                        int id = r.ReadInt32();
                        float casttime = r.ReadInt32();
                        float casttimePerLevel = r.ReadInt32();
                        float mincasttime = r.ReadInt32();
                        string s = String.Format("{3} - {0:F1}ms(base) / {1:F1}ms(per level) / {2:F1}ms(min)", casttime, casttimePerLevel, mincasttime, id);
                        dict.Add(id, s);
                    }
                    r.Close();
                    stream.Close();
                    break;
                default:
                    break;
            }
            return dict;
        }

        private static Dictionary<int, string> LoadDBCDefine(string file, int idCol, int nameCol, bool addZeroIndex = false, bool withID = true)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            if (addZeroIndex)
                dict.Add(0, "0 - NA/DEFAULT");

            using (FileStream stream = File.OpenRead(DBC_ROOT_PATH + file))
            {
                BinaryReader r = new BinaryReader(stream);
                stream.Position = 4;
                int records = r.ReadInt32();
                stream.Position = 12;
                int rowSize = r.ReadInt32();
                for (int i = 0; i != records; ++i)
                {
                    stream.Position = 20 + rowSize * i + DBC_COLUMN_SIZE * (idCol - 1);
                    int id = r.ReadInt32();
                    stream.Position = 20 + rowSize * i + DBC_COLUMN_SIZE * (nameCol - 1);
                    int nameOfs = r.ReadInt32();
                    stream.Position = 20 + rowSize * records + nameOfs;
                    string s = "";
                    List<byte> blist = new List<byte>();
                    while (true)
                    {
                        byte b = r.ReadByte();
                        if (b == 0)
                            break;
                        blist.Add(b);
                    }
                    if (blist.Count != 0)
                    {
                        byte[] stringBytes = new byte[blist.Count];
                        for (int j = 0; j != blist.Count; ++j)
                            stringBytes[j] = blist[j];
                        s = Encoding.UTF8.GetString(stringBytes);
                    }
                    if (withID)
                        dict.Add(id, id + " - " + s);
                    else
                        dict.Add(id, s);
                }
                r.Close();
                return dict;
            }
        }

        private static Dictionary<int, string>[] LoadDBCDefine(string file, int categoryCol, int idCol, int nameCol, int categoryCount)
        {
            Dictionary<int, string>[] dicts = new Dictionary<int, string>[categoryCount];

            using (FileStream stream = File.OpenRead(DBC_ROOT_PATH + file))
            {
                BinaryReader r = new BinaryReader(stream);
                stream.Position = 4;
                int records = r.ReadInt32();
                stream.Position = 12;
                int rowSize = r.ReadInt32();
                for (int i = 0; i != records; ++i)
                {
                    stream.Position = 20 + rowSize * i + DBC_COLUMN_SIZE * (categoryCol - 1);
                    int category = r.ReadInt32();
                    stream.Position = 20 + rowSize * i + DBC_COLUMN_SIZE * (idCol - 1);
                    int id = r.ReadInt32();
                    stream.Position = 20 + rowSize * i + DBC_COLUMN_SIZE * (nameCol - 1);
                    int nameOfs = r.ReadInt32();
                    stream.Position = 20 + rowSize * records + nameOfs;
                    string s = "";
                    List<byte> blist = new List<byte>();
                    while (true)
                    {
                        byte b = r.ReadByte();
                        if (b == 0)
                            break;
                        blist.Add(b);
                    }
                    if (blist.Count != 0)
                    {
                        byte[] stringBytes = new byte[blist.Count];
                        for (int j = 0; j != blist.Count; ++j)
                            stringBytes[j] = blist[j];
                        s = Encoding.UTF8.GetString(stringBytes);
                    }
                    if (dicts[category] == null)
                        dicts[category] = new Dictionary<int, string>();
                    dicts[category].Add(id, id + " - " + s);
                }
                r.Close();
            }
            return dicts;
        }

        public static Dictionary<int, string> ItemQuality = LoadDBDefine("ItemQuality");
        public static Dictionary<int, string> ItemAmmoType = LoadDBDefine("ItemAmmoType");
        public static Dictionary<int, string> ItemBonding = LoadDBDefine("ItemBonding");
        public static Dictionary<int, string> ItemDamageSchool = LoadDBDefine("ItemDamageSchool");
        public static Dictionary<int, string> ItemInventoryType = LoadDBDefine("ItemInventoryType");
        public static Dictionary<int, string> ItemSheath = LoadDBDefine("ItemSheath");
        public static Dictionary<int, string> ItemSocketColor = LoadDBDefine("ItemSocketColor");
        public static Dictionary<int, string> ItemSpellTrigger = LoadDBDefine("ItemSpellTrigger");
        public static Dictionary<int, string> ItemStatType = LoadDBDefine("ItemStatType");
        public static Dictionary<int, string> ItemMaterial = LoadDBDefine("ItemMaterial");
        public static Dictionary<int, string> ReputationRank = LoadDBDefine("ReputationRank");
        public static Dictionary<int, string> GossipIcon = LoadDBDefine("GossipIcon");
        public static Dictionary<int, string> SpellMechanic = LoadDBCDefine("SpellMechanic.dbc", 1, 6, true);
        public static Dictionary<int, string> SpellDispel = LoadDBCDefine("SpellDispelType.dbc", 1, 6);
        public static Dictionary<int, string> TotemCategory = LoadDBCDefine("TotemCategory.dbc", 1, 6, true);
        public static Dictionary<int, string> SpellIcon = LoadDBCDefine("SpellIcon.dbc", 1, 2, false, false);
        public static Dictionary<int, string> SkillLine = LoadDBCDefine("SkillLine.dbc", 1, 8, true);
        public static Dictionary<int, string> ItemClass = LoadDBCDefine("ItemClass.dbc", 1, 8);
        public static Dictionary<int, string> ItemBagFamily = LoadDBCDefine("ItemBagFamily.dbc", 1, 6);
        public static Dictionary<int, string> ItemPetFood = LoadDBCDefine("ItemPetFood.dbc", 1, 6, true);
        public static Dictionary<int, string> HolidayNames = LoadDBCDefine("HolidayNames.dbc", 1, 6, true);
        public static Dictionary<int, string> PageTextMaterial = LoadDBCDefine("PageTextMaterial.dbc", 1, 2, true);
        public static Dictionary<int, string> Language = LoadDBCDefine("Languages.dbc", 1, 6, true);
        public static Dictionary<int, string>[] ItemSubclass = LoadDBCDefine("ItemSubClass.dbc", 1, 2, 15, ItemClass.Count);
        public static Dictionary<int, string> ItemSet = LoadDBCDefine("ItemSet.dbc", 1, 6, true);
        public static Dictionary<int, string> Emotes = LoadDBCDefine("Emotes.dbc", 1, 2);
        public static Dictionary<int, string> SpellDuration = LoadDBCDefine("SpellDuration");
        public static Dictionary<int, string> SpellCastTime = LoadDBCDefine("SpellCastTime");
        public static Dictionary<int, string> SpellRange = LoadDBCDefine("SpellRange");
        public static Dictionary<int, string> SpellEffect = LoadDBDefine("SpellEffect");
        public static Dictionary<int, string> SpellAura = LoadDBDefine("SpellAura");
        public static Dictionary<int, string> SpellEffectTarget = LoadDBDefine("SpellEffectTarget");
        public static Dictionary<int, string> SpellAuraState = LoadDBDefine("SpellAuraState");
        public static Dictionary<int, string> SpellEffectRadius = LoadDBCDefine("SpellRadius");
        public static Dictionary<int, string> SpellFamily = LoadDBDefine("SpellFamily");
        public static Dictionary<int, string> SpellDamageClass = LoadDBDefine("SpellDamageClass");
        public static Dictionary<int, string> SpellPowerType = LoadDBDefine("SpellPowerType");
        public static Dictionary<int, string> ItemGroupSound = LoadDBDefine("ItemGroupSound");
    }

    public class DBCache
    {
        private static List<ItemInfo> Items { get; set; }
        private static List<CreatureInfo> Creatures { get; set; }

        private static void LoadItems()
        {
            Items = new List<ItemInfo>();
            var items = from d in DB.LEGACY.item_template orderby d.entry select d;
            foreach (var item in items)
                Items.Add(new ItemInfo()
                {
                    Entry = item.entry,
                    Class = item.@class,
                    SubClass = item.subclass,
                    Name = item.name,
                    Quality = item.Quality,
                });
            DB.Refresh();
        }

        public static List<ItemInfo> GetItems()
        {
            if (Items == null)
                LoadItems();
            return Items;
        }

        public static void AddItem(int entry, int _class, int subclass, string name, int quality)
        {
            if (Items == null)
                LoadItems();
            Items.Add(new ItemInfo()
            {
                Entry = entry,
                Class = _class,
                SubClass = subclass,
                Name = name,
                Quality = quality,
            });
        }

        public static void DeleteItem(int entry)
        {
            if (Items == null)
                LoadItems();
            var item = (from i in Items where i.Entry == entry select i).SingleOrDefault();
            if (item != null)
                Items.Remove(item);
        }

        public static void UpdateItem(int entry, int _class, int subclass, string name, int quality)
        {
            DeleteItem(entry);
            AddItem(entry, _class, subclass, name, quality);
        }

        private static void LoadCreatures()
        {
            Creatures = new List<CreatureInfo>();
            var creatures = from d in DB.LEGACY.creature_template orderby d.entry select d;
            foreach (var c in creatures)
                Creatures.Add(new CreatureInfo()
                {
                    Entry = c.entry,
                    Name = c.name,
                    MinLevel = c.minlevel,
                    MaxLevel = c.maxlevel,
                    Rank = c.rank
                });
            DB.Refresh();
        }

        public static List<CreatureInfo> GetCreatures()
        {
            if (Creatures == null)
                LoadCreatures();
            return Creatures;
        }

        public static void AddCreature(int entry, string name, int minlevel, int maxlevel, int rank)
        {
            if (Creatures == null)
                LoadCreatures();

            Creatures.Add(new CreatureInfo()
            {
                Entry = entry,
                Name = name,
                MinLevel = minlevel,
                MaxLevel = maxlevel,
                Rank = rank
            });
        }
        public static void UpdateCreature(int entry, string name, int minlevel, int maxlevel, int rank)
        {
            DeleteCreature(entry);
            AddCreature(entry, name, minlevel, maxlevel, rank);
        }

        public static void DeleteCreature(int entry)
        {
            if (Creatures == null)
                LoadCreatures();
            var creature = (from c in Creatures where c.Entry == entry select c).SingleOrDefault();
            if (creature != null)
                Creatures.Remove(creature);
        }
    }

    public class DataDefineStore
    {
        public Dictionary<int, string> ItemQuality { get; set; }
        public Dictionary<int, string> ItemAmmoType { get; set; }
        public Dictionary<int, string> ItemBonding { get; set; }
        public Dictionary<int, string> ItemDamageSchool { get; set; }
        public Dictionary<int, string> ItemInventoryType { get; set; }
        public Dictionary<int, string> ItemSheath { get; set; }
        public Dictionary<int, string> ItemSocketColor { get; set; }
        public Dictionary<int, string> ItemSpellTrigger { get; set; }
        public Dictionary<int, string> ItemStatType { get; set; }
        public Dictionary<int, string> ReputationRank { get; set; }
        public Dictionary<int, string> SpellMechanic { get; set; }
        public Dictionary<int, string> SpellDispel { get; set; }
        public Dictionary<int, string> TotemCategory { get; set; }
        public Dictionary<int, string> SpellIcon { get; set; }
        public Dictionary<int, string> SkillLine { get; set; }
        public Dictionary<int, string> ItemClass { get; set; }
        public Dictionary<int, string> ItemBagFamily { get; set; }
        public Dictionary<int, string> ItemPetFood { get; set; }
        public Dictionary<int, string> HolidayNames { get; set; }
        public Dictionary<int, string> PageTextMaterial { get; set; }
        public Dictionary<int, string> Language { get; set; }
        public Dictionary<int, string> ItemMaterial { get; set; }
        public Dictionary<int, string>[] ItemSubclass { get; set; }
        public Dictionary<int, string> ItemSet { get; set; }
        public Dictionary<int, string> GossipIcon { get; set; }
        public Dictionary<int, string> SpellDuration { get; set; }
        public Dictionary<int, string> SpellCastTime { get; set; }
        public Dictionary<int, string> SpellRange { get; set; }
        public Dictionary<int, string> SpellEffect { get; set; }
        public Dictionary<int, string> SpellAura { get; set; }
        public Dictionary<int, string> SpellEffectTarget { get; set; }
        public Dictionary<int, string> SpellAuraState { get; set; }
        public Dictionary<int, string> SpellEffectRadius { get; set; }
        public Dictionary<int, string> SpellFamily { get; set; }
        public Dictionary<int, string> SpellDamageClass { get; set; }
        public Dictionary<int, string> SpellPowerType { get; set; }
        public Dictionary<int, string> ItemGroupSound { get; set; }
    }

    // get or set world game data.
    public static class LegacyWorld
    {
        private const int MAX_GOSSIP_ITEM = 32;
        #region GENERIC
        public static Dictionary<int, string> GetLanguages()
        {
            return DataDefine.Language;
        }

        public static Dictionary<int, string> GetEmotes()
        {
            return DataDefine.Emotes;
        }
        #endregion
        #region ITEM
        public static DataDefineStore GetDataDefines()
        {
            DataDefineStore store = new DataDefineStore();
            store.ItemAmmoType = DataDefine.ItemAmmoType;
            store.ItemBonding = DataDefine.ItemBonding;
            store.ItemDamageSchool = DataDefine.ItemDamageSchool;
            store.ItemInventoryType = DataDefine.ItemInventoryType;
            store.ItemQuality = DataDefine.ItemQuality;
            store.ItemSheath = DataDefine.ItemSheath;
            store.ItemSocketColor = DataDefine.ItemSocketColor;
            store.ItemSpellTrigger = DataDefine.ItemSpellTrigger;
            store.ItemStatType = DataDefine.ItemStatType;
            store.ReputationRank = DataDefine.ReputationRank;
            store.SkillLine = DataDefine.SkillLine;
            store.SpellDispel = DataDefine.SpellDispel;
            store.SpellIcon = DataDefine.SpellIcon;
            store.SpellMechanic = DataDefine.SpellMechanic;
            store.TotemCategory = DataDefine.TotemCategory;
            store.Language = DataDefine.Language;
            store.ItemClass = DataDefine.ItemClass;
            store.ItemBagFamily = DataDefine.ItemBagFamily;
            store.ItemPetFood = DataDefine.ItemPetFood;
            store.HolidayNames = DataDefine.HolidayNames;
            store.PageTextMaterial = DataDefine.PageTextMaterial;
            store.ItemMaterial = DataDefine.ItemMaterial;
            store.ItemSubclass = DataDefine.ItemSubclass;
            store.ItemSet = DataDefine.ItemSet;
            store.GossipIcon = DataDefine.GossipIcon;
            store.SpellDuration = DataDefine.SpellDuration;
            store.SpellCastTime = DataDefine.SpellCastTime;
            store.SpellRange = DataDefine.SpellRange;
            store.SpellEffect = DataDefine.SpellEffect;
            store.SpellAura = DataDefine.SpellAura;
            store.SpellEffectTarget = DataDefine.SpellEffectTarget;
            store.SpellAuraState = DataDefine.SpellAuraState;
            store.SpellEffectRadius = DataDefine.SpellEffectRadius;
            store.SpellFamily = DataDefine.SpellFamily;
            store.SpellDamageClass = DataDefine.SpellDamageClass;
            store.SpellPowerType = DataDefine.SpellPowerType;
            store.ItemGroupSound = DataDefine.ItemGroupSound;
            return store;
        }

        public static ItemTemplate GetItemTemplate(int entry)
        {
            var itemDB = (from d in DB.LEGACY.item_template where d.entry == entry select d).SingleOrDefault();
            if (itemDB == null)
                return null;
            else
            {
                ItemTemplate item = new ItemTemplate();
                item.Entry = itemDB.entry;
                item.Class = itemDB.@class;
                item.Subclass = itemDB.subclass;
                item.SoundOverrideSubclass = itemDB.SoundOverrideSubclass;
                item.Name = itemDB.name;
                item.DisplayID = itemDB.displayid;
                item.Quality = itemDB.Quality;
                item.Flags = itemDB.Flags;
                item.FlagsExtra = itemDB.FlagsExtra;
                item.BuyCount = itemDB.BuyCount;
                item.BuyPrice = itemDB.BuyPrice;
                item.SellPrice = itemDB.SellPrice;
                item.InventoryType = itemDB.InventoryType;
                item.AllowableClass = itemDB.AllowableClass;
                item.AllowableRace = itemDB.AllowableRace;
                item.ItemLevel = itemDB.ItemLevel;
                item.RequiredLevel = itemDB.RequiredLevel;
                item.RequiredSkill = itemDB.RequiredSkill;
                item.RequiredSkillRank = itemDB.RequiredSkillRank;
                item.RequiredCityRank = itemDB.RequiredCityRank;
                item.RequiredReputationFaction = itemDB.RequiredReputationFaction;
                item.RequiredReputationRank = itemDB.RequiredReputationRank;
                item.MaxCount = itemDB.maxcount;
                item.Stackable = itemDB.stackable;
                item.ContainerSlot = itemDB.ContainerSlots;
                item.StatsCount = itemDB.StatsCount;
                item.StatType[0] = itemDB.stat_type1;
                item.StatType[1] = itemDB.stat_type2;
                item.StatType[2] = itemDB.stat_type3;
                item.StatType[3] = itemDB.stat_type4;
                item.StatType[4] = itemDB.stat_type5;
                item.StatType[5] = itemDB.stat_type6;
                item.StatType[6] = itemDB.stat_type7;
                item.StatType[7] = itemDB.stat_type8;
                item.StatType[8] = itemDB.stat_type9;
                item.StatType[9] = itemDB.stat_type10;
                item.StatValue[0] = itemDB.stat_value1;
                item.StatValue[1] = itemDB.stat_value2;
                item.StatValue[2] = itemDB.stat_value3;
                item.StatValue[3] = itemDB.stat_value4;
                item.StatValue[4] = itemDB.stat_value5;
                item.StatValue[5] = itemDB.stat_value6;
                item.StatValue[6] = itemDB.stat_value7;
                item.StatValue[7] = itemDB.stat_value8;
                item.StatValue[8] = itemDB.stat_value9;
                item.StatValue[9] = itemDB.stat_value10;
                item.ScalingStatDistribution = itemDB.ScalingStatDistribution;
                item.ScalingStatValue = itemDB.ScalingStatValue;
                item.DamageMin[0] = itemDB.dmg_min1;
                item.DamageMin[1] = itemDB.dmg_min2;
                item.DamageMax[0] = itemDB.dmg_max1;
                item.DamageMax[1] = itemDB.dmg_max2;
                item.DamageSchool[0] = itemDB.dmg_type1;
                item.DamageSchool[1] = itemDB.dmg_type2;
                item.Armor = itemDB.armor;
                item.HolyResistance = itemDB.holy_res;
                item.FireResistance = itemDB.fire_res;
                item.NatureResistance = itemDB.nature_res;
                item.FrostResistance = itemDB.frost_res;
                item.ShadowResistance = itemDB.shadow_res;
                item.ArcaneResistance = itemDB.arcane_res;
                item.Speed = itemDB.delay;
                item.AmmoType = itemDB.ammo_type;
                item.RangedModRange = itemDB.RangedModRange;
                item.Spell[0] = itemDB.spellid_1;
                item.Spell[1] = itemDB.spellid_2;
                item.Spell[2] = itemDB.spellid_3;
                item.Spell[3] = itemDB.spellid_4;
                item.Spell[4] = itemDB.spellid_5;
                item.SpellTrigger[0] = itemDB.spelltrigger_1;
                item.SpellTrigger[1] = itemDB.spelltrigger_2;
                item.SpellTrigger[2] = itemDB.spelltrigger_3;
                item.SpellTrigger[3] = itemDB.spelltrigger_4;
                item.SpellTrigger[4] = itemDB.spelltrigger_5;
                item.SpellCharges[0] = itemDB.spellcharges_1;
                item.SpellCharges[1] = itemDB.spellcharges_2;
                item.SpellCharges[2] = itemDB.spellcharges_3;
                item.SpellCharges[3] = itemDB.spellcharges_4;
                item.SpellCharges[4] = itemDB.spellcharges_5;
                item.SpellPPM[0] = itemDB.spellppmRate_1;
                item.SpellPPM[1] = itemDB.spellppmRate_2;
                item.SpellPPM[2] = itemDB.spellppmRate_3;
                item.SpellPPM[3] = itemDB.spellppmRate_4;
                item.SpellPPM[4] = itemDB.spellppmRate_5;
                item.SpellCooldown[0] = itemDB.spellcooldown_1;
                item.SpellCooldown[1] = itemDB.spellcooldown_2;
                item.SpellCooldown[2] = itemDB.spellcooldown_3;
                item.SpellCooldown[3] = itemDB.spellcooldown_4;
                item.SpellCooldown[4] = itemDB.spellcooldown_5;
                item.SpellCategory[0] = itemDB.spellcategory_1;
                item.SpellCategory[1] = itemDB.spellcategory_2;
                item.SpellCategory[2] = itemDB.spellcategory_3;
                item.SpellCategory[3] = itemDB.spellcategory_4;
                item.SpellCategory[4] = itemDB.spellcategory_5;
                item.SpellCategoryCooldown[0] = itemDB.spellcategorycooldown_1;
                item.SpellCategoryCooldown[1] = itemDB.spellcategorycooldown_2;
                item.SpellCategoryCooldown[2] = itemDB.spellcategorycooldown_3;
                item.SpellCategoryCooldown[3] = itemDB.spellcategorycooldown_4;
                item.SpellCategoryCooldown[4] = itemDB.spellcategorycooldown_5;
                item.Bonding = itemDB.bonding;
                item.Description = itemDB.description;
                item.PageText = itemDB.PageText;
                item.LanguageID = itemDB.LanguageID;
                item.PageMaterial = itemDB.PageMaterial;
                item.StartQuest = itemDB.startquest;
                item.LockID = itemDB.lockid;
                item.Material = itemDB.Material;
                item.Sheath = itemDB.sheath;
                item.RandomProperty = itemDB.RandomProperty;
                item.RandomSuffix = itemDB.RandomSuffix;
                item.Block = itemDB.block;
                item.ItemSet = itemDB.itemset;
                item.MaxDurability = itemDB.MaxDurability;
                item.Area = itemDB.area;
                item.Map = itemDB.Map;
                item.BagFamily = itemDB.BagFamily;
                item.TotemCategory = itemDB.TotemCategory;
                item.SocketColor[0] = itemDB.socketColor_1;
                item.SocketColor[1] = itemDB.socketColor_2;
                item.SocketColor[2] = itemDB.socketColor_3;
                item.SocketContent[0] = itemDB.socketContent_1;
                item.SocketContent[1] = itemDB.socketContent_2;
                item.SocketContent[2] = itemDB.socketContent_3;
                item.SocketBonus = itemDB.socketBonus;
                item.GemProperty = itemDB.GemProperties;
                item.RequiredDisenchantSkill = itemDB.RequiredDisenchantSkill;
                item.ArmorDamageModifier = itemDB.ArmorDamageModifier;
                item.Duration = itemDB.duration;
                item.ItemLimitCategory = itemDB.ItemLimitCategory;
                item.HolidayID = itemDB.HolidayId;
                item.ScriptName = itemDB.ScriptName;
                item.DisenchantID = itemDB.DisenchantID;
                item.FoodType = itemDB.FoodType;
                item.MinMoneyLoot = itemDB.minMoneyLoot;
                item.MaxMoneyLoot = itemDB.maxMoneyLoot;
                item.CustomFlags = itemDB.flagsCustom;
                item.VerifiedBuild = itemDB.VerifiedBuild;
                return item;
            }
        }

        public static ItemTemplate SaveItemTemplate(ItemTemplate item, bool newItem = false)
        {
            if (item == null)
                return null;

            int maxid = (from d in DB.LEGACY.item_template select d.entry).Max() + 1;

            if (item.Entry == 0 || newItem)
            {
                item.Entry = maxid;
                DBCache.AddItem(item.Entry, item.Class, item.Subclass, item.Name, item.Quality);
            }
            else
            {
                var oldItem = (from d in DB.LEGACY.item_template where d.entry == item.Entry select d).SingleOrDefault();
                if (oldItem != null)
                    DB.LEGACY.item_template.Remove(oldItem);
                DBCache.UpdateItem(item.Entry, item.Class, item.Subclass, item.Name, item.Quality);
            }

            DB.LEGACY.item_template.Add(new item_template()
            {
                entry = item.Entry,
                @class = (byte)item.Class,
                subclass = (byte)item.Subclass,
                SoundOverrideSubclass = (sbyte)item.SoundOverrideSubclass,
                name = item.Name,
                displayid = item.DisplayID,
                Quality = (byte)item.Quality,
                Flags = item.Flags,
                FlagsExtra = item.FlagsExtra,
                BuyCount = item.BuyCount,
                BuyPrice = item.BuyPrice,
                SellPrice = item.SellPrice,
                InventoryType = (byte)item.InventoryType,
                AllowableClass = item.AllowableClass,
                AllowableRace = item.AllowableRace,
                ItemLevel = item.ItemLevel,
                RequiredLevel = item.RequiredLevel,
                RequiredSkill = item.RequiredSkill,
                RequiredSkillRank = item.RequiredSkillRank,
                RequiredCityRank = item.RequiredCityRank,
                RequiredReputationFaction = item.RequiredReputationFaction,
                RequiredReputationRank = item.RequiredReputationRank,
                maxcount = item.MaxCount,
                stackable = item.Stackable,
                ContainerSlots = item.ContainerSlot,
                StatsCount = item.StatsCount,
                stat_type1 = (byte)item.StatType[0],
                stat_type2 = (byte)item.StatType[1],
                stat_type3 = (byte)item.StatType[2],
                stat_type4 = (byte)item.StatType[3],
                stat_type5 = (byte)item.StatType[4],
                stat_type6 = (byte)item.StatType[5],
                stat_type7 = (byte)item.StatType[6],
                stat_type8 = (byte)item.StatType[7],
                stat_type9 = (byte)item.StatType[8],
                stat_type10 = (byte)item.StatType[9],
                stat_value1 = item.StatValue[0],
                stat_value2 = item.StatValue[1],
                stat_value3 = item.StatValue[2],
                stat_value4 = item.StatValue[3],
                stat_value5 = item.StatValue[4],
                stat_value6 = item.StatValue[5],
                stat_value7 = item.StatValue[6],
                stat_value8 = item.StatValue[7],
                stat_value9 = item.StatValue[8],
                stat_value10 = item.StatValue[9],
                ScalingStatDistribution = item.ScalingStatDistribution,
                ScalingStatValue = item.ScalingStatValue,
                dmg_min1 = item.DamageMin[0],
                dmg_min2 = item.DamageMin[1],
                dmg_max1 = item.DamageMax[0],
                dmg_max2 = item.DamageMax[1],
                dmg_type1 = (byte)item.DamageSchool[0],
                dmg_type2 = (byte)item.DamageSchool[1],
                armor = item.Armor,
                holy_res = item.HolyResistance,
                fire_res = item.FireResistance,
                nature_res = item.NatureResistance,
                frost_res = item.FrostResistance,
                shadow_res = item.ShadowResistance,
                arcane_res = item.ArcaneResistance,
                delay = item.Speed,
                ammo_type = (byte)item.AmmoType,
                RangedModRange = item.RangedModRange,
                spellid_1 = item.Spell[0],
                spellid_2 = item.Spell[1],
                spellid_3 = item.Spell[2],
                spellid_4 = item.Spell[3],
                spellid_5 = item.Spell[4],
                spelltrigger_1 = (byte)item.SpellTrigger[0],
                spelltrigger_2 = (byte)item.SpellTrigger[1],
                spelltrigger_3 = (byte)item.SpellTrigger[2],
                spelltrigger_4 = (byte)item.SpellTrigger[3],
                spelltrigger_5 = (byte)item.SpellTrigger[4],
                spellcharges_1 = item.SpellCharges[0],
                spellcharges_2 = item.SpellCharges[1],
                spellcharges_3 = item.SpellCharges[2],
                spellcharges_4 = item.SpellCharges[3],
                spellcharges_5 = item.SpellCharges[4],
                spellppmRate_1 = item.SpellPPM[0],
                spellppmRate_2 = item.SpellPPM[1],
                spellppmRate_3 = item.SpellPPM[2],
                spellppmRate_4 = item.SpellPPM[3],
                spellppmRate_5 = item.SpellPPM[4],
                spellcooldown_1 = item.SpellCooldown[0],
                spellcooldown_2 = item.SpellCooldown[1],
                spellcooldown_3 = item.SpellCooldown[2],
                spellcooldown_4 = item.SpellCooldown[3],
                spellcooldown_5 = item.SpellCooldown[4],
                spellcategory_1 = item.SpellCategory[0],
                spellcategory_2 = item.SpellCategory[1],
                spellcategory_3 = item.SpellCategory[2],
                spellcategory_4 = item.SpellCategory[3],
                spellcategory_5 = item.SpellCategory[4],
                spellcategorycooldown_1 = item.SpellCategoryCooldown[0],
                spellcategorycooldown_2 = item.SpellCategoryCooldown[1],
                spellcategorycooldown_3 = item.SpellCategoryCooldown[2],
                spellcategorycooldown_4 = item.SpellCategoryCooldown[3],
                spellcategorycooldown_5 = item.SpellCategoryCooldown[4],
                bonding = (byte)item.Bonding,
                description = item.Description,
                PageText = item.PageText,
                LanguageID = item.LanguageID,
                PageMaterial = (byte)item.PageMaterial,
                startquest = item.StartQuest,
                lockid = item.LockID,
                Material = (sbyte)item.Material,
                sheath = (byte)item.Sheath,
                RandomProperty = item.RandomProperty,
                RandomSuffix = item.RandomSuffix,
                block = item.Block,
                itemset = item.ItemSet,
                MaxDurability = item.MaxDurability,
                area = item.Area,
                Map = item.Map,
                BagFamily = item.BagFamily,
                TotemCategory = item.TotemCategory,
                socketColor_1 = (sbyte)item.SocketColor[0],
                socketColor_2 = (sbyte)item.SocketColor[1],
                socketColor_3 = (sbyte)item.SocketColor[2],
                socketContent_1 = item.SocketContent[0],
                socketContent_2 = item.SocketContent[1],
                socketContent_3 = item.SocketContent[2],
                socketBonus = item.SocketBonus,
                GemProperties = item.GemProperty,
                RequiredDisenchantSkill = item.RequiredDisenchantSkill,
                ArmorDamageModifier = item.ArmorDamageModifier,
                duration = item.Duration,
                ItemLimitCategory = item.ItemLimitCategory,
                HolidayId = item.HolidayID,
                ScriptName = item.ScriptName,
                DisenchantID = item.DisenchantID,
                FoodType = (byte)item.FoodType,
                minMoneyLoot = item.MinMoneyLoot,
                maxMoneyLoot = item.MaxMoneyLoot,
                flagsCustom = item.CustomFlags,
                VerifiedBuild = item.VerifiedBuild
            });
            DB.LEGACY.SaveChanges();
            DB.Refresh();
            return item;
        }

        public static List<ItemInfo> GetItemList()
        {
            return DBCache.GetItems();
        }

        public static ItemTemplate CreateItemTemplate()
        {
            return SaveItemTemplate(new ItemTemplate()
            {
                Entry = 0,
                Class = 0,
                Subclass = 0,
                SoundOverrideSubclass = 0,
                Name = "",
                DisplayID = 0,
                Quality = 0,
                Flags = 0,
                FlagsExtra = 0,
                BuyCount = 0,
                BuyPrice = 0,
                InventoryType = 0,
                AllowableClass = -1,
                AllowableRace = -1,
                ItemLevel = 0,
                RequiredLevel = 0,
                RequiredSkill = 0,
                RequiredSkillRank = 0,
                RequiredCityRank = 0,
                RequiredReputationFaction = 0,
                RequiredReputationRank = 0,
                MaxCount = 0,
                Stackable = 0,
                ContainerSlot = 0,
                StatsCount = 0,
                ScalingStatDistribution = 0,
                ScalingStatValue = 0,
                Armor = 0,
                HolyResistance = 0,
                FireResistance = 0,
                NatureResistance = 0,
                FrostResistance = 0,
                ShadowResistance = 0,
                ArcaneResistance = 0,
                Speed = 0,
                AmmoType = 0,
                RangedModRange = 0,
                Bonding = 0,
                Description = "",
                PageText = 0,
                LanguageID = 0,
                PageMaterial = 0,
                StartQuest = 0,
                LockID = 0,
                Material = 0,
                Sheath = 0,
                RandomProperty = 0,
                RandomSuffix = 0,
                Block = 0,
                ItemSet = 0,
                MaxDurability = 0,
                Area = 0,
                Map = 0,
                BagFamily = 0,
                TotemCategory = 0,
                SocketBonus = 0,
                GemProperty = 0,
                RequiredDisenchantSkill = 0,
                ArmorDamageModifier = 0,
                Duration = 0,
                ItemLimitCategory = 0,
                HolidayID = 0,
                ScriptName = "",
                DisenchantID = 0,
                FoodType = 0,
                MinMoneyLoot = 0,
                MaxMoneyLoot = 0,
                CustomFlags = 0,
                VerifiedBuild = 0,
            });
        }

        public static void DeleteItemTemplate(int entry)
        {
            var item = (from d in DB.LEGACY.item_template where d.entry == entry select d).SingleOrDefault();
            if (item != null)
            {
                DB.LEGACY.item_template.Remove(item);
                DB.LEGACY.SaveChanges();
                DBCache.DeleteItem(entry);
            }
        }

        public static ItemTemplate CopyItemTemplate(int entry)
        {
            ItemTemplate newItem = SaveItemTemplate(GetItemTemplate(entry), true);
            return newItem;
        }

        public static List<ItemDBC> GenerateItemDBC()
        {
            List<ItemDBC> list = new List<ItemDBC>();
            var items = from d in DB.LEGACY.item_template select d;
            foreach (var item in items)
                list.Add(new ItemDBC(item.entry, item.@class, item.subclass, item.SoundOverrideSubclass, item.Material, item.displayid, item.InventoryType, item.sheath));
            DB.Refresh();
            return list;
        }
        #endregion
        #region CREATURE GOSSIP
        public static Dictionary<int, string> GetGossipMenuOptionTypes()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            var types = from d in DB.DATA.define_menu_option_type select d;
            foreach (var type in types)
                dict.Add(type.ID, type.Name);
            return dict;
        }

        public static Dictionary<int, string> GetGossipIconDefines()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            var gossipIconDefines = from d in DB.DATA.define_gossip_icon select d;
            foreach (var define in gossipIconDefines)
                dict.Add(define.id, define.id + " - " + define.define);
            return dict;
        }

        public static Dictionary<int, string> GetCreatureNames()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            var creatureNames = from d in DB.LEGACY.creature_template select d;
            foreach (var creatureName in creatureNames)
                dict.Add(creatureName.entry, creatureName.name);
            DB.Refresh();
            return dict;
        }
        #endregion
        #region CREATURE VENDOR
        public static List<VendorInfo> GetVendorList(int entry)
        {
            List<VendorInfo> list = new List<VendorInfo>();
            var vendor = from d in DB.LEGACY.npc_vendor where d.entry == entry select d;
            foreach (var v in vendor)
            {
                list.Add(new VendorInfo()
                {
                    Slot = v.slot,
                    Item = v.item,
                    MaxCount = v.maxcount,
                    IncrTime = v.incrtime,
                    ExtendedCost = v.ExtendedCost,
                    VerifiedBuild = v.VerifiedBuild
                });
            }
            return list;
        }

        public static List<VendorInfo> GetVendorListAll()
        {
            List<VendorInfo> list = new List<VendorInfo>();
            var vendors = from d in DB.LEGACY.npc_vendor select d;
            foreach (var v in vendors)
            {
                list.Add(new VendorInfo()
                {
                    Slot = v.slot,
                    Item = v.item,
                    MaxCount = v.maxcount,
                    IncrTime = v.incrtime,
                    ExtendedCost = v.ExtendedCost,
                    VerifiedBuild = v.VerifiedBuild
                });
            }
            return list;
        }

        public static void AppendVendorInfo(int relatedItem, VendorInfo vendor)
        {
            var relation = (from d in DB.LEGACY.npc_vendor where d.item == relatedItem select d).ToList();
            if (relation.Count == 0) return;

            foreach (var rel in relation)
            {
                var oldVendor = (from d in DB.LEGACY.npc_vendor where d.entry == rel.entry && d.item == vendor.Item select d).SingleOrDefault();
                if (oldVendor != null)
                    DB.LEGACY.npc_vendor.Remove(oldVendor);

                DB.LEGACY.npc_vendor.Add(new npc_vendor()
                {
                    entry = rel.entry,
                    slot = vendor.Slot,
                    item = vendor.Item,
                    maxcount = vendor.MaxCount,
                    incrtime = vendor.IncrTime,
                    ExtendedCost = vendor.ExtendedCost,
                    VerifiedBuild = vendor.VerifiedBuild
                });
            }
            DB.LEGACY.SaveChanges();
        }

        public static bool SaveVendorList(int npcEntry, List<VendorInfo> list)
        {
            if (list.Count == 0)
            {
                var creature = (from d in DB.LEGACY.creature_template where d.entry == npcEntry select d).SingleOrDefault();
                if (creature != null)
                {
                    creature.npcflag ^= 128;
                    DB.LEGACY.SaveChanges();
                    return true;
                }
            }

            var npc = (from d in DB.LEGACY.creature_template where d.entry == npcEntry select d).SingleOrDefault();
            if (npc == null)
                return false;
            else
                npc.npcflag |= 128;

            var oldVendor = from d in DB.LEGACY.npc_vendor where d.entry == npcEntry select d;
            if (oldVendor.Count() != 0)
            {
                foreach (var v in oldVendor)
                    DB.LEGACY.npc_vendor.Remove(v);
            }

            foreach (var vendor in list)
            {
                DB.LEGACY.npc_vendor.Add(new npc_vendor()
                {
                    entry = npcEntry,
                    slot = vendor.Slot,
                    item = vendor.Item,
                    maxcount = vendor.MaxCount,
                    incrtime = vendor.IncrTime,
                    ExtendedCost = vendor.ExtendedCost,
                    VerifiedBuild = vendor.VerifiedBuild
                });
            }

            DB.LEGACY.SaveChanges();

            return true;
        }
        #endregion
        #region CREATURE TEMPLATE
        public static CreatureTemplate GetCreatureTemplate(int entry)
        {
            var c = (from d in DB.LEGACY.creature_template where d.entry == entry select d).SingleOrDefault();
            if (c == null)
                return null;

            CreatureTemplate creature = new CreatureTemplate();
            creature.Entry[0] = c.entry;
            creature.Entry[1] = c.difficulty_entry_1;
            creature.Entry[2] = c.difficulty_entry_2;
            creature.Entry[3] = c.difficulty_entry_3;
            creature.KillCredit[0] = c.KillCredit1;
            creature.KillCredit[1] = c.KillCredit2;
            creature.Model[0] = c.modelid1;
            creature.Model[1] = c.modelid2;
            creature.Model[2] = c.modelid3;
            creature.Model[3] = c.modelid4;
            creature.Name = c.name;
            creature.Subname = c.subname;
            creature.IconName = c.IconName;
            creature.GossipMenuID = c.gossip_menu_id;
            creature.MinLevel = c.minlevel;
            creature.MaxLevel = c.maxlevel;
            creature.Expansion = c.exp;
            creature.Faction = c.faction;
            creature.NpcFlags = c.npcflag;
            creature.SpeedWalk = c.speed_walk;
            creature.SpeedRun = c.speed_run;
            creature.Scale = c.scale;
            creature.Rank = c.rank;
            creature.DamageSchool = c.dmgschool;
            creature.BaseAttackTime = c.BaseAttackTime;
            creature.RangedAttackTime = c.RangeAttackTime;
            creature.BaseVariance = c.BaseVariance;
            creature.RangedVariance = c.RangeVariance;
            creature.UnitClass = c.unit_class;
            creature.UnitFlags = c.unit_flags;
            creature.UnitFlags2 = c.unit_flags2;
            creature.DynamicFlags = c.dynamicflags;
            creature.Family = c.family;
            creature.TrainerType = c.trainer_type;
            creature.TrainerSpell = c.trainer_spell;
            creature.TrainerClass = c.trainer_class;
            creature.TrainerRace = c.trainer_race;
            creature.Type = c.type;
            creature.TypeFlags = c.type_flags;
            creature.LootID = c.lootid;
            creature.PickpocketLoot = c.pickpocketloot;
            creature.SkinLoot = c.skinloot;
            creature.Resistance[0] = c.resistance1;
            creature.Resistance[1] = c.resistance2;
            creature.Resistance[2] = c.resistance3;
            creature.Resistance[3] = c.resistance4;
            creature.Resistance[4] = c.resistance5;
            creature.Resistance[5] = c.resistance6;
            creature.Spell[0] = c.spell1;
            creature.Spell[1] = c.spell2;
            creature.Spell[2] = c.spell3;
            creature.Spell[3] = c.spell4;
            creature.Spell[4] = c.spell5;
            creature.Spell[5] = c.spell6;
            creature.Spell[6] = c.spell7;
            creature.Spell[7] = c.spell8;
            creature.PetSpellDataID = c.PetSpellDataId;
            creature.VehicleID = c.VehicleId;
            creature.MinMoneyLoot = c.mingold;
            creature.MaxMoneyLoot = c.maxgold;
            creature.AIName = c.AIName;
            creature.MovementType = c.MovementType;
            creature.InhabitType = c.InhabitType;
            creature.HoverHeight = c.HoverHeight;
            creature.HealthModifier = c.HealthModifier;
            creature.ManaModifier = c.ManaModifier;
            creature.ArmorModifier = c.ArmorModifier;
            creature.DamageModifier = c.DamageModifier;
            creature.ExperienceModifier = c.ExperienceModifier;
            creature.IsRacialLeader = c.RacialLeader != 0 ? true : false;
            creature.QuestItem[0] = c.questItem1;
            creature.QuestItem[1] = c.questItem2;
            creature.QuestItem[2] = c.questItem3;
            creature.QuestItem[3] = c.questItem4;
            creature.QuestItem[4] = c.questItem5;
            creature.QuestItem[5] = c.questItem6;
            creature.MovementID = c.movementId;
            creature.RegenerateHealth = c.RegenHealth != 0 ? true : false;
            creature.MechanicImmuneMask = c.mechanic_immune_mask;
            creature.ExtraFlags = c.flags_extra;
            creature.ScriptName = c.ScriptName;
            creature.VerifiedBuild = c.VerifiedBuild;
            creature.WarSchool = c.WarSchool;
            DB.Refresh();
            return creature;
        }

        public static CreatureTemplate SaveCreatureTemplate(CreatureTemplate creature)
        {
            if (creature.Entry[0] != 0)
            {
                int entry = creature.Entry[0];
                var c = (from d in DB.LEGACY.creature_template where d.entry == entry select d).SingleOrDefault();

                if (c != null)
                    DB.LEGACY.creature_template.Remove(c);
            }
            else
                creature.Entry[0] = (from d in DB.LEGACY.creature_template select d.entry).Max() + 1;

            DBCache.UpdateCreature(creature.Entry[0], creature.Name, creature.MinLevel, creature.MaxLevel, creature.Rank);

            DB.LEGACY.creature_template.Add(new creature_template()
            {
                entry = creature.Entry[0],
                difficulty_entry_1 = creature.Entry[1],
                difficulty_entry_2 = creature.Entry[2],
                difficulty_entry_3 = creature.Entry[3],
                KillCredit1 = creature.KillCredit[0],
                KillCredit2 = creature.KillCredit[1],
                modelid1 = creature.Model[0],
                modelid2 = creature.Model[1],
                modelid3 = creature.Model[2],
                modelid4 = creature.Model[3],
                name = creature.Name,
                subname = creature.Subname,
                IconName = creature.IconName,
                gossip_menu_id = creature.GossipMenuID,
                minlevel = creature.MinLevel,
                maxlevel = creature.MaxLevel,
                exp = creature.Expansion,
                faction = creature.Faction,
                npcflag = creature.NpcFlags,
                speed_walk = creature.SpeedWalk,
                speed_run = creature.SpeedRun,
                scale = creature.Scale,
                rank = creature.Rank,
                dmgschool = creature.DamageSchool,
                BaseAttackTime = creature.BaseAttackTime,
                RangeAttackTime = creature.RangedAttackTime,
                BaseVariance = creature.BaseVariance,
                RangeVariance = creature.RangedVariance,
                unit_class = creature.UnitClass,
                unit_flags = creature.UnitFlags,
                unit_flags2 = creature.UnitFlags2,
                dynamicflags = creature.DynamicFlags,
                family = creature.Family,
                trainer_type = creature.TrainerType,
                trainer_spell = creature.TrainerSpell,
                trainer_class = creature.TrainerClass,
                trainer_race = creature.TrainerRace,
                type = creature.Type,
                type_flags = creature.TypeFlags,
                lootid = creature.LootID,
                pickpocketloot = creature.PickpocketLoot,
                skinloot = creature.SkinLoot,
                resistance1 = creature.Resistance[0],
                resistance2 = creature.Resistance[1],
                resistance3 = creature.Resistance[2],
                resistance4 = creature.Resistance[3],
                resistance5 = creature.Resistance[4],
                resistance6 = creature.Resistance[5],
                spell1 = creature.Spell[0],
                spell2 = creature.Spell[1],
                spell3 = creature.Spell[2],
                spell4 = creature.Spell[3],
                spell5 = creature.Spell[4],
                spell6 = creature.Spell[5],
                spell7 = creature.Spell[6],
                spell8 = creature.Spell[7],
                PetSpellDataId = creature.PetSpellDataID,
                VehicleId = creature.VehicleID,
                mingold = creature.MinMoneyLoot,
                maxgold = creature.MaxMoneyLoot,
                AIName = creature.AIName,
                MovementType = creature.MovementType,
                InhabitType = creature.InhabitType,
                HoverHeight = creature.HoverHeight,
                HealthModifier = creature.HealthModifier,
                ManaModifier = creature.ManaModifier,
                ArmorModifier = creature.ArmorModifier,
                DamageModifier = creature.DamageModifier,
                ExperienceModifier = creature.ExperienceModifier,
                RacialLeader = creature.IsRacialLeader == true ? (byte)1 : (byte)0,
                questItem1 = creature.QuestItem[0],
                questItem2 = creature.QuestItem[1],
                questItem3 = creature.QuestItem[2],
                questItem4 = creature.QuestItem[3],
                questItem5 = creature.QuestItem[4],
                questItem6 = creature.QuestItem[5],
                movementId = creature.MovementID,
                RegenHealth = creature.RegenerateHealth == true ? (byte)1 : (byte)0,
                mechanic_immune_mask = creature.MechanicImmuneMask,
                flags_extra = creature.ExtraFlags,
                ScriptName = creature.ScriptName,
                VerifiedBuild = creature.VerifiedBuild,
                WarSchool = creature.WarSchool
            });

            DB.LEGACY.SaveChanges();

            return creature;
        }

        public static List<CreatureInfo> GetCreatureList()
        {
            return DBCache.GetCreatures();
        }

        public static void DeleteCreatureTemplate(int entry)
        {
            var creature = (from d in DB.LEGACY.creature_template where d.entry == entry select d).SingleOrDefault();
            if (creature != null)
            {
                DB.LEGACY.creature_template.Remove(creature);
                DB.LEGACY.SaveChanges();
                DBCache.DeleteCreature(entry);
            }
        }

        public static List<Creature> GetSpawnInfo(int entry)
        {
            List<Creature> list = new List<Creature>();

            var creatures = from d in DB.LEGACY.creature where d.id == entry select d;
            if (creatures.Count() == 0)
                return list;

            foreach (var c in creatures)
                list.Add(new Creature()
                {
                    Guid = c.guid,
                    Map = c.map,
                    Zone = c.zoneId,
                    Area = c.areaId,
                    SpawnMask = c.spawnMask,
                    PhaseMask = c.phaseMask,
                    Model = c.modelid,
                    EquipmentID = c.equipment_id,
                    PositionX = c.position_x,
                    PositionY = c.position_y,
                    PositionZ = c.position_z,
                    Orientation = c.orientation,
                    SpawnTime = c.spawntimesecs,
                    SpawnDistance = c.spawndist,
                    CurrentWaypoint = c.currentwaypoint,
                    CurrentHealth = c.curhealth,
                    CurrentMana = c.curmana,
                    MovementType = c.MovementType,
                    NpcFlags = c.npcflag,
                    UnitFlags = c.unit_flags,
                    DynamicFlags = c.dynamicflags,
                    VerifiedBuild = c.VerifiedBuild
                });
            return list;
        }

        public static void SaveSpawnInfo(int entry, List<Creature> list)
        {
            if (list == null || list.Count == 0)
                return;

            var oldSpawns = from d in DB.LEGACY.creature where d.id == entry select d;
            if (oldSpawns.Count() != 0)
            {
                foreach (var spawn in oldSpawns)
                    DB.LEGACY.creature.Remove(spawn);
            }

            if (list.Count != 0)
            {
                foreach (var spawn in list)
                {
                    DB.LEGACY.creature.Add(new creature()
                    {
                        guid = spawn.Guid,
                        id = entry,
                        map = spawn.Map,
                        zoneId = spawn.Zone,
                        areaId = spawn.Area,
                        spawnMask = spawn.SpawnMask,
                        phaseMask = spawn.PhaseMask,
                        modelid = spawn.Model,
                        equipment_id = spawn.EquipmentID,
                        position_x = spawn.PositionX,
                        position_y = spawn.PositionY,
                        position_z = spawn.PositionZ,
                        orientation = spawn.Orientation,
                        spawntimesecs = spawn.SpawnTime,
                        spawndist = spawn.SpawnDistance,
                        currentwaypoint = spawn.CurrentWaypoint,
                        curhealth = spawn.CurrentHealth,
                        curmana = spawn.CurrentMana,
                        MovementType = spawn.MovementType,
                        npcflag = spawn.NpcFlags,
                        unit_flags = spawn.UnitFlags,
                        dynamicflags = spawn.DynamicFlags,
                        VerifiedBuild = spawn.VerifiedBuild
                    });
                }
            }

            DB.LEGACY.SaveChanges();
        }
        #endregion
        #region CREATURE TRAINER
        public static List<NpcTrainerInfo> GetCreatureTrainerInfo(int entry)
        {
            List<NpcTrainerInfo> list = new List<NpcTrainerInfo>();
            var trainerInfo = from d in DB.LEGACY.npc_trainer where d.entry == entry select d;
            if (trainerInfo.Count() == 0)
                return list;
            foreach (var info in trainerInfo)
            {
                NpcTrainerInfo cti = new NpcTrainerInfo();
                cti.Cost = info.spellcost;
                cti.Entry = info.entry;
                cti.Level = info.reqlevel;
                cti.Skill = info.reqskill;
                cti.SkillValue = info.reqskillvalue;
                cti.Spell = info.spell;
                cti.CityRank = info.reqcityrank;
                list.Add(cti);
            }

            List<NpcTrainerInfo> list2 = new List<NpcTrainerInfo>();
            foreach (var item in list)
            {
                if (item.Spell < 0)
                {
                    int realentry = -item.Spell;
                    var realInfo = from d in DB.LEGACY.npc_trainer where d.entry == realentry select d;
                    foreach (var rInfo in realInfo)
                    {
                        NpcTrainerInfo cti2 = new NpcTrainerInfo();
                        cti2.Entry = rInfo.entry;
                        cti2.Cost = rInfo.spellcost;
                        cti2.Level = rInfo.reqlevel;
                        cti2.Skill = rInfo.reqskill;
                        cti2.SkillValue = rInfo.reqskillvalue;
                        cti2.Spell = rInfo.spell;
                        cti2.CityRank = rInfo.reqcityrank;
                        list2.Add(cti2);
                    }
                }
            }

            if (list2.Count != 0)
            {
                foreach (var item in list2)
                    list.Add(item);
            }

            return list;
        }

        public static void SaveCreatureTrainerInfo(List<NpcTrainerInfo> list)
        {
            if (list == null || list.Count == 0)
                return;

            foreach (var info in list)
            {
                var old = from d in DB.LEGACY.npc_trainer where d.entry == info.Entry select d;
                if (old.Count() != 0)
                {
                    foreach (var o in old)
                        DB.LEGACY.npc_trainer.Remove(o);
                }
                DB.LEGACY.npc_trainer.Add(new npc_trainer()
                {
                    entry = info.Entry,
                    spell = info.Spell,
                    spellcost = info.Cost,
                    reqskill = info.Skill,
                    reqskillvalue = info.SkillValue,
                    reqlevel = info.Level,
                    reqcityrank = info.CityRank
                });
            }

            DB.LEGACY.SaveChanges();
        }
        #endregion
        #region CREATURE LOOT
        public static List<Loot> GetCreatureLoot(int entry, bool onlyRef)
        {
            List<Loot> list = new List<Loot>();
            if (onlyRef)
            {
                var refloots = from d in DB.LEGACY.reference_loot_template where d.Entry == entry select d;
                foreach (var refloot in refloots)
                {
                    Loot lo = new Loot();
                    lo.Entry = refloot.Entry;
                    lo.Item = refloot.Item;
                    lo.Reference = refloot.Reference;
                    lo.Chance = refloot.Chance;
                    lo.QuestRequired = refloot.QuestRequired;
                    lo.LootMode = refloot.LootMode;
                    lo.GroupID = refloot.GroupId;
                    lo.MinCount = refloot.MinCount;
                    lo.MaxCount = refloot.MaxCount;
                    lo.Comment = refloot.Comment;
                    lo.IsRef = true;
                    list.Add(lo);
                }
                return list;
            }
            else
            {
                var loots = from d in DB.LEGACY.creature_loot_template where d.Entry == entry select d;
                foreach (var loot in loots)
                {
                    Loot l = new Loot();
                    l.Entry = loot.Entry;
                    l.Item = loot.Item;
                    l.Reference = loot.Reference;
                    l.Chance = loot.Chance;
                    l.QuestRequired = loot.QuestRequired;
                    l.LootMode = loot.LootMode;
                    l.GroupID = loot.GroupId;
                    l.MinCount = loot.MinCount;
                    l.MaxCount = loot.MaxCount;
                    l.Comment = loot.Comment;
                    l.IsRef = false;
                    list.Add(l);
                }

                List<Loot> refList = new List<Loot>();
                foreach (var l in list)
                {
                    if (l.Reference != 0)
                    {
                        var refloots = from d in DB.LEGACY.reference_loot_template where d.Entry == l.Reference select d;
                        foreach (var refloot in refloots)
                        {
                            Loot lo = new Loot();
                            lo.Entry = refloot.Entry;
                            lo.Item = refloot.Item;
                            lo.Reference = refloot.Reference;
                            lo.Chance = refloot.Chance;
                            lo.QuestRequired = refloot.QuestRequired;
                            lo.LootMode = refloot.LootMode;
                            lo.GroupID = refloot.GroupId;
                            lo.MinCount = refloot.MinCount;
                            lo.MaxCount = refloot.MaxCount;
                            lo.Comment = refloot.Comment;
                            lo.IsRef = true;
                            refList.Add(lo);
                        }
                    }
                }

                foreach (var lo in refList)
                    list.Add(lo);

                return list;
            }
        }

        public static void SaveCreatureLoot(List<Loot> list)
        {
            var creatureLoots = from d in list where d.IsRef == false select d;
            foreach (var creatureLoot in creatureLoots)
            {
                var oldLoot = from d in DB.LEGACY.creature_loot_template where d.Entry == creatureLoot.Entry && d.Item == creatureLoot.Item select d;
                foreach (var ol in oldLoot)
                    DB.LEGACY.creature_loot_template.Remove(ol);
                DB.LEGACY.creature_loot_template.Add(new creature_loot_template()
                {
                    Entry = creatureLoot.Entry,
                    Item = creatureLoot.Item,
                    Reference = creatureLoot.Reference,
                    Chance = creatureLoot.Chance,
                    QuestRequired = creatureLoot.QuestRequired,
                    LootMode = creatureLoot.LootMode,
                    GroupId = creatureLoot.GroupID,
                    MinCount = creatureLoot.MinCount,
                    MaxCount = creatureLoot.MaxCount,
                    Comment = creatureLoot.Comment,
                });
            }
            var refLoots = from d in list where d.IsRef == true select d;
            foreach (var refLoot in refLoots)
            {
                var oldLoot = from d in DB.LEGACY.reference_loot_template where d.Entry == refLoot.Entry && d.Item == refLoot.Item select d;
                foreach (var ol in oldLoot)
                    DB.LEGACY.reference_loot_template.Remove(ol);
                DB.LEGACY.reference_loot_template.Add(new reference_loot_template()
                {
                    Entry = refLoot.Entry,
                    Item = refLoot.Item,
                    Reference = refLoot.Reference,
                    Chance = refLoot.Chance,
                    QuestRequired = refLoot.QuestRequired,
                    LootMode = refLoot.LootMode,
                    GroupId = refLoot.GroupID,
                    MinCount = refLoot.MinCount,
                    MaxCount = refLoot.MaxCount,
                    Comment = refLoot.Comment,
                });
            }
            DB.LEGACY.SaveChanges();
            DB.Refresh();
        }
        #endregion
        #region SMARTSCRIPT
        public static List<SmartScript> GetSmartScript(int type, int entry)
        {
            List<SmartScript> list = new List<SmartScript>();
            var scripts = from d in DB.LEGACY.smart_scripts where d.source_type == type && d.entryorguid == entry select d;
            if (scripts.Count() == 0)
                return list;

            foreach (var script in scripts)
            {
                SmartScript smartScript = new SmartScript();
                smartScript.Entry = script.entryorguid;
                smartScript.SourceType = script.source_type;
                smartScript.ID = script.id;
                smartScript.Link = script.link;
                smartScript.Event = script.event_type;
                smartScript.Chance = script.event_chance;
                smartScript.EventPhase = script.event_phase_mask;
                smartScript.EventFlags = script.event_flags;
                smartScript.EventParam[0] = script.event_param1;
                smartScript.EventParam[1] = script.event_param2;
                smartScript.EventParam[2] = script.event_param3;
                smartScript.EventParam[3] = script.event_param4;
                smartScript.Action = script.action_type;
                smartScript.ActionParam[0] = script.action_param1;
                smartScript.ActionParam[1] = script.action_param2;
                smartScript.ActionParam[2] = script.action_param3;
                smartScript.ActionParam[3] = script.action_param4;
                smartScript.ActionParam[4] = script.action_param5;
                smartScript.ActionParam[5] = script.action_param6;
                smartScript.Target = script.target_type;
                smartScript.TargetParam[0] = script.target_param1;
                smartScript.TargetParam[1] = script.target_param2;
                smartScript.TargetParam[2] = script.target_param3;
                smartScript.TargetPosition[0] = script.target_x;
                smartScript.TargetPosition[1] = script.target_y;
                smartScript.TargetPosition[2] = script.target_z;
                smartScript.TargetPosition[3] = script.target_o;
                smartScript.Comment = script.comment;
                list.Add(smartScript);
            }

            return list;
        }

        public static void SaveSmartScript(List<SmartScript> list)
        {
            //var oldScripts = from d in DB.LEGACY.smart_scripts where d.source_type == type && d.entryorguid == entry select d;
            //if (oldScripts.Count() != 0)
            //{
            //    foreach (var script in oldScripts)
            //        DB.LEGACY.smart_scripts.Remove(script);
            //}
            foreach (var script in list)
            {
                DB.LEGACY.smart_scripts.Add(new smart_scripts()
                {
                    entryorguid = script.Entry,
                    source_type = script.SourceType,
                    id = script.ID,
                    link = script.Link,
                    event_type = script.Event,
                    event_phase_mask = script.EventPhase,
                    event_chance = script.Chance,
                    event_flags = script.EventFlags,
                    event_param1 = script.EventParam[0],
                    event_param2 = script.EventParam[1],
                    event_param3 = script.EventParam[2],
                    event_param4 = script.EventParam[3],
                    action_type = script.Action,
                    action_param1 = script.ActionParam[0],
                    action_param2 = script.ActionParam[1],
                    action_param3 = script.ActionParam[2],
                    action_param4 = script.ActionParam[3],
                    action_param5 = script.ActionParam[4],
                    action_param6 = script.ActionParam[5],
                    target_type = script.Target,
                    target_param1 = script.TargetParam[0],
                    target_param2 = script.TargetParam[1],
                    target_param3 = script.TargetParam[2],
                    target_x = script.TargetPosition[0],
                    target_y = script.TargetPosition[1],
                    target_z = script.TargetPosition[2],
                    target_o = script.TargetPosition[3],
                    comment = script.Comment
                });
            }
        }
        #endregion

        public static List<ItemEnchantmentTemplate> GetItemEnchants()
        {
            List<ItemEnchantmentTemplate> list = new List<ItemEnchantmentTemplate>();
            var enchants = from d in DB.LEGACY.item_enchantment_template select d;
            foreach (var enchant in enchants)
            {
                ItemEnchantmentTemplate ench = new ItemEnchantmentTemplate();
                ench.ID = enchant.entry;
                ench.Enchant = enchant.ench;
                ench.Chance = enchant.chance;
                list.Add(ench);
            }
            return list;
        }

        public static void SaveItemEnchants(List<ItemEnchantmentTemplate> list)
        {
            var olds = from d in DB.LEGACY.item_enchantment_template select d;
            foreach (var old in olds)
                DB.LEGACY.item_enchantment_template.Remove(old);

            foreach (var _new in list)
            {
                DB.LEGACY.item_enchantment_template.Add(new item_enchantment_template()
                {
                    entry = _new.ID,
                    ench = _new.Enchant,
                    chance = _new.Chance
                });
            }

            DB.LEGACY.SaveChanges();
        }

        public static BroadCastText GetBroadCastText(int entry)
        {
            var bct = (from d in DB.LEGACY.broadcast_text where d.ID == entry select d).SingleOrDefault();
            if (bct == null)
                return null;

            BroadCastText text = new BroadCastText();
            text.ID = bct.ID;
            text.Language = bct.Language;
            text.MaleText = bct.MaleText;
            text.FemaleText = bct.FemaleText;
            text.Emote0 = bct.EmoteID0;
            text.Emote1 = bct.EmoteID1;
            text.Emote2 = bct.EmoteID2;
            text.EmoteDelay0 = bct.EmoteDelay0;
            text.EmoteDelay1 = bct.EmoteDelay1;
            text.EmoteDelay2 = bct.EmoteDelay2;
            text.SoundID = bct.SoundId;
            return text;
        }

        public static void SaveBroadCastText(BroadCastText text, bool createNew = true)
        {
            int maxID = (from d in DB.LEGACY.broadcast_text select d.ID).Max() + 1;

            if (createNew)
                text.ID = maxID;
            else
            {
                var oldEntry = (from d in DB.LEGACY.broadcast_text where d.ID == text.ID select d).SingleOrDefault();
                if (oldEntry != null)
                    DB.LEGACY.broadcast_text.Remove(oldEntry);
            }

            DB.LEGACY.broadcast_text.Add(new broadcast_text()
            {
                ID = text.ID,
                Language = text.Language,
                MaleText = text.MaleText,
                FemaleText = text.FemaleText,
                EmoteID0 = text.Emote0,
                EmoteID1 = text.Emote1,
                EmoteID2 = text.Emote2,
                EmoteDelay0 = text.EmoteDelay0,
                EmoteDelay1 = text.EmoteDelay1,
                EmoteDelay2 = text.EmoteDelay2,
                Unk1 = 0,
                Unk2 = 1,
                VerifiedBuild = 10000
            });

            DB.LEGACY.SaveChanges();
        }

        public static List<GossipMenu> GetGossipMenu(int entry)
        {
            List<GossipMenu> menu = new List<GossipMenu>();
            var gossips = from d in DB.LEGACY.gossip_menu where d.entry == entry select d;
            foreach (var gossip in gossips)
            {
                GossipMenu m = new GossipMenu();
                m.Menu = gossip.entry;
                m.NpcText = gossip.text_id;
            }
            return menu;
        }

        public static void SaveGossipMenu(List<GossipMenu> menu, int menuID)
        {
            if (menu == null || menu.Count == 0)
                return;

            var oldMenus = from d in DB.LEGACY.gossip_menu where d.entry == menuID select d;
            if (oldMenus.Count() > 0)
            {
                foreach (var oldMenu in oldMenus)
                    DB.LEGACY.gossip_menu.Remove(oldMenu);
            }

            foreach (var m in menu)
            {
                DB.LEGACY.gossip_menu.Add(new gossip_menu()
                {
                    entry = menuID,
                    text_id = m.NpcText
                });
            }

            DB.LEGACY.SaveChanges();
        }

        public static List<GossipMenuItem> GetGossipMenuItems(int menuID)
        {
            List<GossipMenuItem> menuItems = new List<GossipMenuItem>();
            var items = from d in DB.LEGACY.gossip_menu_option where d.menu_id == menuID select d;
            if (items.Count() != 0)
            {
                foreach (var item in items)
                {
                    GossipMenuItem it = new GossipMenuItem();
                    it.Menu = item.menu_id;
                    it.ID = item.id;
                    it.Icon = item.option_icon;
                    it.GossipTextID = item.OptionBroadcastTextID;
                    it.OptionID = item.option_id;
                    it.NpcFlags = item.npc_option_npcflag;
                    it.ToMenu = item.action_menu_id;
                    it.POI = item.action_poi_id;
                    it.BoxCoded = item.box_coded != 0;
                    it.BoxMoney = item.box_money;
                    it.BoxTextID = item.BoxBroadcastTextID;
                    it.SingleTimeCheck = item.SingleTimeCheck != 0;
                }
            }
            return menuItems;
        }

        public static void SaveGossipMenuItems(List<GossipMenuItem> menuItems, int menuID)
        {
            if (menuItems == null || menuItems.Count == 0)
                return;

            var oldMenuItems = from d in DB.LEGACY.gossip_menu_option where d.menu_id == menuID select d;
            if (oldMenuItems.Count() > 0)
            {
                foreach (var oldMenuItem in oldMenuItems)
                    DB.LEGACY.gossip_menu_option.Remove(oldMenuItem);
            }

            foreach (var m in menuItems)
            {
                DB.LEGACY.gossip_menu_option.Add(new gossip_menu_option()
                {
                    menu_id = menuID,
                    id = m.ID,
                    option_icon = m.Icon,
                    OptionBroadcastTextID = m.GossipTextID,
                    option_id = m.OptionID,
                    npc_option_npcflag = m.NpcFlags,
                    action_menu_id = m.ToMenu,
                    action_poi_id = m.POI,
                    box_coded = (byte)(m.BoxCoded ? 1 : 0),
                    box_money = m.BoxMoney,
                    BoxBroadcastTextID = m.BoxTextID,
                    SingleTimeCheck = (byte)(m.SingleTimeCheck ? 1 : 0)
                });
            }
            DB.LEGACY.SaveChanges();
        }
    }
}
