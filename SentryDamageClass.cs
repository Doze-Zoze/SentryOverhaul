using Terraria.ModLoader;

namespace SentryOverhaul;

public class SentryDamageClass : DamageClass
{
    internal static SentryDamageClass Instance;

    public override void Load()
    {
        Instance = this;
    }

    public override void Unload()
    {
        Instance = null;
    }

    public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
    {
        if (damageClass == ModContent.GetInstance<MinionDamageClass>())
        {
            return new StatInheritanceData(
                damageInheritance: -0.25f,
                critChanceInheritance: 1f,
                attackSpeedInheritance: 1f,
                armorPenInheritance: 0f,
                knockbackInheritance: 1f
            );
        }
        if (damageClass == DamageClass.Summon || damageClass == DamageClass.Generic)
        {
            return StatInheritanceData.Full;
        }
        return StatInheritanceData.None;
    }

    public override bool GetEffectInheritance(DamageClass damageClass)
    {
        return damageClass == DamageClass.Summon;
    }
    public override bool UseStandardCritCalcs => false;
}
