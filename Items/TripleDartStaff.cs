
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace SentryOverhaul.Items
{
	public class TripleDartStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Triple Dart Staff");
			Tooltip.SetDefault("Summons a friendly monkey");
		}

		public override void SetDefaults()
		{
            base.Item.damage = 17;
            base.Item.DamageType = SentryDamageClass.Instance;
            base.Item.sentry = true;
            base.Item.mana = 15;
            base.Item.width = 66;
            base.Item.height = 68;
            base.Item.useTime = (base.Item.useAnimation = 30);
            base.Item.useStyle = 1;
            base.Item.noMelee = true;
            base.Item.knockBack = 5f;
            base.Item.value = 500;
            base.Item.rare = ItemRarityID.Blue;
            base.Item.autoReuse = true;
            base.Item.shoot = ModContent.ProjectileType<TripleDartMonkey>();
            Item.UseSound = SoundID.DD2_DefenseTowerSpawn;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int p = Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI, 16f);
            if (Main.projectile.IndexInRange(p))
            {
                Main.projectile[p].originalDamage = base.Item.damage;
            }
            player.UpdateMaxTurrets();
            return false;
        }

        public override void AddRecipes()
		{
			Recipe.Create(this.Type)
			    .AddIngredient<DartStaff>()
			    .AddIngredient(ItemID.DefenderMedal, 5)
                .AddIngredient(ItemID.SoulofLight)
                .AddIngredient(ItemID.SoulofNight)
                .AddIngredient(ItemID.SoulofFlight)
			    .AddTile(TileID.Anvils)
			    .Register();
		}
	}
}