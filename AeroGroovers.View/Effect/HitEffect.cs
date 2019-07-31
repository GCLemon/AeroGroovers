using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        /// <summary>
        /// ノーツが反応したときのエフェクト
        /// </summary>
        private class HitEffect : EffectObject2D
        {
            Judge Judge = new Judge();

            static Effect JustShoot = Engine.Graphics.CreateEffect("Effects/JustShoot.efk");
            static Effect Shoot = Engine.Graphics.CreateEffect("Effects/Shoot.efk");
            static Effect Hit = Engine.Graphics.CreateEffect("Effects/Hit.efk");
            static Effect Miss = Engine.Graphics.CreateEffect("Effects/Miss.efk");

            public HitEffect(Judge judge, Vector2DF position)
            {
                switch (judge)
                {
                    case Judge.JustShoot: Effect = JustShoot; break;
                    case Judge.Shoot: Effect = Shoot; break;
                    case Judge.Hit: Effect = Hit; break;
                    case Judge.Miss: Effect = Miss; break;
                }

                Position = position;
                Scale = new Vector2DF(5, 5);

                Judge = judge;
            }

            protected override void OnAdded()
            {
                switch (Judge)
                {
                    case Judge.JustShoot:
                    case Judge.Shoot:
                    case Judge.Hit:
                        Sound.Play(Scene.SE_Hit);
                        break;
                }

                Play();
            }

            protected override void OnUpdate()
            {
                if (!IsPlaying) Dispose();
            }
        }
    }
}