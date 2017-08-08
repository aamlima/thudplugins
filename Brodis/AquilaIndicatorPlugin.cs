using Turbo.Plugins.Default;
using SharpDX.Direct2D1;

namespace Turbo.Plugins.Brodis
{

    public class AquilaIndicatorPlugin : BasePlugin, IInGameTopPainter
    {

        public IBrush GreenLineBrush { get; set; }
        public IBrush RedLineBrush { get; set; }

        public AquilaIndicatorPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            RedLineBrush = Hud.Render.CreateBrush(200, 255, 0, 0, 2, DashStyle.Dash, CapStyle.Round, CapStyle.Round);
            GreenLineBrush = Hud.Render.CreateBrush(200, 0, 255, 0, 2, DashStyle.Dash, CapStyle.Round, CapStyle.Round);
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden) return;
            if (clipState != ClipState.BeforeClip) return;

            if (!Hud.Game.Me.Powers.GetBuff(449064).Active) return;

            var uiRect = Hud.Render.GetUiElement("Root.NormalLayer.game_dialog_backgroundScreenPC.game_progressBar_manaBall").Rectangle;

            var x1 = uiRect.Left + uiRect.Width * 0.25f;
            var y1 = uiRect.Top + uiRect.Height * 0.06f;
            var x2 = x1 + uiRect.Width * 0.5f;

            if (HasSecondaryResource(Hud.Game.Me.HeroClassDefinition.HeroClass)) x2 -= uiRect.Width * 0.25f;

            if (GetCurMainResourceValue(Hud.Game.Me.HeroClassDefinition.HeroClass) >= GetMaxMainResourceValue(Hud.Game.Me.HeroClassDefinition.HeroClass) * 0.9f)
            {
                GreenLineBrush.DrawLine(x1, y1, x2, y1);
            }
            else
            {
                RedLineBrush.DrawLine(x1, y1, x2, y1);
            }

        }

        private float GetMaxMainResourceValue(HeroClass hClass)
        {
            switch (hClass)
            {
                case HeroClass.Barbarian:
                    return Hud.Game.Me.Stats.ResourceMaxFury;
                case HeroClass.Crusader:
                    return Hud.Game.Me.Stats.ResourceMaxWrath;
                case HeroClass.DemonHunter:
                    return Hud.Game.Me.Stats.ResourceMaxHatred;
                case HeroClass.Monk:
                    return Hud.Game.Me.Stats.ResourceMaxSpirit;
                case HeroClass.Necromancer:
                    return Hud.Game.Me.Stats.ResourceMaxEssence;
                case HeroClass.WitchDoctor:
                    return Hud.Game.Me.Stats.ResourceMaxMana;
                case HeroClass.Wizard:
                    return Hud.Game.Me.Stats.ResourceMaxArcane;
                default:
                    return 0;
            }
        }

        private float GetCurMainResourceValue(HeroClass hClass)
        {
            switch (hClass)
            {
                case HeroClass.Barbarian:
                    return Hud.Game.Me.Stats.ResourceCurFury;
                case HeroClass.Crusader:
                    return Hud.Game.Me.Stats.ResourceCurWrath;
                case HeroClass.DemonHunter:
                    return Hud.Game.Me.Stats.ResourceCurHatred;
                case HeroClass.Monk:
                    return Hud.Game.Me.Stats.ResourceCurSpirit;
                case HeroClass.Necromancer:
                    return Hud.Game.Me.Stats.ResourceCurEssence;
                case HeroClass.WitchDoctor:
                    return Hud.Game.Me.Stats.ResourceCurMana;
                case HeroClass.Wizard:
                    return Hud.Game.Me.Stats.ResourceCurArcane;
                default:
                    return 0;
            }
        }

        private bool HasSecondaryResource(HeroClass hClass)
        {
            switch (hClass)
            {
                case HeroClass.Barbarian:
                    return false;
                case HeroClass.Crusader:
                    return false;
                case HeroClass.DemonHunter:
                    return true;
                case HeroClass.Monk:
                    return false;
                case HeroClass.Necromancer:
                    return false;
                case HeroClass.WitchDoctor:
                    return false;
                case HeroClass.Wizard:
                    return false;
                default:
                    return false;
            }
        }

    }

}