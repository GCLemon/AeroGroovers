using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        /// <summary>
        /// クリアゲージゲージ
        /// </summary>
        private class ClearGauge : GaugeObject
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

                UR = new Vector2DF(270, 0);
                UL = new Vector2DF(0, 0);
                DR = new Vector2DF(270, 50);
                DL = new Vector2DF(0, 50);

                Character.GaugeMethodSetter =
                    (value) => GaugeValue = value;
            }
        }
    }
}
