using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SentryOverhaul;

public class SentryPlayer : ModPlayer
{
    public bool OOA_T4;

    public override void ResetEffects()
    {
        OOA_T4 = false;
    }
}