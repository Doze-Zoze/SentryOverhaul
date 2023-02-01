using Terraria.ModLoader;

namespace SentryOverhaul
{
    public class SentryOverhaul : Mod
    {
        internal Mod calamityMod;
        public override void Load()
        {
            calamityMod = null;
            ModLoader.TryGetMod("CalamityMod", out calamityMod);
        }

        public override void Unload()
        {
            calamityMod = null;
            base.Unload();
        }
    }
}