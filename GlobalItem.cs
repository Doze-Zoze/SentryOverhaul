using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using System;
using CalamityMod.Items.Weapons.Summon;

namespace SentryOverhaul;

public class SentryGlobalItem : GlobalItem
{ 
    public override void SetDefaults(Item item)
    {
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        HashSet<int> SentryDmgFixSet = new HashSet<int>();
        if (item.accessory)
        {
            item.canBePlacedInVanityRegardlessOfConditions = true;
        }
        if (item.DamageType == DamageClass.Summon)
        {
            if (item.sentry == true)
            {
                item.DamageType = SentryDamageClass.Instance;
            } else if (item.DamageType != DamageClass.SummonMeleeSpeed)
            {
                item.DamageType = MinionDamageClass.Instance;
            }
        }
        
        if (item.sentry == true)
        {
            item.autoReuse = true;
        }
        switch (item.type)
        { 
            case ItemID.MoonlordTurretStaff:
                item.damage = 75;
                break;
            case ItemID.DD2LightningAuraT1Popper:
                item.damage = 9;
                item.useTime = 10;
                item.useAnimation = 10;
                item.mana = 2;
                item.knockBack = 1;
                break;
            case ItemID.DD2LightningAuraT2Popper:
                item.damage = 25;
                item.useTime = 10;
                item.useAnimation = 10;
                item.mana = 4;
                item.knockBack = 1;
                break;
            case ItemID.DD2LightningAuraT3Popper:
                item.damage = 64;
                item.useTime = 10;
                item.useAnimation = 10;
                item.mana = 8;
                item.knockBack = 1;
                break;
            case ItemID.DD2FlameburstTowerT1Popper:
                item.damage = 25;
                break;
            case ItemID.DD2FlameburstTowerT2Popper:
                item.damage = 60;
                item.ArmorPenetration = 10;
                break;
            case ItemID.DD2FlameburstTowerT3Popper:
                item.ArmorPenetration = 10;
                item.damage = 111;
                break;
            case ItemID.DD2BallistraTowerT1Popper:
                item.damage = 35;
                item.mana = 10;
                item.useTime = 45;
                item.useAnimation = 45;
                break;
            case ItemID.DD2BallistraTowerT2Popper:
                item.damage = 105;
                item.mana = 20;
                item.useTime = 45;
                item.useAnimation = 45;
                break;
            case ItemID.DD2BallistraTowerT3Popper:
                item.damage = 200;
                item.mana = 30;
                item.useTime = 45;
                item.useAnimation = 45;
                break;
            case ItemID.DD2ExplosiveTrapT1Popper:
                item.knockBack = 1;
                break;
            case ItemID.DD2ExplosiveTrapT2Popper:
                item.knockBack = 1;
                item.damage = 75;
                break;
            case ItemID.DD2ExplosiveTrapT3Popper:
                item.damage = 140;
                item.knockBack = 1;
                break;
            case ItemID.StaffoftheFrostHydra:
                item.damage = 100;
                item.ArmorPenetration = 30;
                item.useTime = 15;
                item.useAnimation = 15;
                break;
            case ItemID.QueenSpiderStaff:
                item.damage = 35;
                item.ArmorPenetration = 20;
                item.useTime = 15;
                item.useAnimation = 15;
                break;
            case ItemID.HoundiusShootius:
                item.damage = 30;
                item.ArmorPenetration = 20;
                item.useTime = 15;
                item.useAnimation = 15;
                break;
        }
        if (calamityMod != null)
        {
            switch (item.type)
            {
                case ItemID.DD2FlameburstTowerT1Popper:
                    item.damage = 23;
                    break;
            }
            sentryDamageFixStr(calamityMod,"PolypLauncher");
            if (item.type == calamityMod.Find<ModItem>("EnergyStaff").Type)
            {
                item.damage = 135;
            }
            if (item.type == calamityMod.Find<ModItem>("GuidelightofOblivion").Type)
            {
                item.damage = 188;
            }
            if (item.type == calamityMod.Find<ModItem>("CadaverousCarrion").Type)
            {
                item.damage = 350;
            }
            if (item.type == calamityMod.Find<ModItem>("Perdition").Type)
            {
                item.damage = 444;
            }
            if (item.type == calamityMod.Find<ModItem>("AtlasMunitionsBeacon").Type)
            {
                item.damage = 666;
            }
            if (item.type == calamityMod.Find<ModItem>("PolypLauncher").Type)
            {
                item.sentry = true;
            }
            if (item.type == calamityMod.Find<ModItem>("CryogenicStaff").Type)
            {
                item.damage = 20;
            }
            if (item.type == calamityMod.Find<ModItem>("HivePod").Type)
            {
                item.damage = 35;
            }

        }
        if (SentryDmgFixSet.Contains(item.type))
        {
            item.DamageType = SentryDamageClass.Instance;
        }
        void sentryDamageFixStr(Mod mod, string proj)
        {
            SentryDmgFixSet.Add(mod.Find<ModItem>(proj).Type);
        }
    }
    public override void UpdateEquip(Item item, Player player)
    {
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        switch (item.type)
        {
            case ItemID.FlinxFurCoat:
                player.maxTurrets++;
                player.GetDamage<SentryDamageClass>().Flat += 3;
                break;
            case ItemID.ObsidianShirt:
                player.maxTurrets += 2;
                break;
            case ItemID.AncientBattleArmorHat:
                player.GetDamage<SummonDamageClass>() -= 0.15f;
                player.GetDamage<SentryDamageClass>() += 0.15f;
                player.maxTurrets += 1;
                break;
            case ItemID.AncientBattleArmorShirt:
                player.GetDamage<SummonDamageClass>() -= 0.1f;
                player.GetDamage<SentryDamageClass>() += 0.150f;
                player.maxTurrets += 1;
                player.maxMinions -= 1;
                player.manaFlower = true;
                break;
            case ItemID.AncientBattleArmorPants:
                if (calamityMod != null) {
                    player.maxTurrets++;
                } else
                {
                    player.maxTurrets += 2;
                }
                player.maxMinions -= 1;
                break;
            case ItemID.StardustHelmet:
                player.maxTurrets++;
                break;
            case ItemID.StardustBreastplate:
                player.whipRangeMultiplier += 0.15f;
                break;
            case ItemID.StardustLeggings:
                player.whipRangeMultiplier += 0.15f;
                break;
            case ItemID.ApprenticeAltHead:
            case ItemID.SquirePlating:
            case ItemID.SquireGreaves:
                player.GetDamage<SummonDamageClass>() -= 0.15f;
                player.GetDamage<SentryDamageClass>() += 0.15f;
                break;
            case ItemID.ApprenticeAltPants:
            case ItemID.MonkAltHead:
            case ItemID.MonkAltPants:
            case ItemID.MonkAltShirt:
            case ItemID.SquireAltPants:
            case ItemID.HuntressJerkin:
            case ItemID.ApprenticeRobe:
            case ItemID.MonkShirt:
                player.GetDamage<SummonDamageClass>() -= 0.2f;
                player.GetDamage<SentryDamageClass>() += 0.2f;
                break;
            case ItemID.MonkPants:
            case ItemID.HuntressPants:
            case ItemID.ApprenticeTrousers:
                player.GetDamage<SummonDamageClass>() -= 0.1f;
                player.GetDamage<SentryDamageClass>() += 0.1f;
                break;
        }
    }
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        switch (item.type)
        {
            case ItemID.HuntressBuckler:
            case ItemID.SquireShield:
            case ItemID.ApprenticeScarf:
            case ItemID.MonkBelt:
                ChangeToSentryDmg(0.1f);
                player.GetDamage<SentryDamageClass>() += 0.05f;
                break;
            case ItemID.PapyrusScarab:
                ChangeToMinionDmg(0.15f);
                player.GetDamage<SummonMeleeSpeedDamageClass>() += 0.15f;
                break;
            case ItemID.NecromanticScroll:
                ChangeToMinionDmg(0.1f);
                player.GetDamage<SummonMeleeSpeedDamageClass>() += 0.1f;
                break;
            case ItemID.PygmyNecklace:
                player.maxTurrets++;
                break;
            /*case ItemID.PanicNecklace:
                player.setSquireT2 = true;
                break;
            case ItemID.FlameWakerBoots:
                player.setApprenticeT2 = true;
                break;
            case ItemID.JellyfishNecklace:
                player.setMonkT2 = true;
                break;
            case ItemID.MagmaStone:
                player.setHuntressT2 = true;
                break;*/
        }
        if (calamityMod != null)
        {
            if (item.type == calamityMod.Find<ModItem>("EldritchSoulArtifact").Type)
            {
                player.maxTurrets += 2;
            }
            if (item.type == calamityMod.Find<ModItem>("BlazingCore").Type)
            {
                player.maxTurrets += 1;
                player.GetDamage<SentryDamageClass>() += 0.15f;
            }
            if (item.type == calamityMod.Find<ModItem>("DarkSunRing").Type)
            {
                player.maxTurrets += 2;
            }
            if (item.type == calamityMod.Find<ModItem>("Nucleogenesis").Type)
            {
                ChangeToMinionDmg(0.15f);
            }
            if (item.type == calamityMod.Find<ModItem>("StarTaintedGenerator").Type)
            {
                ChangeToMinionDmg(0.07f);
            }
            if (item.type == calamityMod.Find<ModItem>("StatisBlessing").Type)
            {
                ChangeToMinionDmg(0.1f);
            }
            if (item.type == calamityMod.Find<ModItem>("StatisCurse").Type)
            {
                ChangeToMinionDmg(0.1f);
            }
        }
        void ChangeToSentryDmg(float amount)
        {
            player.GetDamage<SummonDamageClass>() -= amount;
            player.GetDamage<SentryDamageClass>() += amount;
        }
        void ChangeToMinionDmg(float amount)
        {
            player.GetDamage<SummonDamageClass>() -= amount;
            player.GetDamage<MinionDamageClass>() += amount;
        }
    }
    static Func<Item, TooltipLine, bool> LineNum(int n)
    {
        return (Item i, TooltipLine l) => l.Mod == "Terraria" && l.Name == $"Tooltip{n}";
    }
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        switch (item.type)
        {
            case ItemID.HuntressBuckler:
            case ItemID.SquireShield:
            case ItemID.ApprenticeScarf:
            case ItemID.MonkBelt:
                EditTooltipByNum(1, delegate (TooltipLine line)
                {
                    line.Text = "Increases sentry damage by 15%";
                });
                break;
            case ItemID.HerculesBeetle:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "Increases summon damage by 15%";
                });
                break;
            case ItemID.PygmyNecklace:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "Increases your max number of minions and sentries by 1";
                });
                break;
            case ItemID.FlinxFurCoat:
                EditTooltipByNum(1, delegate (TooltipLine line)
                {
                    line.Text = "Increases your max number of minions and sentries by 1\nSentries deal +3 damage";
                });
                break;
            case ItemID.ObsidianShirt:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "Increases your max number of minions and sentries";
                });
                break;
            case ItemID.AncientBattleArmorHat:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "15% increased magic and sentry damage\nIncreases your max number of sentries by 1";
                });
                break;
            case ItemID.AncientBattleArmorShirt:
                EditTooltipByNum(1, delegate (TooltipLine line)
                {
                    line.Text = "15% increased sentry damage";
                });
                EditTooltipByNum(2, delegate (TooltipLine line)
                {
                    line.Text = "Increases your max number of sentries by 1\nAutomatically drink Mana Potions when needed";
                });
                break;
            case ItemID.AncientBattleArmorPants:
                if (calamityMod != null) 
                {
                    EditTooltipByNum(2, delegate (TooltipLine line)
                    {
                        line.Text = "Increases your max number of sentries by 1";
                    });
                } else { 
                    EditTooltipByNum(2, delegate (TooltipLine line)
                    {
                        line.Text = "Increases your max number of sentries by 2";
                    });
                }
                break;
            case ItemID.StardustHelmet:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "Increases your max number of minions and sentries by 1";
                });
                break;
            case ItemID.StardustBreastplate:
                EditTooltipByNum(1, delegate (TooltipLine line)
                {
                    line.Text = "Increases summon damage by 22%\nIncreases whip range by 15%";
                });
                break;
            case ItemID.StardustLeggings:
                EditTooltipByNum(1, delegate (TooltipLine line)
                {
                    line.Text = "Increases summon damage by 22%\nIncreases whip range by 15%";
                });
                break;
            /*case ItemID.PanicNecklace:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "Increases Movement speed after taking damage\nProvides Ballista Panic on hit and increases Ballista effectiveness.";
                });
                break;
            case ItemID.FlameWakerBoots:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "Increases Flameburst effectiveness.\n'Never get cold feet again";
                });
                break;
            case ItemID.JellyfishNecklace:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "Generates a very subtle glow which becomes more vibrant underwater\nIncreases Lightning Aura effectiveness.";
                });
                break;
            case ItemID.MagmaStone:
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "Increases Explosive Trap effectiveness.";
                });
                break;*/
        }

        if (calamityMod != null)
        {
            if (item.type == calamityMod.Find<ModItem>("EldritchSoulArtifact").Type)
            {
                EditTooltipByNum(1, delegate (TooltipLine line)
                {
                    line.Text = "Boosts melee speed by 10%, ranged velocity by 25%, rogue stealth regen by 10%, max minions and sentries by 2, and reduces mana cost by 15%";
                });
            }
            if (item.type == calamityMod.Find<ModItem>("BlazingCore").Type)
            {
                EditTooltipByNum(0, delegate (TooltipLine line)
                {
                    line.Text = "The searing core of the profaned goddess\n15% Increased sentry damage and +1 max sentry";
                });
            }
            if (item.type == calamityMod.Find<ModItem>("DarkSunRing").Type)
            {
                EditTooltipByNum(2, delegate (TooltipLine line)
                {
                    line.Text = "+1 life regen, 15% increased pick speed and +2 max minions and sentries";
                });
            }
        }

        void ApplyTooltipEdits(IList<TooltipLine> lines, Func<Item, TooltipLine, bool> predicate, Action<TooltipLine> action)
        {
            foreach (TooltipLine line2 in lines)
            {
                if (predicate(item, line2))
                {
                    action(line2);
                }
            }
        }
        void EditTooltipByNum(int lineNum, Action<TooltipLine> action)
        {
            ApplyTooltipEdits(tooltips, LineNum(lineNum), action);
        }

    }
    public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
    {
        SentryPlayer splayer = player.GetModPlayer<SentryPlayer>();
        switch (item.type)
        {
            case ItemID.DD2LightningAuraT3Popper:
                if (splayer.OOA_T4 == true)
                {
                    damage *= 7;
                }
                break;
            case ItemID.DD2ExplosiveTrapT3Popper:
            case ItemID.DD2BallistraTowerT3Popper:
                if (splayer.OOA_T4 == true) {
                    damage *= 6;
                }
                break;
            case ItemID.DD2FlameburstTowerT3Popper:
                if (splayer.OOA_T4 == true)
                {
                    damage *= 8;
                }
                break;
        }
    }
}