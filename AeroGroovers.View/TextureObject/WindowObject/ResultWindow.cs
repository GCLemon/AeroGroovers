using asd;

using System.Linq;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class ResultScene
    {
        /// <summary>
        /// リザルトを表示するウィンドウ
        /// </summary>
        private class ResultWindow : Window
        {

            /// <summary>
            /// テキストオブジェクトの中心座標
            /// </summary>
            private static Vector2DF center = new Vector2DF(0.5f, 0.5f);

            private AGText Player       = new AGText(36, 0, center) { Position = new Vector2DF(135,  30) };
            private AGText JustShoot    = new AGText(24, 0, center) { Position = new Vector2DF(135,  80) };
            private AGText Shoot        = new AGText(24, 0, center) { Position = new Vector2DF(135, 110) };
            private AGText Hit          = new AGText(24, 0, center) { Position = new Vector2DF(135, 140) };
            private AGText Miss         = new AGText(24, 0, center) { Position = new Vector2DF(135, 170) };
            private AGText MaxCombo     = new AGText(24, 0, center) { Position = new Vector2DF(135, 200) };
            private AGText GaugePoint_L = new AGText(24, 0, center) { Position = new Vector2DF(135, 240) };
            private AGText GaugePoint_V = new AGText(24, 0, center) { Position = new Vector2DF(135, 320) };
            private AGText Score_L      = new AGText(24, 0, center) { Position = new Vector2DF(135, 360) };
            private AGText Score_V      = new AGText(36, 0, center) { Position = new Vector2DF(135, 395) };
            private AGText Rank         = new AGText(36, 0, center) { Position = new Vector2DF(135, 450) };
            private AGText Place        = new AGText(36, 0, center) { Position = new Vector2DF(135, 505) };

            private ClearGauge Gauge;

            public ResultWindow(int player_number) : base(270, 540)
            {
                // 引数に指定されたプレイヤー番号に応じてウィンドウの色を変える
                switch (player_number)
                {
                    case 1: Color = new Color(87, 209, 235); break;
                    case 2: Color = new Color(245, 0, 94); break;
                    case 3: Color = new Color(151, 225, 36); break;
                    case 4: Color = new Color(224, 213, 97); break;
                }

                // ウィンドウの位置を変更する
                Position = new Vector2DF(310 * player_number - 266, 175);

                // プレイヤーの取得
                Player player = Game.Player[player_number - 1];

                Gauge = new ClearGauge(player.Character);
                Gauge.Position = new Vector2DF(27, 260);

                Player.SetText("Player " + player_number);

                JustShoot.SetText(player.JustShoot.ToString("JustShoot : 0"));
                Shoot.SetText(player.Shoot.ToString("Shoot : 0"));
                Hit.SetText(player.Hit.ToString("Hit : 0"));
                Miss.SetText(player.Miss.ToString("Miss : 0"));
                MaxCombo.SetText(player.BestCombo.ToString("Max Combo : 0"));

                GaugePoint_L.SetText("Clear Gauge");
                GaugePoint_V.SetText
                (
                    player.ClearJudge == ClearJudge.Failure ? "Failed : " : "Cleared : "
                    + (player.ClearPoint * 0.0001).ToString("0") + "%"
                );

                Score_L.SetText("Total Score");
                Score_V.SetText(player.Score.ToString("000000"));

                Rank.SetText("Rank : " + player.Rank.ToString());

                int place = 1;
                var players = Game.Player.Where(p => p != null && p != player);
                foreach (Player p in players)
                    if (p.Score > player.Score) ++place;

                switch(place)
                {
                    case 1: Place.SetText("1st"); break;
                    case 2: Place.SetText("2nd"); break;
                    case 3: Place.SetText("3rd"); break;
                    case 4: Place.SetText("4th"); break;
                }

                AddObjects
                (
                    Player,
                    JustShoot,
                    Shoot,
                    Hit,
                    Miss,
                    MaxCombo,
                    GaugePoint_L,
                    Gauge,
                    GaugePoint_V,
                    Score_L,
                    Score_V,
                    Rank,
                    Place
                );
            }
        }
    }
}
