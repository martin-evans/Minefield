namespace Minefield.Core {
    public class GameSettings {

        public int NumberOfLives { get; set; }

        public int ExplosionLengthMs { get; set; }

        public IMineLayingStrategy MineLayingStrategy { get; set; }


        public static GameSettings Default () {

            return new GameSettings { NumberOfLives = 3, MineLayingStrategy = new LayRandomMines(), ExplosionLengthMs = 1500 };            
        }

        public static GameSettings UnexplodedMinesEverywhere (int lives = 3) {

            return new GameSettings { NumberOfLives = lives, MineLayingStrategy = new SaturateBoardWithMinesStrategy(), ExplosionLengthMs=1000 };            
        }

        public static GameSettings ExplodedMinesEverywhere(int lives = 3)
        {

            return new GameSettings { NumberOfLives = lives, MineLayingStrategy = new SaturateBoardWithMinesStrategy(), ExplosionLengthMs = 1000};
        }

    }

}