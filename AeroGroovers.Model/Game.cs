using System.Collections.Generic;

namespace AeroGroovers.Model
{
    /// <summary>
    /// ゲーム全体が持つ情報
    /// </summary>
    public class Game
    {
        /// <summary>
        /// 登録されているプレイヤー 登録されていない場合はnull
        /// </summary>
        public Player[] Player { get; private set; } = new Player[4];

        /// <summary>
        /// プレイヤーを登録する
        /// </summary>
        public void AddPlayer(Player new_player)
        {
            for (int i = 0; i < 4; ++i)
                if (Player[i] == null)
                {
                    Player[i] = new_player;
                    break;
                }
        }

        /// <summary>
        /// プレイヤーを削除する
        /// </summary>
        public void DeletePlayer(int player_number)
        {
            Player[player_number - 1] = null;
        }

        /// <summary>
        /// 読み込んだ譜面
        /// </summary>
        public List<Score> Scores { get; private set; }

        /// <summary>
        /// 譜面を取得するときのインデックス番号
        /// </summary>
        private int ScoreIndex;

        /// <summary>
        /// 譜面を読み込む
        /// </summary>
        public void ReadScore()
        {

        }
    }
}
