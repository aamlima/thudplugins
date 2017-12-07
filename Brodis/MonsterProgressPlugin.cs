using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Brodis {
    public class MonsterProgressPlugin : BasePlugin, IInGameWorldPainter {
        public GroundLabelDecorator Decorator { get; set; }
        public MonsterProgressPlugin() {
            Enabled = true;
        }

        public override void Load(IController hud) {
            base.Load(hud);

            Decorator = new GroundLabelDecorator(Hud) {
                BackgroundBrush = Hud.Render.CreateBrush(175, 0, 0, 0, 0),
                TextFont = Hud.Render.CreateFont("tahoma", 10, 255, 255, 255, 255, true, false, true)
            };
        }

        public void PaintWorld(WorldLayer layer) {
            if ((Hud.Game.SpecialArea != SpecialArea.Rift) && (Hud.Game.SpecialArea != SpecialArea.GreaterRift) && (Hud.Game.SpecialArea != SpecialArea.ChallengeRift)) return;
            if (layer != WorldLayer.Ground) return;

            var monsters = Hud.Game.AliveMonsters.Where(x =>(x.SnoMonster != null) && (x.IsOnScreen) && !((x.SummonerAcdDynamicId != 0) && (x.Rarity == ActorRarity.RareMinion)));

            foreach (var monster in monsters) {
                Decorator.Paint(monster, monster.FloorCoordinate, (monster.SnoMonster.RiftProgression / Hud.Game.MaxQuestProgress * 100d).ToString("F2", CultureInfo.InvariantCulture) + "%");
            }
        }

    }
}
