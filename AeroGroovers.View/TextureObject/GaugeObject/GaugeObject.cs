using asd;

using System.Text;

namespace AeroGroovers.View
{
    public abstract class GaugeObject : GeometryObject2D
    {
        public double GaugeValue { protected get; set; }

        static byte[] shader_code =
            Engine.Graphics.GraphicsDeviceType == GraphicsDeviceType.OpenGL
            ? Engine.File.CreateStaticFile("Shaders/GaugeShader.glsl").Buffer
            : Engine.Graphics.GraphicsDeviceType == GraphicsDeviceType.DirectX11
            ? Engine.File.CreateStaticFile("Shaders/GaugeShader.hlsl").Buffer
            : new byte[0];

        protected Material2D GaugeMaterial =
            Engine.Graphics.CreateMaterial2D(
                Engine.Graphics.CreateShader2D(
                    new UTF8Encoding().GetString(shader_code)
                )
            );

        protected Vector2DF UL, UR, DL, DR;

        protected void Initialize(string path)
        {
            Texture2D GaugeTexture = Engine.Graphics.CreateTexture2D(path);
            GaugeMaterial.SetTexture2D("g_texture", GaugeTexture);
            GaugeMaterial.SetTextureFilterType("g_texture", TextureFilterType.Linear);
            GaugeMaterial.SetTextureWrapType("g_texture", TextureWrapType.Repeat);
        }

        protected override void OnUpdate()
        {
            GaugeMaterial.SetFloat("g_value", (float)GaugeValue);
        }

        protected override void OnDrawAdditionally()
        {
            DrawSpriteWithMaterialAdditionally(
                GetGlobalPosition() + UL, GetGlobalPosition() + UR, GetGlobalPosition() + DR, GetGlobalPosition() + DL,
                new Color(255, 255, 255), new Color(255, 255, 255), new Color(255, 255, 255), new Color(255, 255, 255),
                new Vector2DF(0, 0), new Vector2DF(1, 0), new Vector2DF(1, 1), new Vector2DF(0, 1),
                GaugeMaterial, AlphaBlendMode.Opacity, 0
            );
        }
    }
}
