using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        /// <summary>
        /// スキルゲージが満杯になったときのエフェクト
        /// </summary>
        private class GaugeEffect : EffectObject2D
        {
            static Effect Attack = Engine.Graphics.CreateEffect("Effects/Trigger_Attack.efk");
            static Effect Heal = Engine.Graphics.CreateEffect("Effects/Trigger_Heal.efk");
            static Effect Boost = Engine.Graphics.CreateEffect("Effects/Trigger_Boost.efk");
            static Effect Guard = Engine.Graphics.CreateEffect("Effects/Trigger_Guard.efk");
            static Effect Support = Engine.Graphics.CreateEffect("Effects/Trigger_Support.efk");
            static Effect Edge = Engine.Graphics.CreateEffect("Effects/Trigger_Edge.efk");
            static Effect Damage = Engine.Graphics.CreateEffect("Effects/Trigger_Danger.efk");

            public GaugeEffect(SkillType skill, Vector2DF position)
            {
                switch (skill)
                {
                    case SkillType.Attack: Effect = Attack; break;
                    case SkillType.Heal: Effect = Heal; break;
                    case SkillType.Boost: Effect = Boost; break;
                    case SkillType.Guard: Effect = Guard; break;
                    case SkillType.Support: Effect = Support; break;
                    case SkillType.Edge: Effect = Edge; break;
                    case SkillType.Damage: Effect = Damage; break;
                }

                Scale = new Vector2DF(10, 10);
                Position = position;
            }

            protected override void OnAdded()
            {
                Play();
            }

            protected override void OnUpdate()
            {
                if (!IsPlaying) Dispose();
            }
        }
    }
}