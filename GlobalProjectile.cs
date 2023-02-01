using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SentryOverhaul;

public class SentryGlobalProjectile : GlobalProjectile
{
    public override void SetDefaults(Projectile projectile)
    {
        ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
        HashSet<int> SentryDmgFixSet = new HashSet<int>();
        if (projectile.sentry == true ||  (ProjectileID.Sets.SentryShot[projectile.type] == true))
        {
            projectile.DamageType = SentryDamageClass.Instance;
        }
        if (calamityMod != null)
        {
            sentryDamageFixStr(calamityMod, "FrogGore1");
            sentryDamageFixStr(calamityMod, "FrogGore2");
            sentryDamageFixStr(calamityMod, "FrogGore3");
            sentryDamageFixStr(calamityMod, "FrogGore4");
            sentryDamageFixStr(calamityMod, "FrogGore5");
            sentryDamageFixStr(calamityMod, "PolypLauncherProjectile");
            sentryDamageFixStr(calamityMod, "PolypLauncherShrapnel");
            sentryDamageFixStr(calamityMod, "IceSentryShard");
            sentryDamageFixStr(calamityMod, "Dreadmine");
            sentryDamageFixStr(calamityMod, "FlameBlast");
            sentryDamageFixStr(calamityMod, "LanternFlame");
            sentryDamageFixStr(calamityMod, "LostSoulGold");
            sentryDamageFixStr(calamityMod, "LostSoulGiant");
            sentryDamageFixStr(calamityMod, "LostSoulLarge");
            sentryDamageFixStr(calamityMod, "LostSoulSmall");
            sentryDamageFixStr(calamityMod, "AtlasMunitionsLaser");
            sentryDamageFixStr(calamityMod, "AtlasMunitionsLaserOverdrive");
            if (projectile.type == calamityMod.Find<ModProjectile>("AtlasMunitionsAutocannon").Type || projectile.type == calamityMod.Find<ModProjectile>("AtlasMunitionsAutocannonHeld").Type || projectile.type == calamityMod.Find<ModProjectile>("AtlasMunitionsDropPod").Type || projectile.type == calamityMod.Find<ModProjectile>("AtlasMunitionsDropPodUpper").Type)
            {
                projectile.sentry = false;
                projectile.DamageType = SentryDamageClass.Instance;
            }
        }
        if (SentryDmgFixSet.Contains(projectile.type))
        {
            projectile.DamageType = SentryDamageClass.Instance;
        }
        void sentryDamageFixStr(Mod mod, string proj)
        {
            SentryDmgFixSet.Add(mod.Find<ModProjectile>(proj).Type);
        }

        switch (projectile.type) {
            case ProjectileID.DD2FlameBurstTowerT1Shot:
            case ProjectileID.HoundiusShootiusFireball:
                projectile.extraUpdates = 1;
                break;
            case ProjectileID.MoonlordTurretLaser:
                projectile.extraUpdates = 2;
                projectile.DamageType = SentryDamageClass.Instance;
                break;
            case ProjectileID.MoonlordTurret:
                projectile.extraUpdates = 2;
                break;
            case ProjectileID.DD2FlameBurstTowerT2Shot:
                projectile.extraUpdates = 2;
                break;
            case ProjectileID.DD2FlameBurstTowerT3Shot:
                projectile.extraUpdates = 3;
                break;
            case ProjectileID.HoundiusShootius:
                break;
            case ProjectileID.RainbowCrystalExplosion:
                projectile.extraUpdates = 1;
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = -1;
                break;
            case ProjectileID.FrostHydra:
            case ProjectileID.FrostBlastFriendly:
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = -1;
                break;
        }
    }
    public override void ModifyDamageScaling(Projectile projectile, ref float damageScale)
    {
        Player player = Main.player[projectile.owner];
        SentryPlayer splayer = player.GetModPlayer<SentryPlayer>();
        switch (projectile.type)
        {
            case ProjectileID.DD2LightningAuraT3:
                if (splayer.OOA_T4 == true)
                {
                    damageScale *= 7;
                }
                break;
            case ProjectileID.DD2ExplosiveTrapT3Explosion:
            case ProjectileID.DD2BallistraProj:
                if (splayer.OOA_T4 == true)
                {
                    damageScale *= 6;
                }
                break;
            case ProjectileID.DD2FlameBurstTowerT3Shot:
                if (splayer.OOA_T4 == true)
                {
                    damageScale *= 8;
                }
                break;
            case ProjectileID.SandnadoFriendly:
                damageScale = player.GetDamage<SentryDamageClass>().ApplyTo(1);
                if (splayer.OOA_T4 == true)
                {
                    damageScale *= 4;
                }
                break;
        }
    }
}