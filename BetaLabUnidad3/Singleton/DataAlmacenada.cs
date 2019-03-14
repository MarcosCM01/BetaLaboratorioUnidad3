using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BetaLabUnidad3.Models;
using System.IO;

namespace BetaLabUnidad3.Singleton
{
    public class DataAlmacenada
    {
        private static DataAlmacenada instancia = null;
        public static DataAlmacenada Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new DataAlmacenada();
                }
                return instancia;
            }
        }
        public List<Med> ListaMed = new List<Med>(); //LISTA PARA ALMACENAR TODA LA INFO DEL FARMACO
        public List<Pedidos> ListaPedidos = new List<Pedidos>(); //LISTA PARA ALMACENAR TODA LA INFO DEL FARMACO
        public EstructurasLineales.ArbolBinario<Med> ArbolMed = new EstructurasLineales.ArbolBinario<Med>(); //INSTANCIAS PARA ALMACENAR EL NOMBRE E ID DEL FARMACO EN ARBOL 
        public List<Med> FarmacosEliminados = new List<Med>(); //LISTA AUXILIAR PARA ALMACENAR LOS FARMACOS QUE YA NO TIENEN EN EXISTENCIA, Y SERVIRAN PARA REABASTECER
        public List<Med> MedBuscados = new List<Med>();


        public void LecturaArchivo()
        {
            string[] lineas = File.ReadAllLines("C:/Users/Marcos Andrés CM/Desktop/Version 4.0/EstructuraLab2/BetaLabUnidad3/Data-Laboratorio_Unidad_3.csv");
            int contador = 0;
            char[] separadores = { ','};

            foreach (var linea in lineas)
            {
                Med tmp = new Med();

                if(contador > 0)
                {
                    int i = 0;  //Variables para contador de id

                    //ID
                    while (linea[i] != ',')
                    {
                        i++;
                    }
                    tmp.id = int.Parse(linea.Substring(0, i));
                    i++;

                    //NOMBRE
                    int i2 = i; //Variables para contador de nombre
                    if (linea[i] == '"')
                    {
                        while (linea[i + 1] != '"') { i++; }
                        tmp.Nombre = linea.Substring(i2 + 1, i - i2);
                        i += 3;
                    }
                    else
                    {
                        while (linea[i] != ',')
                        {
                            i++;
                        }
                        tmp.Nombre = linea.Substring(i2, i - i2);
                        i++;
                    }


                    //DESCRIPCION
                    int i3 = i; //Variables para contador de la descripcion
                    if (linea[i] == '"')
                    {
                        while (linea[i + 1] != '"') { i++; }
                        tmp.descripcion = linea.Substring(i3 + 1, i - i3);
                        i += 3;
                    }
                    else
                    {
                        while (linea[i] != ',')
                        {
                            i++;
                        }
                        tmp.descripcion = linea.Substring(i3, i - i3);
                        i++;
                    }

                    //CASA PRODUCTORA
                    int i4 = i; //Variables para contador de la casa productora
                    if (linea[i] == '"')
                    {
                        while (linea[i + 1] != '"') { i++; }
                        tmp.casa = linea.Substring(i4 + 1, i - i4);
                        i += 3;
                    }
                    else
                    {
                        while (linea[i] != ',')
                        {
                            i++;
                        }
                        tmp.casa = linea.Substring(i4, i - i4);
                        i++;
                    }

                    //PRECIO
                    int i5 = i; //Variables para contador de la casa productora
                    if (linea[i] == '"')
                    {
                        while (linea[i + 1] != '"') { i++; }
                        tmp.precio = double.Parse(linea.Substring(i5 + 2, i - i5 - 1));
                        i += 2;
                    }
                    else
                    {
                        while (linea[i] != ',')
                        {
                            i++;
                        }
                        tmp.precio = double.Parse(linea.Substring(i5 + 1, i - i5 - 1));
                        i++;
                    }

                    //EXISTENCIA
                    if (linea[linea.Length - 2] == ',')
                    {
                        tmp.existencia = int.Parse(linea.Substring(linea.Length - 1));
                    }
                    else
                    {
                        tmp.existencia = int.Parse(linea.Substring(linea.Length - 2));
                    }

                    ListaMed.Add(tmp);
                    ArbolMed.AgregarNodoR(tmp.Nombre);
                }
                else { contador++; }

                
            }  
        }

    }
}
