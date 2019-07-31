using asd;

using System.Diagnostics;
using System.Text;

namespace AeroGroovers.View
{
    /// <summary>
    /// このゲームで使用する背景エフェクト
    /// </summary>
    public class Background : PostEffect
    {
        private Material2D material;
        private Stopwatch stopwatch;

        public Background(Vector3DF color, Vector3DF light)
        {
            byte[] shader_code =
                Engine.Graphics.GraphicsDeviceType == GraphicsDeviceType.OpenGL
                ? Engine.File.CreateStaticFile("Shaders/BackEffect.glsl").Buffer
                : Engine.Graphics.GraphicsDeviceType == GraphicsDeviceType.DirectX11
                ? Engine.File.CreateStaticFile("Shaders/BackEffect.hlsl").Buffer
                : new byte[0];

            // マテリアルを作成する
            material = Engine.Graphics.CreateMaterial2D(
                Engine.Graphics.CreateShader2D(
                        new UTF8Encoding().GetString(shader_code)
                    )
                );

            // ストップウォッチを作成する
            stopwatch = new Stopwatch();

            // マテリアル内の変数を設定する
            material.SetVector2DF("resolution", Engine.WindowSize.To2DF());
            material.SetVector3DF("color", color);
            material.SetVector3DF("light", light);

            // ストップウォッチを起動させる
            stopwatch.Start();
        }

        protected override void OnDraw(RenderTexture2D dst, RenderTexture2D src)
        {
            // マテリアル内の変数を設定する
            material.SetFloat("time", stopwatch.ElapsedMilliseconds * 0.001f);

            // マテリアルの内容を出力する
            DrawOnTexture2DWithMaterial(dst, material);
        }
    }
}
