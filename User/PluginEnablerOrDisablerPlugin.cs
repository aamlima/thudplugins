using Turbo.Plugins.Brodis;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.User
{

    public class PluginEnablerOrDisablerPlugin : BasePlugin, ICustomizer
    {

        public PluginEnablerOrDisablerPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
        }

        public void Customize()
        {

            Hud.TogglePlugin<DebugPlugin>(true);
            Hud.TogglePlugin<TopExperienceStatistics>(false);
            Hud.TogglePlugin<AquilaIndicatorPlugin>(false);

            Hud.RunOnPlugin<PlayerBottomBuffListPlugin>(plugin =>
            {
                plugin.RuleCalculator.Rules.Add(new BuffRule(402461) { IconIndex = 2, ShowTimeLeft = false, UseLegendaryItemTexture = Hud.Sno.SnoItems.Unique_Ring_017 }); // Oculus
                plugin.RuleCalculator.Rules.Add(new BuffRule(430674) { IconIndex = null }); // CoE
            });

            Hud.RunOnPlugin<GlobePlugin>(plugin =>
            {
                plugin.RiftOrbDecorator.Add(new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 240, 120, 240, 3),
                    Radius = 1.5f
                });

                plugin.PowerGlobeDecorator.Add(new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(255, 240, 240, 120, 3),
                    Radius = 1.5f
                });
            });

            Hud.RunOnPlugin<ItemsPlugin>(plugin =>
            {
                plugin.DeathsBreathDecorator.Add(new GroundCircleDecorator(Hud)
                {
                    Brush = Hud.Render.CreateBrush(192, 102, 202, 177, -2),
                    Radius = 1.25f,
                });

                plugin.LegendaryDecorator.Decorators.Add(new MapLabelDecorator(Hud)

                {
                    LabelFont = Hud.Render.CreateFont("tahoma", 6, 255, 235, 120, 0, false, false, false),
                    RadiusOffset = 14,
                    Up = true,
                });

                plugin.SetDecorator.Decorators.Add(new MapLabelDecorator(Hud)
                {

                    LabelFont = Hud.Render.CreateFont("tahoma", 6, 255, 0, 170, 0, false, false, false),
                    RadiusOffset = 14,
                    Up = true,
                });

                plugin.AncientDecorator.Decorators.Add(new MapLabelDecorator(Hud)
                {
                    LabelFont = Hud.Render.CreateFont("tahoma", 6, 255, 235, 120, 0, true, false, false),
                    RadiusOffset = 15,
                    Up = true,
                });

                plugin.AncientSetDecorator.Decorators.Add(new MapLabelDecorator(Hud)
                {

                    LabelFont = Hud.Render.CreateFont("tahoma", 6, 255, 0, 170, 0, true, false, false),
                    RadiusOffset = 15,
                    Up = true,
                });

                plugin.PrimalDecorator.Decorators.Add(new MapLabelDecorator(Hud)
                {

                    LabelFont = Hud.Render.CreateFont("tahoma", 7, 255, 240, 20, 0, true, false, false),
                    RadiusOffset = 16,
                    Up = true,

                });

                plugin.PrimalSetDecorator.Decorators.Add(new MapLabelDecorator(Hud)
                {
                    LabelFont = Hud.Render.CreateFont("tahoma", 7, 255, 240, 20, 0, true, false, false),
                    RadiusOffset = 16,
                    Up = true,
                });

            });
        }

    }

}