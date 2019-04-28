﻿using System;
using System.Linq;

namespace Minefield.Core
{
    public class Game {
        public Board Board { get; set; }
        public Player Player { get; set; }

        private GameSettings _settings;

        public Game (GameSettings settings = null) {
            
            if(settings == null) {
                settings = new DefaultGameSettings();
            }
            
            _settings = settings;

            InitialiseTheGame();

            
        }
        
        private void InitialiseTheGame(){

        Board = new Board (strategy: _settings.MineLayingStrategy);
            
            Player = new Player(lives:_settings.NumberOfLives)
            {
                 Position = Board.StartPosition()
            };

        }

        public void MovePlayer(Direction direction){            

            Player.Position = Board.NewPositionRelativeTo(direction, from: Player.Position);
                                    
            Player.IncrementMoveCount();

            if(PlayerHasLandedOnMine()){
                System.Console.WriteLine("BOOM!");
                Player.LoseALife();
            }

        }

        private bool PlayerHasLandedOnMine()
        {
            return Board.Mines.Contains(Player.Position);
            
        }

        public bool GameHasBeenWon()
        {
            return Player.Position.Column == Board.ColumnNames.ToArray()[Board.Columns-1];
        }

        public bool IsOver()
        {
            return Player.Lives == 0;
        }

        public bool PlayerIsAtStart()
        {
            return Player.Position == Board.StartPosition();
        }

        public void Restart()
        {
            InitialiseTheGame();
        }
    }

    public class GameSettings{

        public int NumberOfLives { get; set; }

        public IMineLayingStrategy MineLayingStrategy { get; set; }

    }

    public class DefaultGameSettings : GameSettings {
        public DefaultGameSettings()
        {
            NumberOfLives = 3;
            MineLayingStrategy = new LayRandomMines();
        }
    }

}