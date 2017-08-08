using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Brodis
{
    public class HeathGlobePlugin : BasePlugin, IInGameWorldPainter
    {
        public WorldDecoratorCollection HealthGlobeDecorator { get; set; }

        public HeathGlobePlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            HealthGlobeDecorator = new WorldDecoratorCollection(new GroundCircleDecorator(Hud)
            {
                Brush = Hud.Render.CreateBrush(200, 255, 0, 0, -2),
                Radius = 1.5f
            }, new MapShapeDecorator(Hud)
            {
                Brush = Hud.Render.CreateBrush(255, 255, 0, 0, 0),
                ShadowBrush = Hud.Render.CreateBrush(96, 0, 0, 0, 1),
                Radius = 6.0f,
                ShapePainter = new CircleShapePainter(Hud),
            },
            new GroundLabelDecorator(Hud)
            {
                BackgroundBrush = Hud.Render.CreateBrush(255, 255, 0, 0, 0),
                TextFont = Hud.Render.CreateFont("tahoma", 6.5f, 255, 0, 0, 0, false, false, false),
            });
        }

        public void PaintWorld(WorldLayer layer)
        {
            var actors = Hud.Game.Actors.Where(x => x.SnoActor.Kind == ActorKind.HealthGlobe);

            foreach (var actor in actors)
            {
                HealthGlobeDecorator.ToggleDecorators<GroundLabelDecorator>(!actor.IsOnScreen);
                HealthGlobeDecorator.Paint(layer, actor, actor.FloorCoordinate, "health globe");
            }
        }

    }
}