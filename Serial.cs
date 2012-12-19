using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace Serial
{
    public partial class Form1 : Form
    {

        static bool _continue = true;
        static SerialPort _serialPort = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string name;
            string command;
            string[] portas;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            Thread t = new Thread(Read);

            portas = SerialPort.GetPortNames();            

            foreach (string porta in portas)
            {
                PortasLista.Items.Add(porta);
            }
            
            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM3";
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = 0;
            _serialPort.DataBits = 8;

            // Set the read/write timeouts
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();
            t.Start();

            while (_continue)
            {
                name = Console.ReadLine();
                if(stringComparer.Equals("sair", name)) _continue = false;              

                if (!stringComparer.Equals("+++", name))
                {
                    command = name + "\r\n";
                    _serialPort.Write(command);
                }
                
                _serialPort.Write(name);
            }

            t.Join();
            _serialPort.Close();
        }

        public void Read()
        {
            Form1 objeto = new Form1();
            byte[] buffer = new byte[1];
            int value;

            while (_continue)
            {
                try
                {
                    value = _serialPort.Read(buffer, 0, 1);
                    string va = ASCIIEncoding.ASCII.GetString(buffer);
                        
                        this.textBox1.Invoke(new Action(() =>
                        {
                            this.textBox1.AppendText(va + " ");
                        }));

                }
                
                catch (TimeoutException) { }
            }
        }

    }

}

