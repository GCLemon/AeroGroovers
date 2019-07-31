using asd;

namespace AeroGroovers.View
{
    /// <summary>
    /// このゲームで使用するテキスト
    /// </summary>
    class AGText : TextObject2D
    {
        /// <summary>
        /// テキストオブジェクトの中心座標
        /// </summary>
        public Vector2DF CenteringPosition { private get; set; }

        /// <param name="fontSize"> 文字の大きさ </param>
        /// <param name="fontColor"> 文字の色 </param>
        /// <param name="outlineSize"> 輪郭線の太さ </param>
        /// <param name="outlineColor"> 輪郭線の色 </param>
        public AGText(
            int fontSize,
            Color fontColor,
            int outlineSize,
            Color outlineColor,
            Vector2DF centering = new Vector2DF()
        )
        {
            // フォントを設定する
            Font = Engine.Graphics.CreateDynamicFont(
                "Graphics/Period.otf",
                fontSize,
                fontColor,
                outlineSize,
                outlineColor
            );

            // 中心座標を設定する
            CenteringPosition = centering;
            SetCenterPosition();
        }

        /// <param name="fontSize"> 文字の大きさ </param>
        /// <param name="outlineSize"> 輪郭線の太さ </param>
        public AGText(
            int fontSize,
            int outlineSize,
            Vector2DF centering = new Vector2DF()
        )
        {
            // フォントを設定する
            Font = Engine.Graphics.CreateDynamicFont(
                "Graphics/Period.otf",
                fontSize,
                new Color(255, 255, 255),
                outlineSize,
                new Color(0, 0, 0)
            );

            // 中心座標を設定する
            CenteringPosition = centering;
            SetCenterPosition();
        }

        /// <summary>
        /// オブジェクトの拡大率を設定する
        /// </summary>
        /// <param name="x"> x方向の拡大率 </param>
        /// <param name="y"> y方向の拡大率 </param>
        public void SetScale(float x, float y)
        {
            Scale = new Vector2DF(x, y);
            SetCenterPosition();
        }

        /// <summary>
        /// テキストを設定する
        /// </summary>
        /// <param name="text"> 変更後のテキスト </param>
        public void SetText(string text)
        {
            Text = text;
            SetCenterPosition();
        }

        /// <summary>
        /// 中心座標を設定する
        /// </summary>
        private void SetCenterPosition()
        {
            WritingDirection direction = WritingDirection.Horizontal;
            Vector2DF size = Font.CalcTextureSize(Text, direction).To2DF();
            CenterPosition = size * CenteringPosition;

        }
    }
}
