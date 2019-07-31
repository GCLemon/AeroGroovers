namespace AeroGroovers.Model
{
    public partial class Rimu
    {
        /// <summary>
        /// ゲージ回復スキル : 天照
        /// </summary>
        private class Amaterasu : Skill
        {
            /// <summary>
            /// スキルの種類
            /// </summary>
            public override SkillType SkillType =>
                SkillType.Heal;

            /// <summary>
            /// スキルの発動要件
            /// </summary>
            protected override bool SkillState =>
                SkillPoint >= SkillPoint_Max;

            /// <param name="character"> 紐づけるキャラクター </param>
            public Amaterasu(Character character)
                : base(character)
            {
                SkillPoint = 0;
                SkillPoint_Max = 20;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                // Shoot以上でポイントを加算
                switch (judge)
                {
                    case Judge.JustShoot:
                    case Judge.Shoot: ++SkillPoint; break;
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
                Character.Player.ClearPoint += 5_0000;

                base.Trigger();
            }
        }

        private class Tsukuyomi : Skill
        {
            /// <summary>
            /// スキルの種類
            /// </summary>
            public override SkillType SkillType =>
                SkillType.Heal;

            /// <summary>
            /// スキルの発動要件
            /// </summary>
            protected override bool SkillState =>
                SkillPoint >= SkillPoint_Max;

            /// <param name="character"> 紐づけるキャラクター </param>
            public Tsukuyomi(Character character)
                : base(character)
            {
                SkillPoint = 0;
                SkillPoint_Max = 35;
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
            /// すきろが発動した時の動作
            /// </summary>
            protected override void Trigger()
            {
                Character.Player.ClearPoint += 5_0000;

                base.Trigger();
            }
        }

        private class Susanoo : Skill
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
            public Susanoo(Character character)
                : base(character)
            {
                SkillPoint = 0;
                SkillPoint_Max = 40;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                // JustShootでポイントを加算
                switch (judge)
                {
                    case Judge.JustShoot: ++SkillPoint; break;
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
                        player.Character.Damage((int)(Character.Player.ClearPoint * 0.12));

                base.Trigger();
            }
        }
    }
}
