using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 
            //FileStream lector = new FileStream("agenda.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            //while (lector.Length > lector.Position)
            //{
            //    Console.Write((char)lector.ReadByte());
            //}
            #endregion

            //leer();
            //escribir();
            //leer();

            Console.WriteLine("Presione una tecla para generar un archivo agendaXML.xml con los datos de agenda.txt");
            Console.ReadKey();
            escribirXML();
            Console.WriteLine("Archivo agendaXML.xml generado correctamente. Presione una tecla para ver su contenido");
            Console.ReadKey();
            Console.WriteLine();
            leerXML();

            Console.ReadKey();


        }

        private static void leer()
        {
            StreamReader lector = File.OpenText("agenda.txt");
            string linea;
            Console.WriteLine("Nombre\tApellido\te-mail\t\t\tTelefono");
            do
            {
                linea = lector.ReadLine();
               
                if (linea != "" && linea != null)
                {
                    string[] valores = linea.Split(';');
                    
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", valores[0], valores[1], valores[2], valores[3]);
                }
            } while (linea != null);

            lector.Close();
        }

        private static void escribir()
        {
            StreamWriter escritor = File.AppendText("agenda.txt");
            Console.WriteLine("Ingrese nuevo contacto");
            string rta = "s";
            while (rta == "s")
            {
                Console.WriteLine("Ingrese nombre");
                string nombre = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Ingrese apellido");
                string apellido = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Ingrese e-mail");
                string email = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Ingrese telefono");
                string telefono = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();


                escritor.WriteLine(nombre + ";" + apellido + ";" + email + ";" + telefono);
                Console.Write("Desea ingresar otro contacto? (s/n)");
                rta = Console.ReadLine(); 
            } 

            escritor.Close();
        }

        private static void escribirXML()
        {
            XmlTextWriter escritorXML = new XmlTextWriter("agendaXML.xml", null);
            escritorXML.Formatting = Formatting.Indented; //no es necesario, pero se hace mas facil leer el contenido 11
            escritorXML.WriteStartDocument(true);
            escritorXML.WriteStartElement("DocumentElement"); //este elemento no es necesario, es para compatibilidad con los xml generados
            StreamReader lector = File.OpenText("agenda.txt");
            string linea;

            do
            {
                linea = lector.ReadLine();
                if (linea != null && linea != "")
                {
                    string[] valores = linea.Split(';');
                    escritorXML.WriteStartElement("contactos");
                    escritorXML.WriteStartElement("nombre");
                    escritorXML.WriteValue(valores[0]);
                    escritorXML.WriteEndElement(); //cerramos el tag del nombre
                    escritorXML.WriteStartElement("apellido");
                    escritorXML.WriteValue(valores[1]);
                    escritorXML.WriteEndElement(); //cerramos el tag del apellido
                    escritorXML.WriteStartElement("email");
                    escritorXML.WriteValue(valores[2]);
                    escritorXML.WriteEndElement();  //cerramos el tag del email
                    escritorXML.WriteStartElement("telefono");
                    escritorXML.WriteValue(valores[3]);
                    escritorXML.WriteEndElement(); //cerramos el tag del telefono
                    escritorXML.WriteEndElement(); //cerramos el tag del contactos
                }                 
            } while (linea != null);

            escritorXML.WriteEndElement(); //cerramos el tag de DocumentElement 
            escritorXML.WriteEndDocument(); //
            escritorXML.Close();

            lector.Close();
        }

        private static void leerXML()
        {
            XmlTextReader lectorXML = new XmlTextReader("agendaXML.xml");
            string tagAnterior = "";
            while (lectorXML.Read())
            {
                if (lectorXML.NodeType == XmlNodeType.Element)
                {
                    tagAnterior = lectorXML.Name;
                }
                else if (lectorXML.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine(tagAnterior + ": " + lectorXML.Value);
                }
            }
            lectorXML.Close();
        }

    }
}
