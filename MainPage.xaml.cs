using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Plugin.Maui.Audio;

namespace TicTacToeMaui
{
    public partial class MainPage : ContentPage
    {
        private string currentPlayer;
        private string[,] board;
        private bool gameEnded;
        private IAudioPlayer _audioPlayer;

        public MainPage()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            currentPlayer = "🦈";
            board = new string[3, 3];
            gameEnded = false;
            statusLabel.Text = "Player 🦈's turn";
            ResetButtons();
        }

        private void ResetButtons()
        {
            btn00.Text = btn01.Text = btn02.Text = "";
            btn10.Text = btn11.Text = btn12.Text = "";
            btn20.Text = btn21.Text = btn22.Text = "";
            // Reset button background color to default (white)
            btn00.Background = btn01.Background = btn02.Background =
            btn10.Background = btn11.Background = btn12.Background =
            btn20.Background = btn21.Background = btn22.Background = Colors.Blue;
            btn00.IsEnabled = btn01.IsEnabled = btn02.IsEnabled = true;
            btn10.IsEnabled = btn11.IsEnabled = btn12.IsEnabled = true;
            btn20.IsEnabled = btn21.IsEnabled = btn22.IsEnabled = true;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (gameEnded) return;

            var button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (string.IsNullOrEmpty(board[row, col]))
            {
                board[row, col] = currentPlayer;
                button.Text = currentPlayer;

                if (currentPlayer == "🦈")
                {
                    button.Background = Colors.DarkGray;  // Color for player 🦈
                    /* var stream = GetType().Assembly.GetManifestResourceStream("TicTacToeMaui.Resources.sword-sound-effect-1.mp3");
                    if (stream != null)
                    {
                        _audioPlayer = AudioManager.Current.CreatePlayer(stream);  // Use the stream to create the player
                        _audioPlayer.Play();  // Play the sound synchronously
                    } */
                }
                else
                {
                    button.Background = Colors.LightBlue; // Color for player 🐬
                    /* var stream = GetType().Assembly.GetManifestResourceStream("TicTacToeMaui.Resources.sword-sound-effect-2.mp3");
                    if (stream != null)
                    {
                        _audioPlayer = AudioManager.Current.CreatePlayer(stream);  // Use the stream to create the player
                        _audioPlayer.Play();  // Play the sound synchronously
                    } */
                }

                if (CheckForWinner())
                {
                    statusLabel.Text = $"Player {currentPlayer} wins!";
                    gameEnded = true;
                    DisableAllButtons();
                }
                else if (CheckForDraw())
                {
                    statusLabel.Text = "It's a draw!";
                    gameEnded = true;
                }
                else
                {
                    SwitchPlayer();
                }
            }
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == "🦈" ? "🐬" : "🦈";
            statusLabel.Text = $"Player {currentPlayer}'s turn";
        }

        private bool CheckForWinner()
        {
            // Check rows, columns, and diagonals for a win
            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(board[i, 0]) && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
                if (!string.IsNullOrEmpty(board[0, i]) && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }
            if (!string.IsNullOrEmpty(board[0, 0]) && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;
            if (!string.IsNullOrEmpty(board[0, 2]) && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;

            return false;
        }

        private bool CheckForDraw()
        {
            foreach (var cell in board)
            {
                if (string.IsNullOrEmpty(cell))
                    return false;
            }
            return true;
        }

        private void DisableAllButtons()
        {
            btn00.IsEnabled = btn01.IsEnabled = btn02.IsEnabled = false;
            btn10.IsEnabled = btn11.IsEnabled = btn12.IsEnabled = false;
            btn20.IsEnabled = btn21.IsEnabled = btn22.IsEnabled = false;
        }

        private void OnRestartClicked(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}