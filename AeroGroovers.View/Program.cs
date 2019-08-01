using asd;

namespace AeroGroovers.View
{
    class Program
    {
        static void Main(string[] args)
        {
            EngineOption option = new EngineOption();
#if !DEBUG
            option.IsFullScreen = true;
#endif
            Engine.Initialize("Aero Groovers", 1280, 800, option);

            Engine.File.AddRootDirectory("Resources/");
            // Engine.File.AddRootPackageWithPassword("Resources.pack", "AERO_GROOVERS");

            Engine.ChangeSceneWithTransition(new TitleScene(), new TransitionFade(0, 1));

            while (Engine.DoEvents())
            {
                Engine.Update();

                if(Engine.Keyboard.GetKeyState(Keys.Escape) == ButtonState.Push)
                {
                    break;
                }
            }

            Engine.Terminate();
        }
    }
}
