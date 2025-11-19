using Lab2_8;

namespace Lab3_2
{
    public partial class Form1 : Form
    {
        List<Pilot> pilots = new List<Pilot>();
        List<Passenger> passes = new List<Passenger>();
        List<Airplane> airplanes = new List<Airplane>();
        List<Flight> flights = new List<Flight>();

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                string birthDate = textBox2.Text;
                string licenseNumber = textBox3.Text;
                int flightHourse = Int32.Parse(textBox4.Text);

                pilots.Add(new Pilot(name, birthDate, licenseNumber, flightHourse));
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox8.Text;
            string birthDate = textBox7.Text;
            string passportNumber = textBox6.Text;

            passes.Add(new Passenger(name, birthDate, passportNumber));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string model = textBox12.Text;
            int capasity = Int32.Parse(textBox11.Text);
            int range = Int32.Parse(textBox10.Text);

            airplanes.Add(new Airplane(model, capasity, range));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox1.Items.Clear();
            //foreach(Airplane airplane in airplanes)
            //{
            //    comboBox1.Items.Add(airplane.Model);
            //}
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox2.Items.Clear();
            //comboBox2.Items.AddRange(pilots);
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (Person p in pilots)
            {
                comboBox2.Items.Add(p.Name);
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (Airplane airplane in airplanes)
            {
                comboBox1.Items.Add(airplane.Model);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string flightNumber = textBox16.Text;
            string origin = textBox15.Text;
            string destination= textBox14.Text;
            DateTime departureTime = DateTime.Now
                .AddHours(Int32.Parse(textBox13.Text));

            string airplaneModel=comboBox1.Text;
            string pilotName = comboBox2.Text;

            Airplane selectedAirplane;
            foreach (Airplane airplane in airplanes) {

            }

            flights.Add(new Flight(
                flightNumber, origin, destination, departureTime,
                airplanes.First(a => a.Model == airplaneModel)),
                pilots.First(p => p.Name == pilotName),passes);
        }
    }
}
