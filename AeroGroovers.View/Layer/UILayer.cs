using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        private class UILayer : Layer2D
        {
            /// <summary>
            /// レイヤーに紐づいているプレイヤー
            /// </summary>
            private Player Player;

            /// <summary>
            /// 画面に表示するテキスト
            /// </summary>
            AGText Score_text;

            public UILayer(int i)
            {
                DrawingPriority = 5;

                // プレイヤーの紐付け
                Player = Game.Player[i];

                // テキストの配置
                AddObject(Score_text = new AGText(36, 4, new Vector2DF(0.5f, 0.5f))
                {
                    Position = new Vector2DF(310 * Player.PlayerNumber - 135, 80)
                });
                Score_text.SetText(0.ToString("0000000"));

                // ゲージの配置
                AddObject(new ClearGauge(Player.Character)
                {
                    Position = new Vector2DF(310 * i + 40, 690)
                });
                AddObject(new SkillGauge(Player.Character.Skills[0])
                {
                    Position = new Vector2DF(310 * i + 160, 570)
                });
                AddObject(new SkillGauge(Player.Character.Skills[1])
                {
                    Position = new Vector2DF(310 * i + 160, 610)
                });
                AddObject(new SkillGauge(Player.Character.Skills[2])
                {
                    Position = new Vector2DF(310 * i + 160, 650)
                });

                Player.Character.Skills[0].AddEffect =
                        () => ((GameScene)Scene).EffectLayer.AddObject(
                            new GaugeEffect(
                                Player.Character.Skills[0].SkillType,
                                new Vector2DF(310 * i + 235, 590)
                            )
                        );
                Player.Character.Skills[1].AddEffect =
                        () => ((GameScene)Scene).EffectLayer.AddObject(
                            new GaugeEffect(
                                Player.Character.Skills[1].SkillType,
                                new Vector2DF(310 * i + 235, 630)
                            )
                        );
                Player.Character.Skills[2].AddEffect =
                        () => ((GameScene)Scene).EffectLayer.AddObject(
                            new GaugeEffect(
                                Player.Character.Skills[2].SkillType,
                                new Vector2DF(310 * i + 235, 670)
                            )
                        );
            }

            protected override void OnUpdated()
            {
                Score_text.SetText(Player.Score.ToString("0000000"));
            }
        }
    }
}
