using Turbo.Plugins.Default;

namespace Turbo.Plugins.Brodis
{

    public class HoveredItemSnoInfoPlugin : BasePlugin, IInGameTopPainter
    {

        public IFont ItemSnoFont { get; set; }

        public HoveredItemSnoInfoPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            ItemSnoFont = Hud.Render.CreateFont("tahoma", 6, 254, 154, 154, 24, true, false, 128, 0, 0, 0, true);
        }

        public void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.AfterClip) return;

            var item = Hud.Inventory.HoveredItem;
            if (item == null) return;

            var uiElement = Hud.Inventory.GetHoveredItemTopUiElement();

            var snoText = item.SnoItem.Sno.ToString();
            var snoLayout = ItemSnoFont.GetTextLayout(snoText);
            ItemSnoFont.DrawText(snoLayout, uiElement.Rectangle.Left + (uiElement.Rectangle.Width / 2f),
            uiElement.Rectangle.Bottom + (uiElement.Rectangle.Height * 0.25f) - (snoLayout.Metrics.Height / 2f));

        }

    }

}