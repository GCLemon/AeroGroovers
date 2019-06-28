using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class SelectScene
    {
        private class TextLayer : Layer2D
        {
            // テキストオブジェクト
            static Color green = new Color(46, 223, 148);
            static Color yellow = new Color(223, 213, 97);
            static Color red = new Color(245, 0, 92);
            static Color black = new Color(0, 0, 0);

            private static Vector2DF center = new Vector2DF(0.5f, 0.5f);

            AGText Head = new AGText(72, 4, center) { Position = new Vector2DF(640, 70) };
            AGText Title = new AGText(72, 4, center) { Position = new Vector2DF(640, 540) };
            AGText Subtitle = new AGText(36, 4, center) { Position = new Vector2DF(640, 610) };
            AGText Novice_v = new AGText(72, green, 4, black, center) { Position = new Vector2DF(400, 690) };
            AGText Medium_v = new AGText(72, yellow, 4, black, center) { Position = new Vector2DF(640, 690) };
            AGText Expert_v = new AGText(72, red, 4, black, center) { Position = new Vector2DF(880, 690) };
            AGText Novice_l = new AGText(36, green, 4, black, center) { Position = new Vector2DF(400, 750) };
            AGText Medium_l = new AGText(36, yellow, 4, black, center) { Position = new Vector2DF(640, 750) };
            AGText Expert_l = new AGText(36, red, 4, black, center) { Position = new Vector2DF(880, 750) };

            protected override void OnAdded()
            {
                Head.SetText("TUNE SELECT");
                Novice_l.SetText("NOVICE");
                Medium_l.SetText("MEDIUM");
                Expert_l.SetText("EXPERT");

                // テキストオブジェクトの追加
                AddObject(Head);
                AddObject(Title);
                AddObject(Subtitle);
                AddObject(Novice_v);
                AddObject(Medium_v);
                AddObject(Expert_v);
                AddObject(Novice_l);
                AddObject(Medium_l);
                AddObject(Expert_l);
            }

            protected override void OnUpdated()
            {
                Title.SetText(Game.Score.Title);
                Subtitle.SetText(Game.Score.Subtitle);
                Novice_v.SetText(Game.Score.Level[Difficulty.Novice].ToString());
                Medium_v.SetText(Game.Score.Level[Difficulty.Medium].ToString());
                Expert_v.SetText(Game.Score.Level[Difficulty.Expert].ToString());
            }
        }
    }
}
