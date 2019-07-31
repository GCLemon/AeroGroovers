using asd;

using System.Diagnostics;
using System.Text;

namespace AeroGroovers.View
{
    /// <summary>
    /// ゲーム中に使うウィンドウオブジェクト
    /// </summary>
    public class Window : TextureObject2D
    {
        /// <summary>
        /// ウィンドウの大きさ
        /// </summary>
        public Vector2DF WindowSize;

        /// <summary>
        /// 背景のノイズの濃さ
        /// </summary>
        public float NoisyValue;

        /// <summary>
        /// ウィンドウの大きさの最小値
        /// </summary>
        private readonly Vector2DF MinimumSize = new Vector2DF(32, 0);

        /// <summary>
        /// ウィンドウを構成する子オブジェクト
        /// </summary>
        private TextureObject2D lu = new TextureObject2D();
        private TextureObject2D _u = new TextureObject2D();
        private TextureObject2D ru = new TextureObject2D();
        private TextureObject2D l_ = new TextureObject2D();
        private TextureObject2D r_ = new TextureObject2D();
        private TextureObject2D ld = new TextureObject2D();
        private TextureObject2D _d = new TextureObject2D();
        private TextureObject2D rd = new TextureObject2D();

        /// <summary>
        /// ウィンドウに描画するマテリアル
        /// </summary>
        static byte[] shader_code =
            Engine.Graphics.GraphicsDeviceType == GraphicsDeviceType.OpenGL
            ? Engine.File.CreateStaticFile("Shaders/WindowShader.glsl").Buffer
            : Engine.Graphics.GraphicsDeviceType == GraphicsDeviceType.DirectX11
            ? Engine.File.CreateStaticFile("Shaders/WindowShader.hlsl").Buffer
            : new byte[0];

        private readonly Material2D WindowMaterial =
            Engine.Graphics.CreateMaterial2D(
                Engine.Graphics.CreateShader2D(
                    new UTF8Encoding().GetString(shader_code)
                )
            );

        /// <summary>
        /// シェーダーに使うストップウォッチ
        /// </summary>
        private readonly Stopwatch Stopwatch = new Stopwatch();

        public Window(float size_x, float size_y)
        {
            // ウィンドウの大きさを設定
            if (size_x < MinimumSize.X) size_x = MinimumSize.X;
            if (size_y < MinimumSize.Y) size_y = MinimumSize.Y;
            WindowSize = new Vector2DF(size_x, size_y);

            // テクスチャの設定
            Texture = Engine.Graphics.CreateEmptyTexture2D(0, 0, 0);
            SetTexture();

            // 描画部分の設定
            Src = new RectF(4, 24, 50, 50);
            SetDrawArea();

            // 拡大率の設定
            Scale = WindowSize * 0.02f;
            SetScale();

            // 座標の設定
            SetPosition();

            // 色を設定する
            SetColor();

            // 子オブジェクトを追加する
            SetChild();

            // ストップウォッチを作動させる
            Stopwatch.Start();
        }

        protected override void OnUpdate()
        {
            // ウィンドウの大きさを設定
            float size_x = WindowSize.X;
            float size_y = WindowSize.Y;
            if (size_x < MinimumSize.X) size_x = MinimumSize.X;
            if (size_y < MinimumSize.Y) size_y = MinimumSize.Y;
            WindowSize = new Vector2DF(size_x, size_y);

            // 拡大率の設定
            Scale = WindowSize * 0.02f;
            SetScale();

            // 座標の設定
            SetPosition();

            // 色を設定する
            SetColor();
        }

        protected override void OnDrawAdditionally()
        {
            Vector2DF xy_ul = GetGlobalPosition() - CenterPosition;
            Vector2DF xy_ur = GetGlobalPosition() - CenterPosition + new Vector2DF(WindowSize.X, 0);
            Vector2DF xy_dl = GetGlobalPosition() - CenterPosition + new Vector2DF(0, WindowSize.Y);
            Vector2DF xy_dr = GetGlobalPosition() - CenterPosition + WindowSize;

            Color white = new Color(255, 255, 255, 255);

            Vector2DF uv_ul = new Vector2DF(0, 0);
            Vector2DF uv_ur = new Vector2DF(1, 0);
            Vector2DF uv_dl = new Vector2DF(0, 1);
            Vector2DF uv_dr = new Vector2DF(1, 1);

            WindowMaterial.SetFloat("time", Stopwatch.ElapsedMilliseconds * 0.001f);
            WindowMaterial.SetFloat("noise", NoisyValue);

            DrawSpriteWithMaterialAdditionally(
                    xy_ul, xy_ur, xy_dr, xy_dl,
                    white, white, white, white,
                    uv_ul, uv_ur, uv_dr, uv_dl,
                    WindowMaterial, AlphaBlendMode.Add, 0
                );
        }

        /// <summary>
        /// ウィンドウにオブジェクトを追加する
        /// </summary>
        /// <param name="object_2d"> 追加するオブジェクト </param>
        public void AddObject(Object2D object_2d)
        {
            AddChild(
                object_2d,
                ChildManagementMode.RegistrationToLayer,
                ChildTransformingMode.Position
            );
        }

        /// <summary>
        /// ウィンドウにオブジェクトを複数追加する
        /// </summary>
        /// <param name="object_2ds"> 追加するオブジェクト </param>
        public void AddObjects(params Object2D[] object_2ds)
        {
            foreach (var o in object_2ds) AddObject(o);
        }

        /// <summary>
        /// ウィンドウに用いる画像を設定する
        /// </summary>
        private void SetTexture()
        {
            lu.Texture = _u.Texture = ru.Texture =
            l_.Texture = r_.Texture =
            ld.Texture = _d.Texture = rd.Texture =
            Engine.Graphics.CreateTexture2D("Graphics/Window.png");
        }

        /// <summary>
        /// 描画範囲を設定する
        /// </summary>
        private void SetDrawArea()
        {
            lu.Src = new RectF(0, 0, 20, 24);
            _u.Src = new RectF(20, 0, 18, 24);
            ru.Src = new RectF(38, 0, 20, 24);
            l_.Src = new RectF(0, 24, 4, 50);
            r_.Src = new RectF(54, 24, 4, 50);
            ld.Src = new RectF(0, 74, 12, 16);
            _d.Src = new RectF(12, 74, 34, 16);
            rd.Src = new RectF(46, 74, 12, 16);
        }

        /// <summary>
        /// 子オブジェクトの拡大率を設定する
        /// </summary>
        private void SetScale()
        {
            l_.Scale = new Vector2DF(1, WindowSize.Y / 50);
            r_.Scale = new Vector2DF(1, WindowSize.Y / 50);
            _u.Scale = new Vector2DF((WindowSize.X - 32) / 18, 1);
            _d.Scale = new Vector2DF((WindowSize.X - 16) / 34, 1);
        }

        /// <summary>
        /// 描画位置を設定する
        /// </summary>
        private void SetPosition()
        {
            lu.Position = new Vector2DF(-4, -24) - CenterPosition;
            _u.Position = new Vector2DF(16, -24) - CenterPosition;
            ru.Position = new Vector2DF(WindowSize.X - 16, -24) - CenterPosition;
            l_.Position = new Vector2DF(-4, 0) - CenterPosition;
            r_.Position = new Vector2DF(WindowSize.X, 0) - CenterPosition;
            ld.Position = new Vector2DF(-4, WindowSize.Y) - CenterPosition;
            _d.Position = new Vector2DF(8, WindowSize.Y) - CenterPosition;
            rd.Position = new Vector2DF(WindowSize.X - 8, WindowSize.Y) - CenterPosition;
        }

        /// <summary>
        /// ウィンドウの色を設定する
        /// </summary>
        private void SetColor()
        {
            lu.Color = _u.Color = ru.Color =
            l_.Color = r_.Color =
            ld.Color = _d.Color = rd.Color = Color;
        }

        /// <summary>
        /// 子オブジェクトを追加する
        /// </summary>
        private void SetChild()
        {
            var m = ChildManagementMode.RegistrationToLayer;
            var t = ChildTransformingMode.Position;
            AddChild(lu, m, t);
            AddChild(_u, m, t);
            AddChild(ru, m, t);
            AddChild(l_, m, t);
            AddChild(r_, m, t);
            AddChild(ld, m, t);
            AddChild(_d, m, t);
            AddChild(rd, m, t);
        }
    }
}
