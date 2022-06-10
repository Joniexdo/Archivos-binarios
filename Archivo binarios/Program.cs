using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Archivo_binarios
{
    class Program
    {
        class ArchivoBinarioEmpleados
        {
            //declaracion de flujos
            BinaryWriter bw = null; //flujo salida - escritura de datos
            BinaryReader br = null; //flujo entrada - lectura de datos

            //campos de la clase
            string Nombre, Direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float SalarioDiario;

            public void CrearArchivo(string Archivo)
            {
                //variable local metodo
                char resp;

                try
                {
                    //creacion del flujo para escribir datos al archivo
                    bw = new BinaryWriter(new FileStream
                        (Archivo, FileMode.Create, FileAccess.Write));

                    //Ciclo para captura de datos
                    do
                    {
                        //Limpieza de pantalla
                        Console.Clear();

                        //Captura de datos
                        Console.Write("Numero del Empleado: ");
                        NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del Empleado: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Direccion del Empleado: ");
                        Direccion = Console.ReadLine();
                        Console.Write("Telefono del Empleado: ");
                        Telefono = Int64.Parse(Console.ReadLine());
                        Console.Write("Dias trabajados del Empleado: ");
                        DiasTrabajados = Int32.Parse(Console.ReadLine());
                        Console.Write("Salario Diario del Empleado: ");
                        SalarioDiario = Single.Parse(Console.ReadLine());

                        //escribe los dtos al archivo
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);
                        Console.Write("\n\nDeseas Almacenar otro Registro (s/n)?");

                        resp = Char.Parse(Console.ReadLine());
                    } while ((resp == 's') || (resp == 'S'));
                }
                catch (IOException e)
                {
                    Console.WriteLine("\nError : " + e.Message);
                    Console.WriteLine("\nRuta : " + e.StackTrace);
                }
                finally
                {
                    if (bw != null)
                    {
                        bw.Close(); //Cierra el flujo - escritura                  
                    }
                    Console.Write("\nPresione <Enter> para terminar la Escritura" +
                        "de Datos y regresar al Menu");
                    Console.ReadKey();
                }
            }
            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //verifica si existe el archivo
                    if (File.Exists(Archivo))
                    {
                        //creacion flujo para leer datos del archivo
                        br = new BinaryReader(new FileStream
                            (Archivo, FileMode.Open, FileAccess.Read));

                        //Limpieza de pantalla
                        Console.Clear();

                        //despliegue de datos en pantalla
                        do
                        {
                            //lectura de registros mientras no llegue a EndOfFile
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            //muestra los datos
                            Console.WriteLine("Numero del Empleado :" + NumEmp);
                            Console.WriteLine("Nombre del Empleado :" + Nombre);
                            Console.WriteLine("Direccion del Empleado :" + Direccion);
                            Console.WriteLine("Telefono del Empleado :" + Telefono);
                            Console.WriteLine("Dias trabajados del Empleado :" + DiasTrabajados);
                            Console.WriteLine("Salario Diario del Empleado : {0:C}", SalarioDiario);

                            Console.WriteLine("SUELDO TOTAL DEL Empleado : {0:C}", (DiasTrabajados * SalarioDiario));
                            Console.WriteLine("\n");
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo " + Archivo + " No" +
                            " Existe en el Disco!!");
                        Console.Write("\nPresione <Enter> para Continuar . . . ");
                        Console.ReadKey();
                    }
                }

                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del Listado de Empleados");
                    Console.Write("\nPresione <Enter> para Continuar ...");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null)
                    {
                        br.Close();
                    }
                    Console.Write("\nPresione <Enter> para terminar la Lectura de Datos" +
                        " y regresar al Menu.");
                    Console.ReadKey();
                }
            }
                static void Main(string[] args)
                {
                    //declaracion de variables auxiliares
                    string Arch = null;
                    int opcion;

                    //Creacion de objeto
                    ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();

                    //Menu de Opciones
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                        Console.WriteLine("1.- Creacion de un Archivo");
                        Console.WriteLine("2.- Lectura de un Archivo");
                        Console.WriteLine("3.- Salida del Programa");
                        opcion = Int16.Parse(Console.ReadLine());

                        switch (opcion)
                        {
                            case 1:
                                //bloque de escritura
                                try
                                {
                                    //captura nombre archivo
                                    Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                                    Arch = Console.ReadLine();

                                    //verfica si existe el archivo
                                    char resp = 's';
                                    if (File.Exists(Arch))
                                    {
                                        Console.Write("El Archivo Existe! !, Deseas" +
                                            " Sobreescribirlo (s/n) ? ");
                                        resp = Char.Parse(Console.ReadLine());
                                    }
                                    if ((resp == 's') || (resp == 'S'))
                                    {
                                        A1.CrearArchivo(Arch);
                                    }
                                }
                                catch (IOException e)
                                {
                                    Console.WriteLine("\nError : " + e.Message);
                                    Console.WriteLine("\nRuta : " + e.StackTrace);
                                }
                                break;

                            case 2:
                                //bloque de lectura
                                try
                                {
                                    //captura nombre archivo
                                    Console.Write("\nAlimenta el Nombre del Archivo que" +
                                        " deseas Leer: ");
                                    Arch = Console.ReadLine();
                                    A1.MostrarArchivo(Arch);
                                }
                                catch (IOException e)
                                {
                                    Console.WriteLine("\nError : " + e.Message);
                                    Console.WriteLine("\nRuta : " + e.StackTrace);
                                }
                                break;

                            case 3:
                                Console.Write("\nPresione <Enter> para Salir del Programa.");
                                Console.ReadKey();
                                break;

                            default:
                                Console.Write("\nEsa Opcion No Existe ! !, " +
                                    "Presione <Enter> para Continuar . . .");
                                Console.ReadKey();
                                break;
                        }
                    } while (opcion != 3);
                }
            }
        }
    }
