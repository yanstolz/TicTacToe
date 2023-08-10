using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private enum Player { None, Cross, Circle }

        private Player currentPlayer = Player.Cross;
        private Player[,] board = new Player[3, 3];

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = Player.None;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (board[row, col] == Player.None)
            {
                board[row, col] = currentPlayer;
                button.Content = currentPlayer == Player.Cross ? "X" : "O";
                currentPlayer = currentPlayer == Player.Cross ? Player.Circle : Player.Cross;

                CheckForWin();
                CheckForDraw();
            }
        }

        private void CheckForWin()
        {
            // Проверка выигрышных комбинаций
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != Player.None && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    ShowWinnerMessage(board[i, 0]);
                    return;
                }

                if (board[0, i] != Player.None && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    ShowWinnerMessage(board[0, i]);
                    return;
                }
            }

            if (board[0, 0] != Player.None && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                ShowWinnerMessage(board[0, 0]);
                return;
            }

            if (board[0, 2] != Player.None && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                ShowWinnerMessage(board[0, 2]);
                return;
            }
        }

        private void ShowWinnerMessage(Player winner)
        {
            string win = (winner == Player.Cross ? "Победа крестиков" : "Победа ноликов");
            MessageBox.Show(win);
            ResetGame();
        }

        private void CheckForDraw()
        {
            bool isDraw = true;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == Player.None)
                    {
                        isDraw = false;
                        break;
                    }
                }

                if (!isDraw)
                {
                    break;
                }
            }

            if (isDraw)
            {
                MessageBox.Show("Ничья");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            InitializeGame();

            foreach (var button in GameGrid.Children)
            {
                if (button is Button gameButton)
                {
                    gameButton.Content = "";
                }
            }

            currentPlayer = Player.Cross;
        }
    }
}
