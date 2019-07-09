namespace AeroGroovers.Model
{
    /// <summary>
    /// プレイヤーが持つ情報
    /// </summary>
    public class Player
    {
        //////////////////////////////////////////////////
        //
        //    インスタンスが作られるときに設定する項目
        //    外部クラスから設定不可能
        //

        /// <summary>
        /// プレイヤー番号
        /// </summary>
        public int PlayerNumber;

        /// <summary>
        /// 登録されているコントローラー
        /// </summary>
        public Controller Controller { get; private set; }

        //////////////////////////////////////////////////
        //
        //    OptionSceneで設定する項目
        //    外部クラスから設定可能
        //

        /// <summary>
        /// プレイするときの難易度
        /// </summary>
        public Difficulty Difficulty;

        /// <summary>
        /// ノートの速度
        /// </summary>
        public float HighSpeed = 1;

        /// <summary>
        /// 登録されているキャラクター
        /// </summary>
        public Character Character;

        //////////////////////////////////////////////////
        //
        //    GameSceneで更新する項目
        //    外部クラスから設定不可能
        //

        /// <summary>
        /// JustShootの数
        /// </summary>
        public int JustShoot { get; private set; }

        /// <summary>
        /// Shootの数
        /// </summary>
        public int Shoot { get; private set; }

        /// <summary>
        /// Hitの数
        /// </summary>
        public int Hit { get; private set; }

        /// <summary>
        /// Missの数
        /// </summary>
        public int Miss { get; private set; }

        /// <summary>
        /// コンボ数
        /// </summary>
        public int Combo { get; private set; }

        /// <summary>
        /// 最大コンボ数
        /// </summary>
        public int BestCombo { get; private set; }

        /// <summary>
        /// 天井コンボ数
        /// </summary>
        public int MaxCombo { get; private set; }

        /// <summary>
        /// クリアゲージの値
        /// </summary>
        public int ClearPoint { get; internal set; }

        /// <summary>
        /// 点数
        /// </summary>
        public int Score
        {
            get
            {
                return (int)((100_0000 * Character.PointRate.JustShoot * JustShoot
                            + 100_0000 * Character.PointRate.Shoot * Shoot
                            + 100_0000 * Character.PointRate.Hit * Hit
                            + 100_0000 * Character.PointRate.Miss * Miss
                            + 100_0000 * Character.PointRate.Combo * BestCombo) / MaxCombo
                            + ClearPoint / (ClearPoint >= 70_0000 ? 1 : 2) * Character.PointRate.ClearPoint);
            }
        }

        /// <summary>
        /// kuriahantei
        /// </summary>
        public ClearJudge ClearJudge
        {
            get
            {
                if (ClearPoint < 70_0000) return ClearJudge.Failure;
                if (Miss == 0)
                {
                    if (Hit == 0)
                    {
                        if (Shoot == 0)
                        {
                            return ClearJudge.UltimateShoot;
                        }
                        return ClearJudge.AllShoot;
                    }
                    return ClearJudge.FullCombo;
                }
                return ClearJudge.Success;
            }
        }

        /// <summary>
        /// ランク
        /// </summary>
        public Rank Rank
        {
            get
            {
                if (Score < 50_0000) return Rank.F;
                if (Score < 60_0000) return Rank.E;
                if (Score < 70_0000) return Rank.D;
                if (Score < 80_0000) return Rank.C;
                if (Score < 85_0000) return Rank.B;
                if (Score < 90_0000) return Rank.A;
                if (Score < 95_0000) return Rank.S;
                if (Score < 98_0000) return Rank.SS;
                return Rank.SSS;
            }
        }

        /// <param name="controller"> コントローラー </param>
        public Player(Controller controller)
        {
            // プレイヤーとコントローラーの紐づけ
            Controller = controller;
        }

        /// <summary>
        /// プレイヤーの点数を初期化する
        /// </summary>
        public void Initialize(int max_combo)
        {
            JustShoot =
                Shoot =
                  Hit =
                 Miss =
                Combo =
            BestCombo = 0;

            MaxCombo = max_combo;

            ClearPoint = 0;

            switch(Character.GetType().Name)
            {
                case "Kanon":   Character = new Kanon(this);   break;
                case "Rimu":    Character = new Rimu(this);    break;
                case "Rintaro": Character = new Rintaro(this); break;
                case "Kakeru":  Character = new Kakeru(this);  break;
            }
        }

        /// <summary>
        /// プレイヤーを更新する
        /// </summary>
        /// <param name="judge"> 判定 </param>
        /// <param name="point"> ゲージに加算する値 </param>
        public void Update(Judge judge, int point)
        {
            switch (judge)
            {
                case Judge.JustShoot:
                    ++JustShoot; ++Combo;
                    break;
                case Judge.Shoot:
                    ++Shoot; ++Combo;
                    break;
                case Judge.Hit:
                    ++Hit; ++Combo;
                    break;
                case Judge.Miss:
                    ++Miss; Combo = 0;
                    break;
            }

            if (Combo > BestCombo) BestCombo = Combo;

            ClearPoint += point;

            if (ClearPoint > 100_0000) ClearPoint = 100_0000;
            if (ClearPoint < 0) ClearPoint = 0;
        }
    }
}
