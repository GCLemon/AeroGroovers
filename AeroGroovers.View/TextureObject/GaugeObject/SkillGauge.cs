﻿using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        /// <summary>
        /// スキルゲージ
        /// </summary>
        private class SkillGauge : GaugeObject
        {
            /// <summary>
            /// ゲージに紐付けるスキル
            /// </summary>
            Skill Skill;

            public SkillGauge(Skill skill)
            {
                // スキルを紐付け
                Skill = skill;

                string path = "";

                switch (skill.SkillType)
                {
                    case SkillType.Attack: path = "Resources/Graphics/Gauge_Attack.png"; break;
                    case SkillType.Heal: path = "Resources/Graphics/Gauge_Heal.png"; break;
                    case SkillType.Boost: path = "Resources/Graphics/Gauge_Boost.png"; break;
                    case SkillType.Guard: path = "Resources/Graphics/Gauge_Guard.png"; break;
                    case SkillType.Support: path = "Resources/Graphics/Gauge_Support.png"; break;
                    case SkillType.Edge: path = "Resources/Graphics/Gauge_Edge.png"; break;
                    case SkillType.Damage: path = "Resources/Graphics/Gauge_Danger.png"; break;
                }

                Initialize(path);

                UR = new Vector2DF(150, 0);
                UL = new Vector2DF(0, 0);
                DR = new Vector2DF(150, 40);
                DL = new Vector2DF(0, 40);

                Skill.GaugeMethodSetter =
                    (value) => GaugeValue = value;
            }
        }
    }
}