namespace Minefield.Core {
    public class GameSettings {

        public int NumberOfLives { get; set; }

        public IMineLayingStrategy MineLayingStrategy { get; set; }

        public GameState? State { get; set; }


        public static GameSettings Default () {

            return new GameSettings { NumberOfLives = 3, MineLayingStrategy = new LayRandomMines() };            
        }

        public static GameSettings TestSettings_UnexplodedMinesEverywhere (int lives = 3) {

            return new GameSettings { NumberOfLives = lives, MineLayingStrategy = new SaturateBoardWithMinesStrategy() };            
        }

        public static GameSettings TestSettings_NoMines(int lives = 3, GameState? state = null)
        {

            return new GameSettings { NumberOfLives = lives, MineLayingStrategy = new NoMinesStrategy(), State = state};
        }

    }

}