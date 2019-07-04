using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class ResultScene
    {
        /// <summary>
        /// リザルト表示シーンに使うゲージ
        /// </summary>
        public class ClearGauge : GaugeObject
        {
            /// <summary>
            /// ゲージに紐付けるキャラクター
            /// </summary>
            private Character Character;

            public ClearGauge(Character character)
            {
                // キャラクターを紐付け
                Character = character;

                Initialize("Resources/Graphics/ClearGauge.png");

                UR = new Vector2DF(216, 0);
                UL = new Vector2DF(0, 0);
                DR = new Vector2DF(216, 40);
                DL = new Vector2DF(0, 40);

                Character.GaugeMethodSetter =
                    (value) => GaugeValue = value;
            }
        }
    }
}
