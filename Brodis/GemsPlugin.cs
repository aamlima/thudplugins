using System;
using System.Linq;
using System.Text.RegularExpressions;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Brodis
{
    public class GemsPlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection GemDecorator { get; set; }
        public MapTextureDecorator MapDecorator { get; set; }
        public GroundCircleDecorator GroundDecorator { get; set; }
        public GroundLabelDecorator LabelDecorator { get; set; }
        public int MinGemQuality { get; set; }
        public bool ShowLabel { get; private set; }
        private Regex GemNameRegex { get; set; }

        public GemsPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            GemNameRegex = new Regex(@"x1_(\w+)_(\d+)", RegexOptions.Compiled); // x1_Amethyst_01
            MinGemQuality = 6; //Marquise
            ShowLabel = true;

            GemDecorator = new WorldDecoratorCollection(GroundDecorator = new GroundCircleDecorator(Hud)
            {
                Brush = Hud.Render.CreateBrush(200, 255, 255, 255, -2),
                Radius = 1f
            }, MapDecorator = new MapTextureDecorator(Hud)
            {
                Radius = 0.33f
            }, LabelDecorator = new GroundLabelDecorator(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 10, 255, 255, 255, 255, true, false, true)
            });
        }

        public void PaintWorld(WorldLayer layer)
        {
            var items = Hud.Game.Items.Where(item => item.Location == ItemLocation.Floor);
            foreach (var item in items)
            {
                if (item.SnoItem.Kind == ItemKind.gem)
                {
                    int quality = 0;
                    string type = "";

                    Match match = GemNameRegex.Match(item.SnoActor.Code);

                    if (match.Success)
                    {
                        GroupCollection groups = match.Groups;

                        type = groups[1].Value;
                        Int32.TryParse(groups[2].Value, out quality);
                    }

                    if (quality >= MinGemQuality)
                    {
                        MapDecorator.Texture = Hud.Texture.GetItemTexture(item.SnoItem);
                        GroundDecorator.Radius = 0.7f + (quality * 0.1f);
                        LabelDecorator.Enabled = item.IsOnScreen && ShowLabel;

                        GemDecorator.Paint(layer, item, item.FloorCoordinate, quality.ToString());
                    }
                }
            }

        }

    }
}