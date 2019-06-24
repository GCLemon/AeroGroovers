﻿using asd;
using System;

using static System.Environment;

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

            Engine.Initialize("Aero Groovers", 1280, 800, new EngineOption());

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