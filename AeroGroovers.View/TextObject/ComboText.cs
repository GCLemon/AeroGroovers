using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        private class ComboText : AGText
        {
            /// <summary>
            /// 紐づいているプレイヤー
            /// </summary>
            Player Player;

            /// <summary>
            /// コンボ数
            /// </summary>
            int Combo;

            /// <summary>
            /// フレームカウンタ
            /// </summary>
            int Frame;

            /// <summary>
            /// 「COMBO」
            /// </summary>
            AGText ComboLabel;

            /// <summary>
            /// テキストオブジェクトの中心座標
            /// </summary>
            private static Vector2DF center =
                new Vector2DF(0.5f, 0.5f);

            public ComboText(int player) : base(96, 0, center)
            {
                Player = Game.Player[player - 1];

                Color = new Color(255, 255, 255, 0);
                Text = Player.Combo.ToString();
                Position = new Vector2DF(310 * player - 135, 220);

                ComboLabel = new AGText(36, 0, center)
                {
                    Color = new Color(255, 255, 255, 0),
                    Position = new Vector2DF(0, 70)
                };
                ComboLabel.SetText("COMBO");

                // 子オブジェクトの追加
                var management = ChildManagementMode.RegistrationToLayer;
                var transform = ChildTransformingMode.All;
                AddChild(ComboLabel, management, transform);
            }

            protected override void OnUpdate()
            {
                // コンボ数を設定
                Text = Player.Combo.ToString();

                // コンボ数が1桁だった場合は表示しない
                Color = ComboLabel.Color
                    = new Color(255, 255, 255, Player.Combo < 10 ? 0 : 16);

                // コンボ数が増えたらテキストにエフェクトを加える
                if (Combo < Player.Combo) Frame = 5;

                // 大きさを設定
                float s = 1 + Frame * Frame / 250.0f;
                SetScale(s, s);

                // カウンタをデクリメント
                if (Frame > 0) --Frame;

                // 前フレームのコンボ数を記憶
                Combo = Player.Combo;
            }
        }
    }
}
