namespace AeroGroovers.Model
{
    public partial class Rintaro
    {
        /// <summary>
        /// 判定易化スキル : エクリプス
        /// </summary>
        private class Eclipse : Skill
        {
            /// <summary>
            /// スキルの種類
            /// </summary>
            public override SkillType SkillType =>
                SkillType.Support;

            /// <summary>
            /// スキルの発動要件
            /// </summary>
            protected override bool SkillState =>
                SkillPoint > 0;

            /// <summary>
            /// <para> コンボごとにインクリメント </para>
            /// <para> コンボが途切れたりスキルポイントがたまると0になる </para>
            /// </summary>
            private int SkillCount;

            /// <param name="character"> 紐づけるキャラクター </param>
            public Eclipse(Character character)
                : base(character)
            {
                SkillPoint = 0;
                SkillPoint_Max = 10;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                if (judge == Judge.Miss && SkillState)
                {
                    System.Console.WriteLine(SkillPoint);
                    Trigger();
                    base.Update(judge);
                    return true;
                }

                else if (judge != Judge.Miss)
                {
                    System.Console.WriteLine(SkillPoint);
                    ++SkillCount;
                    if (SkillCount == 70)
                    {
                        ++SkillPoint;
                        SkillCount = 0;
                    }
                    base.Update(judge);
                    return false;
                }

                else
                {
                    SkillCount = 0;
                    return false;
                }
            }

            /// <summary>
            /// スキルが発動した時の動作
            /// </summary>
            protected override void Trigger()
            {
                --SkillPoint;

                base.Trigger();
            }
        }

        /// <summary>
        /// ステータスアップスキル : プロミネンス
        /// </summary>
        private class Prominence : Skill
        {
            /// <summary>
            /// スキルの種類
            /// </summary>
            public override SkillType SkillType =>
                SkillType.Boost;

            /// <summary>
            /// スキルの発動要件
            /// </summary>
            protected override bool SkillState =>
                SkillPoint >= SkillPoint_Max;

            /// <param name="character"> 紐づけるキャラクター </param>
            public Prominence(Character character)
                : base(character)
            {
                SkillPoint = 0;
                SkillPoint_Max = 25;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                // 最大コンボ数を更新しているときに加算
                var player = Character.Player;
                if (player.BestCombo == player.Combo)
                {
                    switch (judge)
                    {
                        case Judge.JustShoot:
                        case Judge.Shoot:
                        case Judge.Hit: ++SkillPoint; break;
                    }
                }

                // スキル発動条件を満たした時の処理
                if (SkillState)
                {
                    Trigger();
                    SkillPoint -= SkillPoint_Max;
                }

                return base.Update(judge);
            }

            /// <summary>
            /// スキルが発動した時の動作
            /// </summary>
            protected override void Trigger()
            {
                ((Rintaro)Character).GaugeAdd += 320;
                base.Trigger();
            }
        }

        /// <summary>
        /// クリア妨害スキル : フレア
        /// </summary>
        private class Flare : Skill
        {
            /// <summary>
            /// スキルの種類
            /// </summary>
            public override SkillType SkillType =>
                SkillType.Attack;

            /// <summary>
            /// スキルの発動要件
            /// </summary>
            protected override bool SkillState =>
                SkillPoint >= SkillPoint_Max;

            /// <param name="character"> 紐づけるキャラクター </param>
            public Flare(Character character)
                : base(character)
            {
                SkillPoint = 0;
                SkillPoint_Max = 30;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                // コンボを繋げるとポイントを加算
                switch (judge)
                {
                    case Judge.JustShoot:
                    case Judge.Shoot:
                    case Judge.Hit: ++SkillPoint; break;
                    case Judge.Miss: SkillPoint = 0; break;
                }

                // スキル発動条件を満たした時の処理
                if (SkillState)
                {
                    Trigger();
                    SkillPoint -= SkillPoint_Max;
                }

                return base.Update(judge);
            }

            /// <summary>
            /// スキルが発動した時の動作
            /// </summary>
            protected override void Trigger()
            {
                foreach (var player in Game.Player)
                    if (player != null && player != Character.Player)
                        player.Character.Damage(10_0000);

                base.Trigger();
            }
        }
    }
}
