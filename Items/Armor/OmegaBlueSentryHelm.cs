using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using System.Globalization;
using rail;

namespace SentryOverhaul.Items.Armor;
[AutoloadEquip(new EquipType[] { EquipType.Head })]
[JITWhenModsEnabled("CalamityMod")]
public class OmegaBlueSentryHelm : ModItem
{
    public override void SetStaticDefaults()
    {
        SacrificeTotal = 1;
        DisplayName.SetDefault("Omega Blue Sentry Helmet");
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        if (calamityMod != null)
        {
            Tooltip.SetDefault("You can move freely through liquids\n15% increased damage");
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
            if (body.type == calamityMod.Find<ModItem>("OmegaBlueChestplate").Type)
            {
                return legs.type == calamityMod.Find<ModItem>("OmegaBlueTentacles").Type;
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
        player.ignoreWater = true;
        player.GetArmorPenetration<GenericDamageClass>() += 15f;
        player.GetDamage<GenericDamageClass>() += 0.15f;
        player.maxTurrets += 3;
        if (ModContent.GetInstance<SentryOverhaul>().calamityMod != null) 
        {
            OmegaBlueSetBonus();
        }

        void OmegaBlueSetBonus()
        {
            player.setBonus = "Increases armor penetration by 15\n15% increased damage and +3 max sentries\nShort-ranged tentacles heal you by sucking enemy life\nPress your Armor Set Bonus hotkey to activate abyssal madness for 5 seconds\nAbyssal madness increases damage, critical strike chance, and tentacle aggression/range\nThis effect has a 25 second cooldown";
            player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>().omegaBlueSet = true;
            player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>().WearingPostMLSummonerSet = true;

        }
}
    public override void UpdateEquip(Player player)
    {
        player.GetDamage<GenericDamageClass>() += 0.15f;
    }

    

    public override void AddRecipes()
    {
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        if (calamityMod != null)
        {
            CreateRecipe()
            .AddIngredient(calamityMod.Find<ModItem>("ReaperTooth"), 8)
            .AddIngredient(calamityMod.Find<ModItem>("Lumenyl"), 5)
            .AddIngredient(calamityMod.Find<ModItem>("Tenebris"), 5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }

    }
}
