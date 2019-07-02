using asd;

using System.IO;

namespace AeroGroovers.View
{
    public abstract class GaugeObject : TextureObject2D
    {
        public double GaugeValue { protected get; set; }

        protected Material2D GaugeMaterial =
            Engine.Graphics.CreateMaterial2D(
                Engine.Graphics.CreateShader2D(
                      Engine.Graphics.GraphicsDeviceType == GraphicsDeviceType.OpenGL
                    ? new StreamReader("Resources/Shaders/GaugeShader.glsl").ReadToEnd()
                    : Engine.Graphics.GraphicsDeviceType == GraphicsDeviceType.DirectX11
                    ? new StreamReader("Resources/Shaders/GaugeShader.hlsl").ReadToEnd()
                    : null
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
                GaugeMaterial, AlphaBlendMode.Blend, 0
            );
        }
    }
}
