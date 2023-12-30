namespace Pairs
{
    public partial class Pairs : Form
    {
        public Pairs()
        {
            InitializeComponent();
            Button[] buttons = new Button[16];
            int i = 0;
            foreach (Control c in this.Controls) // Makes an array of all buttons/cards.
            {
                if (c is Button)
                {
                    buttons[i] = c as Button;
                    i++;
                }
            }
            Random randomNumberGenerator = new Random();
            int arrayLength = buttons.Length;
            for (int j = arrayLength - 1; j >= 0; j--) // Random picture position (= Text property) assignment.
            {
                string originalText = buttons[j].Text;
                int newButtonPosition = randomNumberGenerator.Next(j + 1);
                string newText = buttons[newButtonPosition].Text;
                buttons[j].Text = newText;
                buttons[newButtonPosition].Text = originalText;
            }
        }

        private Button Card1;
        private Button Card2;

        private string card1Text = string.Empty; // = picture (Text property in Wingdings)
        private string card2Text = string.Empty; // = picture (Text property in Wingdings)
        private int numberOfMatchedCards = 0; // When every card is matched, a message box appears (see below).
        private int numberOfMoves = 0;
        int timeElapsed;

        private void IncreaseNumberOfMoves() // Increases the number of guesses/moves and updates the corresponding label.
        {
            numberOfMoves += 1;
            labelNumberOfMoves.Text = numberOfMoves.ToString();
        }
        private bool DoCardsMatch(string card1Text, string card2Text) // Compares two selected cards.
        {
            return card1Text == card2Text;
        }

        private void button_Click(object sender, EventArgs e)
        {
            timer.Enabled = true; // Time starts after the first button is clicked, not when the program starts.
            if (card1Text == string.Empty && card2Text == string.Empty)
            {
                if (Card1 is not null && Card2 is not null) // If the cards don't match, they "turn over".
                {
                    Card1.ForeColor = Color.CornflowerBlue;
                    Card2.ForeColor = Color.CornflowerBlue;
                }
                Card1 = ((Button)sender);
                Card1.ForeColor = Color.Black;
                card1Text = Card1.Text;
            }
            else if (card2Text == string.Empty && Card1 != ((Button)sender)) // Otherwise the card matches with itself.
            {
                Card2 = ((Button)sender);
                Card2.ForeColor = Color.Black;
                card2Text = Card2.Text;
                IncreaseNumberOfMoves();
                if (DoCardsMatch(card1Text, card2Text)) // When matched, the cards stay turned over (the pictures are visible) and the buttons are deactivated.
                {
                    Card1.Enabled = false;
                    Card2.Enabled = false;
                    numberOfMatchedCards += 2;
                }
                if (numberOfMatchedCards == 16) // 16 = all matched
                {
                    timer.Stop();
                    MessageBox.Show($"Congratulations! You found all pairs in {timeElapsed} seconds and {numberOfMoves} moves!", "Congratulations", MessageBoxButtons.OK);
                }
                card1Text = string.Empty; // The text variables (pictures) are reset so that a new pair of values can be set.
                card2Text = string.Empty;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            labelTimeElapsed.Text = timeElapsed.ToString();
        }
    }
}
