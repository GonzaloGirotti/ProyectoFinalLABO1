using System;
namespace std
{
    internal class Program
    {
        static string rutaM = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\MATERIAS.txt";
        static string rutaAl = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\ALUMNOS.txt";
        static string rutaC = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\CARRERAS.txt";
        static string rutaDNIAl = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\DNI_ALUMNOS.txt";
        static string rutaNC = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\NOMBRES_CARRERAS.txt";
        static string rutaNM = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\NOMBRES_MATERIAS.txt";
        static string rutaActivosAl = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\ALUMNOS_ACTIVOS.txt";
        static string rutaCarrera_alumnos = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\CARRERA_ALUMNOS.txt";
        static string rutaCarrera_materias = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\CARRERA_MATERIAS.txt";
        static string rutaAlumno_materias = "C:\\Users\\gongi\\Desktop\\All\\PROGRAMACION\\C#\\PROYECTO1\\ALUMNO_MATERIAS.txt";
        static List<string> listCar = new List<string>(File.ReadAllLines(rutaC));
        static List<string> listMat = new List<string>(File.ReadAllLines(rutaM));
        static List<string> listAlum = new List<string>(File.ReadAllLines(rutaAl));
        static List<string> listDNIal = new List<string>(File.ReadAllLines(rutaDNIAl));
        static List<string> listNC = new List<string>(File.ReadAllLines(rutaNC));
        static List<string> listNM = new List<string>(File.ReadAllLines(rutaNM));
        static List<string> listActivosAl = new List<string>(File.ReadAllLines(rutaActivosAl));
        static List<string> listCarAl = new List<string>(File.ReadAllLines(rutaCarrera_alumnos));
        static List<string> listCarMat = new List<string>(File.ReadAllLines(rutaCarrera_materias));
        static List<string> listAlMat = new List<string>(File.ReadAllLines(rutaAlumno_materias));
        static int indiceMaterias = (listMat.Count);
        static int indiceAlumnos = (listAlum.Count);
        static int indiceCarreras = (listCar.Count);
        static int indiceDNI = (listDNIal.Count);
        static int indiceCarAl = (listCarAl.Count);
        static int indiceCarMat = (listCarMat.Count);
        static int indiceAlMat = (listAlMat.Count);

        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            int opcionMenuP;
            bool validarOpcion;
            string cadenaOpcion;

            do
            {
                Console.WriteLine(" ");
                Console.WriteLine("BIENVENIDO AL MENU PRINCIPAL!");
                Console.WriteLine("Ingrese una opcion: ");
                Console.WriteLine("1) IR AL MENU DE ALUMNOS.");
                Console.WriteLine("2) IR AL MENU DE CARRERAS.");
                Console.WriteLine("3) IR AL MENU DE MATERIAS.");
                Console.WriteLine("4) IR AL MENU DE ASOCIACIONES.");
                Console.WriteLine("5) SALIR.");

                cadenaOpcion = Console.ReadLine();
                validarOpcion = Validar(cadenaOpcion, out opcionMenuP);

                if (validarOpcion)
                {
                    if (opcionMenuP >= 1 && opcionMenuP <= 5)
                    {
                        switch (opcionMenuP)
                        {
                            case 1:
                                MenuAltaAlumnos();
                                break;

                            case 2:
                                MenuAltaCarreras();
                                break;

                            case 3:
                                MenuAltaMaterias();
                                break;

                            case 4:
                                MenuAsociaciones();
                                break;

                            case 5:
                                Console.WriteLine("Saliste.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR. Opcion invalida.");
                }
            } while (!validarOpcion || (opcionMenuP < 1 || opcionMenuP > 5));
        }

        static bool Validar(string cadena, out int num)
        {
            bool validarNum = int.TryParse(cadena, out num);
            return validarNum;
        }

        static bool ValidarChar(string caracter, out char caract)
        {
            bool validarChar = char.TryParse(caracter, out caract);
            return validarChar;
        }

        static void ValidarDNI(int indice, out int dniAlumno)
        {
            bool dniDup = false;
            bool validarReactivar, validarDNIal;
            char reactivar;
            string cadenaReactivar, cadenaDNI;
            int posicionDup = 0;

            do
            {
                Console.WriteLine("Ingrese el DNI del alumno: ");
                cadenaDNI = Console.ReadLine();
                validarDNIal = Validar(cadenaDNI, out dniAlumno);
                dniDup = false;

                if (validarDNIal)
                {
                    if (indiceDNI > 0)
                    {
                        for (int i = 1; i < indiceDNI && !dniDup; i++)
                        {
                            if (listDNIal[i] == $"{dniAlumno}")
                            {
                                dniDup = true;
                                posicionDup = i;
                            }
                        }

                        if (dniDup)
                        {
                            if (listActivosAl[posicionDup - 1] == "False")
                            {
                                do
                                {
                                    Console.WriteLine("Se encontro un alumno desactivado con el mismo DNI.");
                                    Console.WriteLine("Desea reactivarlo? S/N");
                                    cadenaReactivar = Console.ReadLine();
                                    validarReactivar = ValidarChar(cadenaReactivar, out reactivar);

                                    if (validarReactivar)
                                    {
                                        if (reactivar == 's' || reactivar == 'S')
                                        {
                                            string[] campos = listAlum[posicionDup].Split(",");
                                            campos[6] = "true";
                                            string modif = string.Join(",", campos);
                                            listAlum[posicionDup] = modif;
                                            listActivosAl[posicionDup] = "True";
                                            dniDup = false;
                                            File.WriteAllLines(rutaAl, listAlum);
                                            File.WriteAllLines(rutaActivosAl, listActivosAl);

                                        }
                                        else if (reactivar != 'n' || reactivar != 'N')
                                        {
                                            Console.WriteLine("ERROR. Opcion invalida.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR. Opcion invalida.");
                                    }
                                } while (!validarReactivar);
                            }
                            else
                            {
                                Console.WriteLine("Se encontro un alumno activo con el mismo DNI.");
                            }
                        }
                        else
                        {
                            listDNIal.Add($"{dniAlumno}");
                            File.WriteAllLines(rutaDNIAl, listDNIal);
                        }
                    }
                    else
                    {
                        listDNIal.Add($"{dniAlumno}");
                        File.WriteAllLines(rutaDNIAl, listDNIal);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR. DNI Invalido.");
                }
            } while (!validarDNIal || dniDup);

            indiceDNI++;
        }

        static void MenuAsociaciones()
        {
            int opcionMenuAs;
            bool validarOpcion;
            string cadenaOpcion;

            do
            {
                Console.WriteLine(" ");
                Console.WriteLine("BIENVENIDO AL MENU DE ASOCIACIONES!");
                Console.WriteLine("Ingrese una opcion: ");
                Console.WriteLine("1) ASOCIACION CARRERA ALUMNO.");
                Console.WriteLine("2) ASOCIACION CARRERA MATERIA.");
                Console.WriteLine("3) ASOCIACION ALUMNO MATERIA.");
                Console.WriteLine("4) VOLVER AL MENU PRINCIPAL.");
                Console.WriteLine("5) SALIR.");

                cadenaOpcion = Console.ReadLine();
                validarOpcion = Validar(cadenaOpcion, out opcionMenuAs);

                if (validarOpcion)
                {
                    if (opcionMenuAs >= 1 && opcionMenuAs <= 5)
                    {
                        switch (opcionMenuAs)
                        {
                            case 1:
                                if(indiceCarreras == 1 || indiceAlumnos == 1)
                                {
                                    Console.WriteLine("ERROR. Alguno de los archivos CARRERAS o ALUMNOS, no tiene indices para asociar.");
                                    MenuAsociaciones();
                                }
                                else
                                {
                                    AsociarCarreraAlumno();
                                }
                                break;
                            
                            case 2:
                                if (indiceCarreras == 1 || indiceMaterias == 1)
                                {
                                    Console.WriteLine("ERROR. Alguno de los archivos CARRERAS o MATERIAS, no tiene indices para asociar.");
                                    MenuAsociaciones();
                                }
                                else
                                {
                                    AsociarCarreraMateria();
                                }
                                break;

                            case 3:
                                if (indiceCarreras == 1 || indiceAlumnos == 1 || indiceMaterias == 1)
                                {
                                    Console.WriteLine("ERROR. Alguno de los archivos CARRERAS o ALUMNOS o MATERIAS, no tiene indices para asociar.");
                                    MenuAsociaciones();
                                }
                                else
                                {
                                    AsociarAlumnoMateria();
                                }
                                break;
                            
                            case 4:
                                MenuPrincipal();
                                break;
                            
                            case 5:
                                Console.WriteLine("Saliste.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR. Opcion invalida.");
                }
            } while (!validarOpcion || (opcionMenuAs < 1 || opcionMenuAs > 5));
        }

        static void AsociarCarreraAlumno()
        {
            char seguir;
            int indiceAsocioAl, indiceAsocioCar;
            bool validarSeguir, validarAsocioCar, validarAsocioAl;
            string cadenaSeguir, cadenaAsocioCar, cadenaAsocioAl;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el indice de la carrera a asociar: ");
                    Console.WriteLine(" ");
                    cadenaAsocioCar = Console.ReadLine();
                    validarAsocioCar = Validar(cadenaAsocioCar, out indiceAsocioCar);

                    if (validarAsocioCar)
                    {
                        if (indiceAsocioCar > indiceCarreras)
                        {
                            Console.WriteLine("ERROR. Indice invalido");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido");
                        Console.WriteLine(" ");
                    }
                } while (!validarAsocioCar || (indiceAsocioCar > indiceCarreras));

                do
                {
                    Console.WriteLine("Ingrese el indice del alumno a asociar: ");
                    Console.WriteLine(" ");
                    cadenaAsocioAl = Console.ReadLine();
                    validarAsocioAl = Validar(cadenaAsocioAl, out indiceAsocioAl);

                    if (validarAsocioAl)
                    {
                        if (indiceAsocioAl > indiceAlumnos)
                        {
                            Console.WriteLine("ERROR. Indice invalido");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido");
                        Console.WriteLine(" ");
                    }
                } while (!validarAsocioAl || (indiceAsocioAl > indiceAlumnos));

                using (StreamWriter stw = new StreamWriter(rutaCarrera_alumnos))
                {
                    try
                    {
                        stw.WriteLine($"{indiceCarAl},{indiceAsocioAl},{indiceAsocioCar}, True");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    stw.Close();
                }

                do
                {
                    Console.WriteLine("Desea asociar otra vez? S/N");
                    Console.WriteLine(" ");
                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                            Console.WriteLine(" ");
                        }
                    }
                } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
            } while (seguir == 's' || seguir == 'S');

            MenuAsociaciones();
        }

        static void AsociarCarreraMateria()
        {
            char seguir;
            int indiceAsocioMat, indiceAsocioCar, plan;
            bool validarSeguir, validarAsocioCar, validarAsocioMat, validarPlan;
            string cadenaSeguir, cadenaAsocioCar, cadenaAsocioMat, cadenaPlan;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el indice de la carrera a asociar: ");
                    Console.WriteLine(" ");
                    cadenaAsocioCar = Console.ReadLine();
                    validarAsocioCar = Validar(cadenaAsocioCar, out indiceAsocioCar);

                    if (validarAsocioCar)
                    {
                        if (indiceAsocioCar > indiceCarreras)
                        {
                            Console.WriteLine("ERROR. Indice invalido");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido");
                        Console.WriteLine(" ");
                    }
                } while (!validarAsocioCar || (indiceAsocioCar > indiceCarreras));

                do
                {
                    Console.WriteLine("Ingrese el indice de la materia a asociar: ");
                    Console.WriteLine(" ");
                    cadenaAsocioMat = Console.ReadLine();
                    validarAsocioMat = Validar(cadenaAsocioMat, out indiceAsocioMat);

                    if (validarAsocioMat)
                    {
                        if (indiceAsocioMat > indiceMaterias)
                        {
                            Console.WriteLine("ERROR. Indice invalido");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido");
                        Console.WriteLine(" ");
                    }
                } while (!validarAsocioMat || (indiceAsocioMat > indiceMaterias));

                do
                {
                    Console.WriteLine("Ingrese el anio del plan: ");
                    Console.WriteLine(" ");
                    cadenaPlan = Console.ReadLine();
                    validarPlan = Validar(cadenaPlan, out plan);

                    if (validarPlan)
                    {
                        if (plan < 1970)
                        {
                            Console.WriteLine("ERROR. Anio invalido.");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Anio invalido.");
                        Console.WriteLine(" ");
                    }
                } while (!validarPlan || (plan < 1970));

                using (StreamWriter stw = new StreamWriter(rutaCarrera_materias))
                {
                    try
                    {
                        stw.WriteLine($"{indiceCarMat},{plan},{indiceAsocioCar},{indiceAsocioMat}, True");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    stw.Close();
                }

                do
                {
                    Console.WriteLine("Desea asociar otra vez? S/N");

                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                            Console.WriteLine(" ");
                        }
                    }
                } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
            } while (seguir == 's' || seguir == 'S');

            MenuAsociaciones();
        }

        static void AsociarAlumnoMateria()
        {
            char seguir;
            int indiceAsocioAl, indiceAsocioMat, indiceCarreraAsociada, anioFecha = 1969, nota = 0;
            bool validarSeguir, validarAsocioMat, validarAsocioAl, validarFecha, validarCarAsoc, validarNota;
            string cadenaSeguir, cadenaAsocioMat, cadenaAsocioAl, cadenaFecha, estado, cadenaAsocioCar, cadenaNota, rindio;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el indice del alumno a asociar: ");
                    Console.WriteLine(" ");
                    cadenaAsocioAl = Console.ReadLine();
                    validarAsocioAl = Validar(cadenaAsocioAl, out indiceAsocioAl);

                    if (validarAsocioAl)
                    {
                        if (indiceAsocioAl > indiceAlumnos)
                        {
                            Console.WriteLine("ERROR. Indice invalido");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido");
                        Console.WriteLine(" ");
                    }
                } while (!validarAsocioAl || (indiceAsocioAl > indiceAlumnos));

                do
                {
                    Console.WriteLine("Ingrese el indice de la materia a asociar: ");
                    Console.WriteLine(" ");
                    cadenaAsocioMat = Console.ReadLine();
                    validarAsocioMat = Validar(cadenaAsocioMat, out indiceAsocioMat);

                    if (validarAsocioMat)
                    {
                        if (indiceAsocioMat > indiceMaterias)
                        {
                            Console.WriteLine("ERROR. Indice invalido");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido");
                        Console.WriteLine(" ");
                    }
                } while (!validarAsocioMat || (indiceAsocioMat > indiceMaterias));

                do
                {
                    Console.WriteLine("Ingrese el indice de la carrera asociada a esa materia: ");
                    cadenaAsocioCar = Console.ReadLine();
                    validarCarAsoc = Validar(cadenaAsocioCar, out indiceCarreraAsociada);

                    if (validarCarAsoc)
                    {
                        if (indiceCarreraAsociada > indiceCarreras)
                        {
                            Console.WriteLine("ERROR. Indice invalido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido.");
                    }
                } while (!validarCarAsoc || indiceCarreraAsociada > indiceCarreras);

                estado = "anotado";

                do
                {
                    Console.WriteLine("El alumno ya rindio final? SI/NO");
                    rindio = Console.ReadLine();
                } while (rindio != "SI" && rindio != "NO");

                if(rindio == "SI")
                {
                    do
                    {
                        Console.WriteLine("Ingrese la nota del alumno:");
                        cadenaNota = Console.ReadLine();
                        validarNota = Validar(cadenaNota, out nota);

                        if (validarNota)
                        {
                            if (nota < 1 || nota > 10)
                            {
                                Console.WriteLine("ERROR. Nota invalida.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR. Nota invalida.");
                        }
                    } while (!validarNota || (nota < 1 || nota > 10));

                    do
                    {
                        Console.WriteLine("Ingrese la fecha en la cual se dio la nota al alumno: ");
                        Console.WriteLine(" ");
                        cadenaFecha = Console.ReadLine();
                        validarFecha = Validar(cadenaFecha, out anioFecha);

                        if (validarFecha)
                        {
                            if (anioFecha < 1970)
                            {
                                Console.WriteLine("ERROR. Anio invalido");
                                Console.WriteLine(" ");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR. Anio invalido.");
                            Console.WriteLine(" ");
                        }
                    } while (!validarFecha || (anioFecha < 1970));
                }

                using (StreamWriter stw = new StreamWriter(rutaAlumno_materias))
                {
                    try
                    {
                        if(rindio == "SI") 
                        {
                            stw.WriteLine($"{indiceAlMat},{indiceCarreraAsociada},{indiceAsocioAl},{indiceAsocioMat},{estado},{nota},{anioFecha}, True");
                        }
                        else
                        {
                            stw.WriteLine($"{indiceAlMat},{indiceCarreraAsociada},{indiceAsocioAl},{indiceAsocioMat},{estado}, NO RINDIO AUN, True");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    stw.Close();
                }

                do
                {
                    Console.WriteLine("Desea asociar otra vez? S/N");
                    Console.WriteLine(" ");
                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                            Console.WriteLine(" ");
                        }
                    }
                } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
            } while (seguir == 's' || seguir == 'S');

            MenuAsociaciones();
        }

        static void MenuAltaAlumnos()
        {
            int opcionMenuA;
            bool validarOpcion;
            string cadenaOpcion;

            do
            {
                Console.WriteLine(" ");
                Console.WriteLine("BIENVENIDO AL MENU DE ALTA DE ALUMNOS!");
                Console.WriteLine("Ingrese una opcion: ");
                Console.WriteLine("1) ALTA DE ALUMNO.");
                Console.WriteLine("2) BAJA DE ALUMNO.");
                Console.WriteLine("3) MODIFICACION ALUMNO.");
                Console.WriteLine("4) VOLVER AL MENU PRINCIPAL.");
                Console.WriteLine("5) SALIR.");

                cadenaOpcion = Console.ReadLine();
                validarOpcion = Validar(cadenaOpcion, out opcionMenuA);

                if (validarOpcion)
                {
                    if (opcionMenuA >= 1 && opcionMenuA <= 5)
                    {
                        switch (opcionMenuA)
                        {
                            case 1:
                                AltaAlumno();
                                break;

                            case 2:
                                if(indiceAlumnos == 1)
                                {
                                    Console.WriteLine("ERROR. El archivo ALUMNOS. No tiene indices para dar de baja.");
                                    MenuAltaAlumnos();
                                }
                                else
                                {
                                    BajaAlumno();
                                }
                                break;

                            case 3:
                                if (indiceAlumnos == 1)
                                {
                                    Console.WriteLine("ERROR. El archivo ALUMNOS. No tiene indices para modificar.");
                                    MenuAltaAlumnos();
                                }
                                else
                                {
                                    ModificacionAlumno();
                                }
                                break;

                            case 4:
                                MenuPrincipal();
                                break;

                            case 5:
                                Console.WriteLine("Saliste.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR. Opcion invalida.");
                }
            } while (!validarOpcion || (opcionMenuA < 1 || opcionMenuA > 5));
        }

        static void AltaAlumno()
        {
            string nombreAl, apellidoAl, domicilioAl, cadenaNacimiento, cadenaSeguir;
            int dniAl, nacimientoAl;
            bool validarSeguir, validarNacimiento;
            char seguir;

            do
            {
                Console.WriteLine("Ingrese el nombre del alumno: ");
                nombreAl = Console.ReadLine();
                Console.WriteLine(" ");

                Console.WriteLine("Ingrese el apellido del alumno: ");
                apellidoAl = Console.ReadLine();
                Console.WriteLine(" ");

                Console.WriteLine("Ingrese el domicilio del alumno: ");
                domicilioAl = Console.ReadLine();
                Console.WriteLine(" ");

                ValidarDNI(indiceAlumnos, out dniAl);
                Console.WriteLine(" ");

                do
                {
                    Console.WriteLine("Ingrese el anio de nacimiento del alumno: ");
                    cadenaNacimiento = Console.ReadLine();
                    validarNacimiento = Validar(cadenaNacimiento, out nacimientoAl);

                    if (!validarNacimiento)
                    {
                        Console.WriteLine("ERROR. Anio invalido.");
                    }
                } while (!validarNacimiento);

                using (StreamWriter stw = new StreamWriter(rutaAl, true))
                {
                    try
                    {
                        stw.WriteLine($"{indiceAlumnos},{nombreAl},{apellidoAl},{domicilioAl},{dniAl},{nacimientoAl}, True");
                        listActivosAl.Add($"{indiceAlumnos},True");
                        File.WriteAllLines(rutaActivosAl, listActivosAl);
                        indiceAlumnos++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                do
                {
                    Console.WriteLine("Desea dar de alta a otro alumno? S/N");
                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                        }
                    }
                } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
            } while (seguir == 's' || seguir == 'S');

            MenuAltaAlumnos();
        }

        static void BajaAlumno()
        {
            int indiceBaja;
            string cadenaIndice, cadenaSeguir;
            bool validarIndice, validarSeguir;
            char seguir;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el indice del alumno a eliminar: ");
                    cadenaIndice = Console.ReadLine();
                    validarIndice = Validar(cadenaIndice, out indiceBaja);

                    if (validarIndice)
                    {
                        if (indiceBaja <= indiceAlumnos && indiceBaja > 1)
                        {
                            listAlum.RemoveAt(indiceBaja);
                            File.WriteAllLines(rutaAl, listAlum);
                            listDNIal.RemoveAt(indiceBaja);
                            File.WriteAllLines(rutaDNIAl, listDNIal);
                            listActivosAl.RemoveAt(indiceBaja);
                            File.WriteAllLines(rutaActivosAl, listActivosAl);

                        }
                        else
                        {
                            Console.WriteLine("ERROR. Indice invalido.");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido.");
                        Console.WriteLine(" ");
                    }
                } while (!validarIndice || (indiceBaja > indiceAlumnos) || (indiceBaja < 1));

                do
                {
                    Console.WriteLine("Quiere dar de baja a otro alumno? S/N");
                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 'S' && seguir != 's')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                            Console.WriteLine(" ");
                        }
                    }
                } while (!validarSeguir || (seguir != 'n' && seguir != 'N' && seguir != 'S' && seguir != 's'));

            } while (seguir == 'S' || seguir == 's');
        }

        static void ModificacionAlumno()
        {
            char seguir = 'n';
            bool validarSeguir, validarIndiceMod, validarNacimiento;
            string cadenaSeguir, cadenaIndiceMod, nombreAl, apellidoAl, domicilioAl, cadenaNacimiento;
            int indiceMod, anioNacAl, dniAl;

            do
            {
                Console.WriteLine("Ingrese el indice a modificar: ");
                cadenaIndiceMod = Console.ReadLine();
                validarIndiceMod = Validar(cadenaIndiceMod, out indiceMod);

                if (validarIndiceMod)
                {
                    if(indiceMod > 0 && indiceMod <= indiceAlumnos)
                    {
                        listAlum.RemoveAt(indiceMod - 1);
                        listDNIal.RemoveAt(indiceMod - 1);
                        listActivosAl.RemoveAt(indiceMod - 1);

                        Console.WriteLine("Ingrese el nombre del alumno: ");
                        nombreAl = Console.ReadLine();
                        Console.WriteLine(" ");

                        Console.WriteLine("Ingrese el apellido del alumno: ");
                        apellidoAl = Console.ReadLine();
                        Console.WriteLine(" ");

                        Console.WriteLine("Ingrese el domicilio del alumno: ");
                        domicilioAl = Console.ReadLine();
                        Console.WriteLine(" ");

                        ValidarDNI(indiceAlumnos, out dniAl);

                        do
                        {
                            Console.WriteLine("Ingrese el anio de nacimiento del alumno: ");
                            cadenaNacimiento = Console.ReadLine();
                            validarNacimiento = Validar(cadenaNacimiento, out anioNacAl);

                            if (!validarNacimiento)
                            {
                                Console.WriteLine("ERROR. Anio invalido.");
                            }
                        } while (!validarNacimiento);

                        listAlum.Insert(indiceMod, $"{indiceMod},{nombreAl},{apellidoAl},{domicilioAl},{dniAl},{anioNacAl}, True");
                        File.WriteAllLines(rutaAl, listAlum);

                        listDNIal.Insert(indiceMod - 1, $"{dniAl}");

                        listActivosAl.Insert(indiceMod - 1, "True");

                        do
                        {
                            Console.WriteLine("Desea modificar a otro alumno? S/N");
                            cadenaSeguir = Console.ReadLine();
                            validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                            if (!validarSeguir)
                            {
                                Console.WriteLine("ERROR. Opcion invalida.");
                            }
                            else
                            {
                                if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                                {
                                    Console.WriteLine("ERROR. Opcion invalida.");
                                }
                            }
                        } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido.");
                    }
                }
            } while (seguir == 's' || seguir == 'S' || (indiceMod <= 0) || (indiceMod > indiceAlumnos));
        }

        static void MenuAltaCarreras()
        {
            int opcionMenuC;
            bool validarOpcion;
            string cadenaOpcion;

            do
            {
                Console.WriteLine(" ");
                Console.WriteLine("BIENVENIDO AL MENU DE ALTA DE CARRERAS!");
                Console.WriteLine("Ingrese una opcion: ");
                Console.WriteLine("1) ALTA DE CARRERA.");
                Console.WriteLine("2) BAJA DE CARRERA.");
                Console.WriteLine("3) MODIFICACION DE CARRERA.");
                Console.WriteLine("4) MENU PRINCIPAL.");
                Console.WriteLine("5) SALIR.");

                cadenaOpcion = Console.ReadLine();
                validarOpcion = Validar(cadenaOpcion, out opcionMenuC);

                if (validarOpcion)
                {
                    if (opcionMenuC >= 1 && opcionMenuC <= 5)
                    {
                        switch (opcionMenuC)
                        {

                            case 1:
                                AltaCarrera();
                                break;

                            case 2:
                                if (indiceCarreras == 1)
                                {
                                    Console.WriteLine("ERROR. El archivo CARRERAS. No tiene indices para dar de baja.");
                                    MenuAltaCarreras();
                                }
                                else
                                {
                                    BajaCarrera();
                                }
                                break;

                            case 3:
                                if (indiceCarreras == 1)
                                {
                                    Console.WriteLine("ERROR. El archivo CARRERAS. No tiene indices para modificar.");
                                    MenuAltaCarreras();
                                }
                                else
                                {
                                    ModificacionCarrera();
                                }
                                break;

                            case 4:
                                MenuPrincipal();
                                break;

                            case 5:
                                Console.WriteLine("Saliste.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR. Opcion invalida.");
                }
            } while (!validarOpcion || (opcionMenuC < 1 || opcionMenuC > 5));
        }

        static void AltaCarrera()
        {
            bool validarSeguir;
            bool carreraDup;
            char seguir;
            string nombreCarrera, cadenaSeguir;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el nombre de la carrera: ");
                    nombreCarrera = Console.ReadLine();
                    carreraDup = false;

                    if (listNC.Count > 0)
                    {
                        for (int i = 0; i < listNC.Count && !carreraDup; i++)
                        {
                            if (listNC[i] == $"{nombreCarrera}")
                            {
                                carreraDup = true;
                            }
                        }

                        if (carreraDup)
                        {
                            Console.WriteLine("ERROR. Ya hay una carrera con el mismo nombre.");
                        }
                        else
                        {
                            listNC.Add($"{nombreCarrera}");
                        }
                    }
                    else
                    {
                        listNC.Add($"{nombreCarrera}");
                    }
                } while (carreraDup);

                using (StreamWriter stw = new StreamWriter(rutaC, true))
                {
                    try
                    {
                        stw.WriteLine($"{indiceCarreras},{nombreCarrera},true");
                        indiceCarreras++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                do
                {
                    Console.WriteLine("Desea dar de alta otra carrera? S/N");
                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                        }
                    }
                } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
            } while (seguir == 's' || seguir == 'S');

            MenuAltaCarreras();
        }

        static void BajaCarrera()
        {
            int indiceBaja;
            string cadenaIndice, cadenaSeguir;
            bool validarIndice, validarSeguir;
            char seguir;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el indice de la carrera a eliminar: ");
                    cadenaIndice = Console.ReadLine();
                    validarIndice = Validar(cadenaIndice, out indiceBaja);

                    if (validarIndice)
                    {
                        if (indiceBaja <= indiceCarreras)
                        {
                            listCar.RemoveAt(indiceBaja);
                            File.WriteAllLines(rutaC, listCar);
                            listNC.RemoveAt(indiceBaja);
                            File.WriteAllLines(rutaNC, listNC);

                        }
                        else
                        {
                            Console.WriteLine("ERROR. Indice invalido.");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido.");
                        Console.WriteLine(" ");
                    }
                } while (!validarIndice || (indiceBaja > indiceCarreras));

                do
                {
                    Console.WriteLine("Quiere dar de baja a otro alumno? S/N");
                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 'S' && seguir != 's')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                            Console.WriteLine(" ");
                        }
                    }
                } while (!validarSeguir || (seguir != 'n' && seguir != 'N' && seguir != 'S' && seguir != 's'));
            } while (seguir == 'S' || seguir == 's');
        }

        static void ModificacionCarrera()
        {
            char seguir = 'n';
            bool validarSeguir, validarIndiceMod;
            string cadenaSeguir, cadenaIndiceMod, nombreC;
            int indiceMod;

            do
            {
                Console.WriteLine("Ingrese el indice a modificar: ");
                cadenaIndiceMod = Console.ReadLine();
                validarIndiceMod = Validar(cadenaIndiceMod, out indiceMod);

                if (validarIndiceMod)
                {
                    if(indiceMod > 0 && indiceMod <= indiceCarreras)
                    {
                        listCar.RemoveAt(indiceMod - 1);

                        Console.WriteLine("Ingrese el nombre de la carrera: ");
                        nombreC = Console.ReadLine();
                        Console.WriteLine(" ");

                        listCar.Insert(indiceMod - 1, $"{indiceMod},{nombreC},True");
                        File.WriteAllLines(rutaC, listCar);

                        do
                        {
                            Console.WriteLine("Desea modificar a otra carrera? S/N");
                            cadenaSeguir = Console.ReadLine();
                            validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                            if (!validarSeguir)
                            {
                                Console.WriteLine("ERROR. Opcion invalida.");
                            }
                            else
                            {
                                if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                                {
                                    Console.WriteLine("ERROR. Opcion invalida.");
                                }
                            }
                        } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido.");
                    }
                }
            } while (seguir == 's' || seguir == 'S' || (indiceMod <=  0) || (indiceMod > indiceCarreras));
        }

        static void MenuAltaMaterias()
        {
            int opcionMenuM;
            bool validarOpcion;
            string cadenaOpcion;

            do
            {
                Console.WriteLine(" ");
                Console.WriteLine("BIENVENIDO AL MENU DE ALTA DE MATERIAS!");
                Console.WriteLine("Ingrese una opcion: ");
                Console.WriteLine("1) ALTA DE MATERIA.");
                Console.WriteLine("2) BAJA DE MATERIA.");
                Console.WriteLine("3) MODIFICACION DE MATERIA.");
                Console.WriteLine("4) MENU PRINCIPAL.");
                Console.WriteLine("5) SALIR.");

                cadenaOpcion = Console.ReadLine();
                validarOpcion = Validar(cadenaOpcion, out opcionMenuM);

                if (validarOpcion)
                {
                    if (opcionMenuM >= 1 && opcionMenuM <= 5)
                    {
                        switch (opcionMenuM)
                        {
                            case 1:
                                AltaMateria();
                                break;

                            case 2:
                                if (indiceMaterias == 1)
                                {
                                    Console.WriteLine("ERROR. El archivo MATERIAS. No tiene indices para dar de baja.");
                                    MenuAltaMaterias();
                                }
                                else
                                {
                                    BajaMateria();
                                }
                                break;

                            case 3:
                                if (indiceMaterias == 1)
                                {
                                    Console.WriteLine("ERROR. El archivo MATERIAS. No tiene indices para modificar.");
                                    MenuAltaMaterias();
                                }
                                else
                                {
                                    ModificacionMateria();
                                }
                                break;

                            case 4:
                                MenuPrincipal();
                                break;

                            case 5:
                                Console.WriteLine("Saliste.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR. Opcion invalida.");
                }
            } while (!validarOpcion || (opcionMenuM < 1 || opcionMenuM > 5));
        }

        static void AltaMateria()
        {
            bool validarSeguir;
            bool materiaDup = false;
            string nombreMateria, cadenaSeguir;
            char seguir;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el nombre de la materia: ");
                    nombreMateria = Console.ReadLine();
                    materiaDup = false;

                    if (listNM.Count > 0)
                    {
                        for (int i = 0; i < listNM.Count && !materiaDup; i++)
                        {
                            if (listNM[i] == $"{nombreMateria}")
                            {
                                materiaDup = true;
                            }
                        }

                        if (materiaDup)
                        {
                            Console.WriteLine("ERROR. Ya hay una materia con el mismo nombre.");
                        }
                        else
                        {
                            listNM.Add($"{nombreMateria}");
                        }
                    }
                    else
                    {
                        listNM.Add($"{nombreMateria}");
                    }
                } while (materiaDup);

                using (StreamWriter stw = new StreamWriter(rutaM, true))
                {
                    try
                    {
                        stw.WriteLine($"{indiceMaterias},{nombreMateria},true");
                        indiceMaterias++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                do
                {
                    Console.WriteLine("Desea dar de alta otra carrera? S/N");
                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                        }
                    }
                } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
            } while (seguir == 's' || seguir == 'S');

            MenuAltaMaterias();
        }

        static void BajaMateria()
        {
            int indiceBaja;
            string cadenaIndice, cadenaSeguir;
            bool validarIndice, validarSeguir;
            char seguir;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el indice de la materia a eliminar: ");
                    cadenaIndice = Console.ReadLine();
                    validarIndice = Validar(cadenaIndice, out indiceBaja);

                    if (validarIndice)
                    {
                        if (indiceBaja <= indiceMaterias)
                        {
                            listMat.RemoveAt(indiceBaja);
                            File.WriteAllLines(rutaM, listMat);
                            listNM.RemoveAt(indiceBaja);
                            File.WriteAllLines(rutaNM, listNM);

                        }
                        else
                        {
                            Console.WriteLine("ERROR. Indice invalido.");
                            Console.WriteLine(" ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido.");
                        Console.WriteLine(" ");
                    }
                } while (!validarIndice || (indiceBaja > indiceMaterias));

                do
                {
                    Console.WriteLine("Quiere dar de baja a otro alumno? S/N");
                    cadenaSeguir = Console.ReadLine();
                    validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                    if (!validarSeguir)
                    {
                        Console.WriteLine("ERROR. Opcion invalida.");
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        if (seguir != 'n' && seguir != 'N' && seguir != 'S' && seguir != 's')
                        {
                            Console.WriteLine("ERROR. Opcion invalida.");
                            Console.WriteLine(" ");
                        }
                    }
                } while (!validarSeguir || (seguir != 'n' && seguir != 'N' && seguir != 'S' && seguir != 's'));

            } while (seguir == 'S' || seguir == 's');
        }

        static void ModificacionMateria()
        {
            char seguir = 'n';
            bool validarSeguir, validarIndiceMod;
            string cadenaSeguir, cadenaIndiceMod, nombreM;
            int indiceMod;

            do
            {
                Console.WriteLine("Ingrese el indice a modificar: ");
                cadenaIndiceMod = Console.ReadLine();
                validarIndiceMod = Validar(cadenaIndiceMod, out indiceMod);

                if (validarIndiceMod)
                {
                    if(indiceMod > 0 && indiceMod <= indiceMaterias)
                    {
                        listCar.RemoveAt(indiceMod - 1);

                        Console.WriteLine("Ingrese el nombre de la materia: ");
                        nombreM = Console.ReadLine();
                        Console.WriteLine(" ");

                        listCar.Insert(indiceMod - 1, $"{indiceMod},{nombreM},True");
                        File.WriteAllLines(rutaC, listCar);

                        do
                        {
                            Console.WriteLine("Desea modificar a otra materia? S/N");
                            cadenaSeguir = Console.ReadLine();
                            validarSeguir = ValidarChar(cadenaSeguir, out seguir);

                            if (!validarSeguir)
                            {
                                Console.WriteLine("ERROR. Opcion invalida.");
                            }
                            else
                            {
                                if (seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S')
                                {
                                    Console.WriteLine("ERROR. Opcion invalida.");
                                }
                            }
                        } while ((seguir != 'n' && seguir != 'N' && seguir != 's' && seguir != 'S') || !validarSeguir);
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Indice invalido.");
                    }
                }
            } while (seguir == 's' || seguir == 'S' || (indiceMod <= 0) || (indiceMod > indiceMaterias));
        }
    }
}