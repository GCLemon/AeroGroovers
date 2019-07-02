using asd;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        /// <summary>
        /// ゲーム開始前のエフェクト
        /// </summary>
        private class ReadyGoEffect : EffectObject2D
        {
            static Effect ReadyGo = Engine.Graphics.CreateEffect("Resources/Effects/Ready_Go.efk");

            public ReadyGoEffect()
            {
                Effect = ReadyGo;
                Position = Engine.WindowSize.To2DF() * 0.5f;
                Scale = new Vector2DF(35, 35);
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