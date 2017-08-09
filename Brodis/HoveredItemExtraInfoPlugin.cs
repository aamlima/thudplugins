using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Brodis
{

    public class HoveredItemExtraInfoPlugin : BasePlugin, IInGameTopPainter
    {

        public IFont ItemSnoFont { get; set; }
        public IFont ItemCountFont { get; set; }
        public IFont ItemSameFont { get; set; }
        public bool ShowItemCount { get; set; }
        public bool ShowItemSno { get; set; }
        public bool HighlightSameItems { get; set; }
        public int[] _location { get; set; }

        public HoveredItemExtraInfoPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            _location = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 19, 23, 24, 25, 26, 27, 28 };

            ShowItemCount = ShowItemSno = HighlightSameItems = true;

            ItemSnoFont = Hud.Render.CreateFont("tahoma", 6, 255, 154, 154, 54, true, false, 128, 0, 0, 0, true);
            ItemCountFont = Hud.Render.CreateFont("tahoma", 8, 255, 174, 174, 124, true, false, 128, 0, 0, 0, true);
            ItemSameFont = Hud.Render.CreateFont("tahoma", 14, 255, 104, 255, 104, true, false, 128, 0, 0, 0, true);
        }

        public void PaintTopInGame(ClipState clipState)
        {
            var item = Hud.Inventory.HoveredItem;
            if (item == null) return;

            var uiTopElement = Hud.Inventory.GetHoveredItemTopUiElement();

            if (clipState == ClipState.AfterClip)
            {
                if (ShowItemSno)
                {
                    var snoText = item.SnoItem.Sno.ToString();
                    var layout = ItemSnoFont.GetTextLayout(snoText);
                    ItemSnoFont.DrawText(layout, uiTopElement.Rectangle.Left + (uiTopElement.Rectangle.Width * 0.5f),
                    uiTopElement.Rectangle.Bottom + (uiTopElement.Rectangle.Height * 0.25f) - (layout.Metrics.Height * 0.5f));
                }

                if (ShowItemCount)
                {
                    var countText = CountItem(item.SnoItem).ToString();
                    var layout = ItemCountFont.GetTextLayout(countText);
                    ItemCountFont.DrawText(layout, uiTopElement.Rectangle.Right - (uiTopElement.Rectangle.Width * 0.05f),
                    uiTopElement.Rectangle.Top - layout.Metrics.Height + uiTopElement.Rectangle.Height * 0.5f);
                }
            }
            else if (clipState == ClipState.Inventory && HighlightSameItems)
            {
                var Items = Hud.Game.Items.Where(i => i.SnoItem == item.SnoItem && i.Location != ItemLocation.Merchant && i != item);

                int stashTab = Hud.Inventory.SelectedStashTabIndex;
                int stashPage = Hud.Inventory.SelectedStashPageIndex;
                int stashTabAbs = stashTab + stashPage * Hud.Inventory.MaxStashTabCountPerPage;

                foreach (var sameItem in Items)
                {
                    if (sameItem.Location == ItemLocation.Stash)
                    {
                        var tabIndex = sameItem.InventoryY / 10;
                        if (tabIndex != stashTabAbs) continue;
                    }
                    if ((sameItem.InventoryX < 0) || (sameItem.InventoryY < 0)) continue;

                    var rect = Hud.Inventory.GetItemRect(sameItem);
                    if (rect == System.Drawing.RectangleF.Empty) continue;

                    var layout = ItemSameFont.GetTextLayout("@");
                    ItemSameFont.DrawText(layout, rect.Left + (rect.Width - layout.Metrics.Width) * 0.5f,
                    rect.Top + (rect.Height - layout.Metrics.Height) * 0.5f);
                }

            }

        }

        private long CountItem(ISnoItem snoItem)
        {
            var count = 0;
            var Items = Hud.Game.Items.Where(i => _location.Contains((int)i.Location));
            foreach (var item in Items)
            {
                if (item.SnoItem == snoItem) count += item.Quantity > 0 ? (int)item.Quantity : 1;
            }
            return count;
        }

    }

}