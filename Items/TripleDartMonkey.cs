using Microsoft.Xna.Framework;
using System;
using System.IO.Pipelines;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SentryOverhaul.Items;

public class TripleDartMonkey : ModProjectile
{
    private float count;

    public override void SetStaticDefaults()
    {
        base.DisplayName.SetDefault("Triple Dart Monkey");
        Main.projPet[Projectile.type] = true;
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
    }
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.sentry = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 7200;
        Projectile.DamageType = SentryDamageClass.Instance;
        Projectile.scale = 0.75f;
        Projectile.width = 72;
        Projectile.height = 72;
        DrawOffsetX = 0;
        DrawOriginOffsetX = 0;
        DrawOriginOffsetY = 0;
    }
    private int shotCooldown = 0;
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        float speed = 30.0f;
        bool foundTarget = false;
        float distanceFromTarget = 0f;
        
        int projectileType = ProjectileID.DD2BallistraProj;
        Vector2 targetCenter = new Vector2();
        SearchForTargets(player, out foundTarget, out distanceFromTarget, out targetCenter);
        if (shotCooldown > 0)
        {
            shotCooldown -= 1;
        }
        if (foundTarget == true && distanceFromTarget <= 1100f && shotCooldown == 0)
        {
            Vector2 direction = targetCenter - Projectile.Center;
            direction.Normalize();
            Projectile.rotation = (float)Math.Atan2(direction.Y, direction.X);
            direction *= speed;
            int spread = 5;
            direction = direction.RotatedBy(MathHelper.ToRadians(-spread));
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y), direction, projectileType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
                direction = direction.RotatedBy(MathHelper.ToRadians(spread));
            }
            
            shotCooldown = 45;
        }
        

    }
    private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
    {
        // Starting search distance
        distanceFromTarget = 550f;
        targetCenter = Projectile.position;
        foundTarget = false;


        // This code is required if your minion weapon has the targeting feature
        if (owner.HasMinionAttackTargetNPC)
        {
            NPC npc = Main.npc[owner.MinionAttackTargetNPC];
            float between = Vector2.Distance(npc.Center, Projectile.Center);

            // Reasonable distance away so it doesn't target across multiple screens
            if (between < 1100f)
            {
                distanceFromTarget = between;
                targetCenter = npc.Center;
                foundTarget = true;
            }
        }
        if (!foundTarget)
        {
            // This code is required either way, used for finding a target
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy())
                {
                    float between = Vector2.Distance(npc.Center, Projectile.Center);
                    bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                    bool inRange = between < distanceFromTarget;
                    bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                    // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                    // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                    bool closeThroughWall = between < 100f;
                    if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                    {
                        distanceFromTarget = between;
                        targetCenter = npc.Center;
                        foundTarget = true;
                    }
                }
            }
        }
    }
}