﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_final_3
{
    internal class AmigoSecreto
    {

        /**
         * Creamos las diferentes propiedades para gestionar la información y el estado del juego 'Amigo Secreto'.
         * Estas propiedades nos permiten configurar los parámetros del juego, realizar cálculos y hacer un seguimiento de los jugadores.
         * Estas propiedades representan los atributos y valores necesarios para administrar el juego.
         */
        public int CantidadDeJugadores { get; set; }

        public DateTime FechaDeInicio { get; set; }

        public DateTime FechaDeDescubrimiento { get; set; }

        public int NumeroDeEndulzadas { get; set; }

        public int FrecuenciaDeEndulzadas { get; set; }

        public double ValorDeEndulzadas { get; set; }

        public double ValorDeRegalo { get; set; }

        public Jugador[] Jugadores { get; set; }



        public AmigoSecreto()
        {
            
        }


        // Creamos un constructor para la clase 'AmigoSecreto'
        public AmigoSecreto(int cantidadJugadores, DateTime fechaInicio, DateTime fechaFin, int numeroEndulzadas, int frecuenciaEndulzadasDias, double valorEndulzada, double valorRegalo)
        {
            this.CantidadDeJugadores = cantidadJugadores;
            this.FechaDeInicio = fechaInicio;
            this.FechaDeDescubrimiento = fechaFin;
            this.NumeroDeEndulzadas = numeroEndulzadas;
            this.FrecuenciaDeEndulzadas = frecuenciaEndulzadasDias;
            this.ValorDeEndulzadas = valorEndulzada;
            this.ValorDeRegalo = valorRegalo;
            this.Jugadores = new Jugador[cantidadJugadores];
        }


        /**
         * Función para poder crear y almacenar jugadores en el juego 'Amigo Secreto'
         */
        public Jugador[] CrearJugadores(int cantidad)
        {
            Jugadores = new Jugador[cantidad]; // Inicializa la propiedad Jugadores

            for (int i = 0; i < cantidad; i++)
            {
                // Crea y almacena cada jugador en la propiedad Jugadores
                string nombre = "Jugador " + (i + 1);
                string correo = "jugador" + (i + 1) + "@correo.com";
                string endulzadaIdeal = "Endulzada Ideal " + (i + 1);
                string regaloIdeal = "Regalo Ideal " + (i + 1);

                Jugadores[i] = new Jugador(nombre, correo, endulzadaIdeal, regaloIdeal);
            }

            return Jugadores;
        }


        /**
         * Esta función se encarga de tomar un conjunto de jugadores y asignarlos al juego 'Amigo Secreto'
         */
        public void AsignarJugadores(Jugador[] jugadores)
        {
            if (jugadores.Length == CantidadDeJugadores)
            {
                Jugadores = jugadores;
            }
            else
            {
                Console.WriteLine("La cantidad de jugadores no coincide con la cantidad especificada en el juego.");
            }
        }



        // Creamos un método que se encargue de asignar amigos secretos a los jugadores del juego y simular el envío de correos electrónicos
        public void AsignarAmigosSecretos()
        {

            if (Jugadores == null || Jugadores.Length < 2)
            {
                MessageBox.Show("No hay suficientes jugadores para asignar amigos secretos.");
                return;
            }

            Random rng = new Random();
            List<int> disponibles = Enumerable.Range(0, Jugadores.Length).ToList();
            StringBuilder mensaje = new StringBuilder("Asignación de Amigos Secretos:\n");

            for (int i = 0; i < Jugadores.Length; i++)
            {
                Jugador jugadorActual = Jugadores[i];

                // Evita que el jugador se asigne a sí mismo como amigo secreto
                int indiceAmigoSecreto;
                do
                {
                    indiceAmigoSecreto = disponibles[rng.Next(disponibles.Count)];
                } while (indiceAmigoSecreto == i);

                Jugador amigoSecreto = Jugadores[indiceAmigoSecreto];

                // Agrega información de amigos secretos al mensaje
                mensaje.AppendLine($"{jugadorActual.Nombre} es el amigo secreto de {amigoSecreto.Nombre}");

                // Remueve el índice asignado de la lista de disponibles
                disponibles.Remove(indiceAmigoSecreto);
            }

            MessageBox.Show(mensaje.ToString(), "Asignación de Amigos Secretos");

        }

        /**
         * Función que se encarga de mostrar la información relevante del juego.
         */
        public void ImprimirInformacionJuego()
        {
            MessageBox.Show($"Fecha de Inicio: {FechaDeInicio}\n" +
                $"Fecha de Fin: {FechaDeDescubrimiento}\n" +
                $"Número de Endulzadas: {NumeroDeEndulzadas}\n" +
                $"Frecuencia de Endulzadas en días: {FrecuenciaDeEndulzadas}\n" +
                $"Valor de la Endulzada: {ValorDeEndulzadas:C}\n" +
                $"Valor del Regalo: {ValorDeRegalo:C}");
        }


        /**
         * Esta función recorre todos los jugadores almacenados en la propiedad 'Jugadores0 y muestra sus gustos.
         * También muestra el nombre, el gusto de endulzada y el regalo.
         */
        public void ImprimirGustosJugadores()
        {
            foreach (Jugador jugador in Jugadores)
            {
                string mensaje = $"Jugador: {jugador.Nombre}\n" +
                                 $"Dulcesito ideal: {jugador.EndulzadaIdeal}\n" +
                                 $"Regalito ideal: {jugador.RegaloIdeal}\n";

                MessageBox.Show(mensaje, "Gustos del Jugador");
            }
        }



        /**
         * Funcion que calcula el número de días hasta la próxima endulzada.
         */
        public int DiasHastaProximaEndulzada(DateTime fechaActual)
        {
            // Calcula la diferencia en días entre la fecha actual y la fecha de inicio del juego
            TimeSpan diferencia = FechaDeInicio - fechaActual;

            // Calcula el número de días restantes hasta la próxima endulzada
            int diasRestantes = (int)diferencia.TotalDays % FrecuenciaDeEndulzadas;

            if (diasRestantes < 0)
            {
                // Si es negativo, significa que ya pasó una endulzada, por lo que se calcula la próxima endulzada
                diasRestantes = FrecuenciaDeEndulzadas + diasRestantes;
            }

            return diasRestantes;
        }
    }
}
