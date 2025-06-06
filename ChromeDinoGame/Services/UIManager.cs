﻿using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Services
{
    public class UIManager
    {
        private TextBlock _scoreBlock = new TextBlock
        {
            Text = "",
            FontSize = 16,
            Foreground = Brushes.DarkSlateGray,
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(730, 10, 0, 0)
        };
        private TextBlock _startInfBlock = new TextBlock
        {
            Text = "Reach 1000000 points to complete the game\nPress ENTER to start",
            FontSize = 18,
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Gray,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(310, 150, 0, 0)
        };
        private TextBlock _pauseBlock = new TextBlock
        {
            Text = "[P] Pause / Resume",
            FontSize = 16,
            Foreground = Brushes.DarkSlateGray,
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(560, 10, 0, 0)
        };
        private TextBlock _replayInfBlock = new TextBlock
        {
            Text = "Press ENTER to replay",
            FontSize = 18,
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Gray,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(400, 170, 0, 0)
        };
        private TextBlock _victoryBlock = new TextBlock
        {
            Text = "🎉 Congratulations! You’ve completed the Dino Run! 🎉\nFinal Score: 100000",
            FontSize = 20,
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Gold,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(225, 140, 0, 0)
        };

        public void UpdateScoreBlock(double score, double highestScore)
        {
            _scoreBlock.Text = $"HI {(int)highestScore}         score: {(int)score}";

            if (!GlobalCanvas.GameArea.Children.Contains(_scoreBlock))
            {
                GlobalCanvas.GameArea.Children.Add(_scoreBlock);
                Panel.SetZIndex(_scoreBlock, 10);
            }
        }

        public void UpdateReplayInfoBlock(bool visibility) => UpdateinfoMessageBlock(_replayInfBlock, visibility);

        public void UpdateStartInfoBlock(bool visibility) => UpdateinfoMessageBlock(_startInfBlock, visibility);

        public void DisplayPauseInfBlock() => UpdateinfoMessageBlock(_pauseBlock, true);

        public void DisplayVictoryBlock()
        {
            GlobalCanvas.GameArea.Children.Add(_victoryBlock);
            Panel.SetZIndex(_scoreBlock, 10);
        }

        private void UpdateinfoMessageBlock(TextBlock block, bool visibility)
        {
            if (!GlobalCanvas.GameArea.Children.Contains(block))
            {
                GlobalCanvas.GameArea.Children.Add(block);
                Panel.SetZIndex(block, 10);
            }

            if (visibility)
                block.Visibility = Visibility.Visible;
            else
                block.Visibility = Visibility.Hidden;
        }
    }
}
