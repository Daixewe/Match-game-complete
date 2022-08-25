using System.Media;
namespace matchGame

{
    public partial class Form1 : Form

    {
        private Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!","!","N","N",
            ",",",","k","k",
            "b","b","v","v",
            "w","w","z","z"
        };
        Label? firstCliked = null;
        Label? secondCliked = null;
        public Form1()
        {
            InitializeComponent();
            AssignIconstToSquares();
        }

        SoundPlayer sonido = new SoundPlayer();

        private void AssignIconstToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }

            }
            timer2.Start();
        }
        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                return;
            }
            Label? clikedLabel = sender as Label;
            if (clikedLabel != null)
            {
                if (clikedLabel.ForeColor == Color.Black)
                {
                    return;

                }
                if (firstCliked == null)
                {
                    firstCliked = clikedLabel;
                    firstCliked.ForeColor = Color.Black;
                    return;
                }
                secondCliked = clikedLabel;
                secondCliked.ForeColor = Color.Black;
                CheckForWinner();
                if (firstCliked.Text == secondCliked.Text)
                {
                    sonido.Stream = Properties.Resource1.correcto;
                    sonido.Play();
                    firstCliked = null;
                    secondCliked = null;

                    
                    return;
                }
                sonido.Stream = Properties.Resource1.incorrecto;
                sonido.Play();
                timer1.Start();
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstCliked.ForeColor = firstCliked.BackColor;
            secondCliked.ForeColor = secondCliked.BackColor;
            firstCliked = null;
            secondCliked = null;

        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label? iconLabel = control as Label;
                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }

                }
            }
            timer2.Stop();
            MessageBox.Show("You won the game " + myTimer);
            Close();
        }

        private int i = 0;

        private string myTimer;

        private void timer2_Tick(object sender, EventArgs e)
        {
            i++;
            myTimer = "Match duration " + i.ToString() + " seconds";
        }
    }
}
