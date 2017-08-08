using Turbo.Plugins.Default;

namespace Turbo.Plugins.Brodis
{

    public class MoveSpeedStatsPlugin : BasePlugin, IInGameTopPainter
    {

        public TopLabelDecorator MoveSpeedDecorator { get; set; }

        public MoveSpeedStatsPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            MoveSpeedDecorator = new TopLabelDecorator(Hud)
            {
                BackgroundTexture1 = Hud.Texture.BackgroundTextureOrange,
                BackgroundTextureOpacity1 = 1.0f,
                TextFont = Hud.Render.CreateFont("tahoma", 6, 255, 200, 180, 100, true, false, 255, 0, 0, 0, true),
                TextFunc = () => (Hud.Game.Me.Stats.MoveSpeed).ToString() + "%",
                HintFunc = () => "MoveSpeed%(Bonus%)\n" + (Hud.Game.Me.Stats.MoveSpeed.ToString() + "%") +
                    ("(" + Hud.Game.Me.Stats.MoveSpeedBonus.ToString() + "%)"),
            };
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden) return;
            if (clipState != ClipState.BeforeClip) return;

            var uiRect = Hud.Render.GetUiElement("Root.NormalLayer.game_dialog_backgroundScreenPC.game_progressBar_healthBall").Rectangle;

            var w = Hud.Window.Size.Height * 0.05f;
            var h = Hud.Window.Size.Height * 0.02f;

            MoveSpeedDecorator.Paint(uiRect.Right - w * 0.84f, uiRect.Bottom - h * 0.5f, w, h, HorizontalAlign.Center);
        }

    }

}