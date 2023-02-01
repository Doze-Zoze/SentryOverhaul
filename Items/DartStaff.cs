
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace SentryOverhaul.Items
{
	public class DartStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dart Staff");
			Tooltip.SetDefault("Summons a friendly monkey");
		}

		public override void SetDefaults()
		{
            base.Item.damage = 8;
            base.Item.DamageType = SentryDamageClass.Instance;
            base.Item.sentry = true;
            base.Item.mana = 10;
            base.Item.width = 66;
            base.Item.height = 68;
            base.Item.useTime = (base.Item.useAnimation = 30);
            base.Item.useStyle = 1;
            base.Item.noMelee = true;
            base.Item.knockBack = 5f;
            base.Item.value = 500;
            base.Item.rare = ItemRarityID.Blue;
            base.Item.autoReuse = true;
            base.Item.shoot = ModContent.ProjectileType<DartMonkey>();
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
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DartTrap, 1);
			recipe.AddIngredient(ItemID.Wood, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}