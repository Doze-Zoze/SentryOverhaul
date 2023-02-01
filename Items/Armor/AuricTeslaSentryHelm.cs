using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace SentryOverhaul.Items.Armor;
[AutoloadEquip(new EquipType[] { EquipType.Head })]
[JITWhenModsEnabled("CalamityMod")]
public class AuricTeslaSentryHelm : ModItem
{
    public override void SetStaticDefaults()
    {
        SacrificeTotal = 1;
        DisplayName.SetDefault("Auric Tesla Gilded Skull");
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        if (calamityMod != null)
        {
            Tooltip.SetDefault("15% increased Sentry damage");
        } else
        {
            Tooltip.SetDefault("This item requires Calamity mod.");
        }
    }

    public override void SetDefaults()
    {
        base.Item.value = 300000;
        base.Item.defense = 12;
        Item.rare = ItemRarityID.Purple;
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        if (calamityMod != null)
        {
            if (body.type == calamityMod.Find<ModItem>("AuricTeslaBodyArmor").Type)
            {
                return legs.type == calamityMod.Find<ModItem>("AuricTeslaCuisses").Type;
            }
        }
        return false;
    }

    public override void ArmorSetShadows(Player player)
    {
        player.armorEffectDrawShadow = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        SentryPlayer splayer = player.GetModPlayer<SentryPlayer>();
        player.setBonus = "Effects of Forbidden armor.\nMassively increased effectiveness of Old One's Army Sentries\nAll projectiles spawn healing auric orbs on enemy hits\n15% increased movement speed\n+6 Max Sentries and 75% increased sentry damage";
        splayer.OOA_T4 = true;
        player.thorns += 3f;
        player.lavaMax += 240;
        player.ignoreWater = true;
        player.crimsonRegen = true;
        player.setHuntressT3 = true;
        player.ballistaPanic = true;
        player.setSquireT3 = true;
        player.setMonkT3 = true;
        player.setApprenticeT3 = true;
        player.setForbidden = true;
        player.manaFlower = true;
        player.moveSpeed += 0.15f;
        player.GetDamage<SentryDamageClass>() += 0.75f;
        player.maxTurrets += 6;
        player.lifeRegen += 4;
        if (ModContent.GetInstance<SentryOverhaul>().calamityMod != null) 
        {
            AuricSetBonus();
        }

        void AuricSetBonus()
        {
            player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>().auricSet = true;
        }
}
    public override void UpdateEquip(Player player)
    {
        player.GetDamage<SentryDamageClass>() += 0.15f;
    }

    

    public override void AddRecipes()
    {
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        if (calamityMod != null)
        {
            CreateRecipe()
            .AddIngredient(calamityMod.Find<ModItem>("AuricBar"), 12)
            .AddIngredient(ItemID.DefenderMedal, 50)
            .AddIngredient(ItemID.AncientBattleArmorHat)
            .AddTile(calamityMod.Find<ModTile>("CosmicAnvil"))
            .Register();
        }

    }
}
