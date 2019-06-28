using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public void AddPlayer(Controller controller)
        {
            // 同じIDを持つコントローラーを持つプレイヤーが存在した場合
            // メソッド内の処理を終える
            if (Player
                    .Where(p => p != null)
                    .Any(p => p.Controller.ControllerID == controller.ControllerID)
               )
                return;

            Player player = new Player(controller);
            
            // nullが見つかったらそこに代入する
            for (int i = 0; i < 4; ++i)
                if (Player[i] == null)
                {
                    player.PlayerNumber = i + 1;
                    Player[i] = player;
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
        private List<Score> ScoreList = new List<Score>();

        /// <summary>
        /// 譜面を取得するときのインデックス番号
        /// </summary>
        private int ScoreIndex;

        /// <summary>
        /// 直前の譜面に切り替え
        /// </summary>
        public void DecrementScoreIndex() =>
            ScoreIndex = nmr(ScoreIndex - 1, ScoreList.Count);

        /// <summary>
        /// 直後の譜面に切り替え
        /// </summary>
        public void IncrementScoreIndex() =>
            ScoreIndex = nmr(ScoreIndex + 1, ScoreList.Count);

        /// <summary>
        /// 譜面を読み込む
        /// </summary>
        public void LoadScore()
        {
            // .tomlファイルパスを取得
            string[] scores = Directory.GetFiles(
                Directory.GetCurrentDirectory(),
                "*.toml",
                SearchOption.AllDirectories
            );

            // Scoresに読み込んだ譜面を放り込む
            foreach(string path in scores)
                ScoreList.Add(new Score(path));
        }

        /// <summary>
        /// 現在選択している譜面
        /// </summary>
        public Score Score { get => ScoreList[ScoreIndex]; }

        /// <summary>
        /// 現在選択している譜面とその前後3つ
        /// </summary>
        public Score[] Scores
        {
            get => new Score[]
            {
                ScoreList[nmr(ScoreIndex - 3, ScoreList.Count)],
                ScoreList[nmr(ScoreIndex - 2, ScoreList.Count)],
                ScoreList[nmr(ScoreIndex - 1, ScoreList.Count)],
                ScoreList[nmr(ScoreIndex + 0, ScoreList.Count)],
                ScoreList[nmr(ScoreIndex + 1, ScoreList.Count)],
                ScoreList[nmr(ScoreIndex + 2, ScoreList.Count)],
                ScoreList[nmr(ScoreIndex + 3, ScoreList.Count)]
            };
        }

        private int nmr(int x, int y) =>
            x % y + ((x * y >= 0) ? 0 : y);
    }
}
