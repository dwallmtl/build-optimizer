﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POEApi.Model;

namespace Procurement.ViewModel
{
    class ItemHoverViewModelFactory
    {
        internal static ItemHoverViewModel Create(Item item)
        {
            Gear gear = item as Gear;
            Nullable<Rarity> r = null;

            if (gear != null)
                r = gear.Rarity;

            Map map = item as Map;
            if (map != null)
                r = map.Rarity;

            if (r != null)
            {
                switch (r)
                {
                    case Rarity.Unique:
                        return new UniqueGearItemHoverViewModel(item);
                    case Rarity.Rare:
                        return new RareGearItemHoverViewModel(item);
                    case Rarity.Magic:
                        return new MagicGearItemHoverViewModel(item);
                    case Rarity.Normal:
                        return new NormalGearItemHoverViewModel(item);
                }
            }

            if (item is Gem)
                return new GemItemHoverViewModel(item);

            if (item is Currency)
                return new CurrencyItemHoverViewModel(item);

            return new ItemHoverViewModel(item);
        }
    }

    public class UniqueGearItemHoverViewModel : ItemHoverViewModel
    {
        public UniqueGearItemHoverViewModel(Item item)
            : base(item)
        { }
    }

    public class RareGearItemHoverViewModel : ItemHoverViewModel
    {
        public RareGearItemHoverViewModel(Item item)
            : base(item)
        { }
    }

    public class MagicGearItemHoverViewModel : ItemHoverViewModel
    {
        public MagicGearItemHoverViewModel(Item item)
            : base(item)
        { }
    }

    public class NormalGearItemHoverViewModel : ItemHoverViewModel
    {
        public NormalGearItemHoverViewModel(Item item)
            : base(item)
        { }
    }

    public class GemItemHoverViewModel : ItemHoverViewModel
    {
        public GemItemHoverViewModel(Item item)
            : base(item)
        { }
    }

    public class CurrencyItemHoverViewModel : ItemHoverViewModel
    {
        public CurrencyItemHoverViewModel(Item item)
            : base(item)
        { }
    }
}
