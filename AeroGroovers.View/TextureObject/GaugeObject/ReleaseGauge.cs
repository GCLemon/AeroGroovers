using asd;

namespace AeroGroovers.View
{
    public partial class EntryScene
    {
        /// <summary>
        /// FrameCountの値を画面に表示するゲージオブジェクト
        /// </summary>
        private class ReleaseGauge : GaugeObject
        {
            public ReleaseGauge()
            {
                Initialize("Graphics/ReleaseGauge.png");

                UR = new Vector2DF(240, 0);
                UL = new Vector2DF(0, 0);
                DR = new Vector2DF(240, 40);
                DL = new Vector2DF(0, 40);
            }
        }
    }
}
