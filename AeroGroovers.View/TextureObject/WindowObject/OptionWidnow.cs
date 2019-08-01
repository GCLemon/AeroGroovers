using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class OptionScene
    {
        private class OptionWidnow : Window
        {
            /// <summary>
            /// ウィンドウの状態
            /// </summary>
            private enum WindowState
            {
                /// <summary> 設定する項目を選んでいる </summary>
                Choosing,

                /// <summary> 項目を設定している </summary>
                Setting,

                /// <summary> 設定が終了している </summary>
                Finished
            }

            /// <summary>
            /// メニューの項目
            /// </summary>
            private enum MenuItem
            {
                /// <summary> 難易度 </summary>
                Difficulty,

                /// <summary> ノーツ速度 </summary>
                HighSpeed,

                /// <summary> キャラクター </summary>
                Character,

                /// <summary> 設定終了 </summary>
                Finish
            }

            /// <summary>
            /// テキストオブジェクトの中心座標
            /// </summary>
            private static Vector2DF center = new Vector2DF(0.5f, 0.5f);

            private AGText Difficulty_l = new AGText(24, 0, center) { Position = new Vector2DF(135, 90) };
            private AGText Difficulty_v = new AGText(32, 0, center) { Position = new Vector2DF(135, 120) };
            private AGText HighSpeed_l = new AGText(24, 0, center) { Position = new Vector2DF(135, 165) };
            private AGText HighSpeed_v = new AGText(32, 0, center) { Position = new Vector2DF(135, 195) };
            private AGText Character_l = new AGText(24, 0, center) { Position = new Vector2DF(135, 240) };
            private TextureObject2D Character_v = new TextureObject2D();
            private AGText Ready = new AGText(48, 0, center) { Position = new Vector2DF(135, 500) };

            /// <summary>
            /// 紐づいているプレイヤー
            /// </summary>
            private Player Player;

            /// <summary>
            /// 紐づいているプレイヤーのコントローラー
            /// </summary>
            private Controller Controller;

            /// <summary>
            /// ウィンドウの状態
            /// </summary>
            private WindowState CurrentState;

            /// <summary>
            /// メニューの項目
            /// </summary>
            private MenuItem CurrentItem;

            /// <summary>
            /// キャラクターのインデックス
            /// </summary>
            private int CharacterIndex;

            /// <summary>
            /// 難易度
            /// </summary>
            private Difficulty Difficulty = Difficulty.Novice;

            /// <summary>
            /// ノーツ速度
            /// </summary>
            private float HighSpeed = 1;

            /// <summary>
            /// キャラクター
            /// </summary>
            private Character Character;

            /// <summary>
            /// 設定が終了したか
            /// </summary>
            public bool IsFinished => CurrentState == WindowState.Finished;

            public OptionWidnow(int player_number) : base(270, 540)
            {
                // プレイヤーを設定
                Player = Game.Player[player_number - 1];
                Controller = (Controller)Player.Controller;

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

                AGText player_l = new AGText(36, 0, center) { Position = new Vector2DF(135, 40) };
                player_l.SetText("Player " + player_number);

                // オブジェクトを追加
                AddObjects
                (
                    player_l,
                    Difficulty_l,
                    Difficulty_v,
                    HighSpeed_l,
                    HighSpeed_v,
                    Character_l,
                    Character_v,
                    Ready
                );

                if (Player.Character == null)
                {
                    Player.Character = new Kanon(Player);
                    CharacterIndex = 0;
                }
                else
                {
                    switch (Player.Character.GetType().Name)
                    {
                        case "Kanon": CharacterIndex = 0; break;
                        case "Rimu": CharacterIndex = 1; break;
                        case "Rintaro": CharacterIndex = 2; break;
                        case "Kakeru": CharacterIndex = 3; break;
                    }
                }

                // テキストの設定
                Difficulty_l.SetText("< Difficulty >");
                HighSpeed_l.SetText("< High Speed >");
                Character_l.SetText("< Artifact >");
                Ready.SetText("READY");

                Difficulty_v.SetText(Player.Difficulty.ToString());
                HighSpeed_v.SetText(Player.HighSpeed.ToString());

                // キャラクターの画像を設定する
                Character_v.Texture = Engine.Graphics.CreateTexture2D(Player.Character.Image);
                Character_v.CenterPosition = Character_v.Texture.Size.To2DF() * 0.5f;
                Character_v.Position = new Vector2DF(135, 355);

                Character = Player.Character;
            }

            protected override void OnUpdate()
            {
                base.OnUpdate();

                // テキストの色を変更する
                void SetTextColor(AGText text, MenuItem item)
                {
                    switch (CurrentState)
                    {
                        case WindowState.Choosing:
                            text.Color =
                                CurrentItem == item
                                ? new Color(255, 255, 255)
                                : new Color(63, 63, 63);
                            break;
                        case WindowState.Setting:
                            text.Color =
                                CurrentItem == item
                                ? new Color(255, 255, 0)
                                : new Color(63, 63, 63);
                            break;
                        case WindowState.Finished:
                            text.Color = new Color(63, 63, 63);
                            break;
                    }
                }

                switch (CurrentState)
                {
                    case WindowState.Choosing:
                        OnChoosing();
                        break;
                    case WindowState.Setting:
                        switch (CurrentItem)
                        {
                            case MenuItem.Difficulty: OnSettingDifficulty(); break;
                            case MenuItem.HighSpeed: OnSettingHighSpeed(); break;
                            case MenuItem.Character: OnSettingCharacter(); break;
                        }
                        break;
                    case WindowState.Finished:
                        OnFinished();
                        break;
                }

                SetTextColor(Difficulty_l, MenuItem.Difficulty);
                SetTextColor(Difficulty_v, MenuItem.Difficulty);
                SetTextColor(HighSpeed_l, MenuItem.HighSpeed);
                SetTextColor(HighSpeed_v, MenuItem.HighSpeed);
                SetTextColor(Character_l, MenuItem.Character);

                SetTextColor(Ready, MenuItem.Finish);

                // 難易度の表示を変更
                Difficulty difficulty = Player.Difficulty;
                int level = Game.Score.Level[difficulty];
                Difficulty_v.SetText(difficulty.ToString() + " : Lv." + level);

                // ノーツ速度の表示を変更
                HighSpeed_v.SetText("× " + Player.HighSpeed.ToString("0.0"));

                // キャラクター画像を変更
                Character_v.Texture = Engine.Graphics.CreateTexture2D(Player.Character.Image);
            }

            /// <summary>
            /// 設定項目を選んでいるときの処理
            /// </summary>
            private void OnChoosing()
            {
                Player.Initialize(Game.Score.Notes[Player.Difficulty].Count);

                // 上ボタンが押された時
                if (Controller.GetPush(Button.Up))
                {
                    int index = ((int)CurrentItem - 1) % 4;
                    if (index < 0) index += 4;
                    CurrentItem = (MenuItem)index;
                }

                // 下ボタンが押された時
                if (Controller.GetPush(Button.Down))
                {
                    int index = ((int)CurrentItem + 1) % 4;
                    if (index < 0) index += 4;
                    CurrentItem = (MenuItem)index;
                }

                // Aボタンが押された時
                if (Controller.GetPush(Button.A))
                {
                    CurrentState =
                        CurrentItem == MenuItem.Finish
                        ? WindowState.Finished
                        : WindowState.Setting;
                }

                // Bボタンが押された時
                if (Controller.GetPush(Button.B))
                {
                    Sound.Stop(BGM_ID);
                    Sound.Play(((Scene)Layer.Scene).SE_Cancel);
                    Engine.ChangeSceneWithTransition(new SelectScene(), new TransitionFade(1, 1));
                }
            }

            /// <summary>
            /// 難易度を設定しているときの処理
            /// </summary>
            private void OnSettingDifficulty()
            {
                // 左ボタンが押された時
                if (Controller.GetPush(Button.Left))
                    if (Player.Difficulty != Difficulty.Novice) --Player.Difficulty;

                // 右ボタンが押された時
                if (Controller.GetPush(Button.Right))
                    if (Player.Difficulty != Difficulty.Expert) ++Player.Difficulty;

                // Aボタンが押された時
                if (Controller.GetPush(Button.A))
                {
                    Difficulty = Player.Difficulty;
                    CurrentState = WindowState.Choosing;
                }

                // Bボタンが押された時
                if (Controller.GetPush(Button.B))
                {
                    Player.Difficulty = Difficulty;
                    CurrentState = WindowState.Choosing;
                }
            }

            /// <summary>
            /// ノーツ速度を設定しているときの処理
            /// </summary>
            private void OnSettingHighSpeed()
            {
                // 左ボタンが押された時
                if (Controller.GetPush(Button.Left))
                    if (Player.HighSpeed > 1.0f) Player.HighSpeed -= 0.5f;

                // 右ボタンが押された時
                if (Controller.GetPush(Button.Right))
                    if (Player.HighSpeed < 20.0f) Player.HighSpeed += 0.5f;

                // Aボタンが押された時
                if (Controller.GetPush(Button.A))
                {
                    HighSpeed = Player.HighSpeed;
                    CurrentState = WindowState.Choosing;
                }

                // Bボタンが押された時
                if (Controller.GetPush(Button.B))
                {
                    Player.HighSpeed = HighSpeed;
                    CurrentState = WindowState.Choosing;
                }
            }

            /// <summary>
            /// キャラクターを設定しているときの処理
            /// </summary>
            private void OnSettingCharacter()
            {
                // 左ボタンが押された時
                if (Controller.GetPush(Button.Left))
                {
                    --CharacterIndex;
                    CharacterIndex %= 4;
                    if (CharacterIndex < 0) CharacterIndex += 4;
                }

                // 右ボタンが押された時
                if (Controller.GetPush(Button.Right))
                {
                    ++CharacterIndex;
                    CharacterIndex %= 4;
                    if (CharacterIndex < 0) CharacterIndex += 4;
                }

                // Aボタンが押された時
                if (Controller.GetPush(Button.A))
                {
                    Character = Player.Character;
                    CurrentState = WindowState.Choosing;
                }

                // Bボタンが押された時
                if (Controller.GetPush(Button.B))
                {
                    Player.Character = Character;
                    switch (Player.Character.GetType().Name)
                    {
                        case "Kanon": CharacterIndex = 0; break;
                        case "Rimu": CharacterIndex = 1; break;
                        case "Rintaro": CharacterIndex = 2; break;
                        case "Kakeru": CharacterIndex = 3; break;
                    }
                    CurrentState = WindowState.Choosing;
                }

                // CharacterIndexごとにキャラクターを変える
                switch (CharacterIndex)
                {
                    case 0: Player.Character = new Kanon(Player); break;
                    case 1: Player.Character = new Rimu(Player); break;
                    case 2: Player.Character = new Rintaro(Player); break;
                    case 3: Player.Character = new Kakeru(Player); break;
                }
            }

            /// <summary>
            /// 設定を終了したときの処理
            /// </summary>
            private void OnFinished()
            {
                // 戻る
                if (Controller.GetPush(Button.B))
                    CurrentState = WindowState.Choosing;
            }
        }
    }
}