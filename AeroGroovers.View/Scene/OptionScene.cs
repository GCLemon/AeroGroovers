using asd;

namespace AeroGroovers.View
{
    public class OptionScene : Scene
    {
        

        protected override void OnRegistered()
        {
            Layer2D back = new Layer2D();
            Layer2D text = new Layer2D();
            Layer2D window = new Layer2D();

            // 背景を設定する
            back.AddPostEffect(
                new Background(
                    new Vector3DF(0.0f, 0.4f, 1.0f),
                    new Vector3DF(0.4f, 0.7f, 1.0f)
                )
            );

            AddLayer(back);
        }
    }
}
