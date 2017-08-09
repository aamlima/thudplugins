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

            if (Hud.Game.Me.Stats.ResourceCurPri >= Hud.Game.Me.Stats.ResourceMaxPri * 0.9f)
            {
                GreenLineBrush.DrawLine(x1, y1, x2, y1);
            }
            else
            {
                RedLineBrush.DrawLine(x1, y1, x2, y1);
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