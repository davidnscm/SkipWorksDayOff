using System;
using System.Collections.Generic;

namespace SkipWorkDaysOff
{
    public static class DateUtils
    {
        public static string InitialsDayOfWeek(DateTime dt)
        {
            var d = dt.DayOfWeek;
            switch (d)
            {
                case DayOfWeek.Sunday:
                    return "Domingo";
                case DayOfWeek.Monday:
                    return "Segunda-feira";
                case DayOfWeek.Tuesday:
                    return "Terça-feira";
                case DayOfWeek.Wednesday:
                    return "Quarta-feira";
                case DayOfWeek.Thursday:
                    return "Quinta-feira";
                case DayOfWeek.Friday:
                    return "Sexta-feira";
                case DayOfWeek.Saturday:
                    return "Sábado";
                default:
                    return "-";
            }
        }

        private static bool IsHoliday(DateTime date)
        {
            // Carregar hashset de feriados "fixos"
            HashSet<DateTime> holidays = new HashSet<DateTime>
            {
                new DateTime(DateTime.Now.Year, 1, 1), // Ano Novo
                new DateTime(DateTime.Now.Year, 4, 10), // Paixão de Cristo
                new DateTime(DateTime.Now.Year, 4, 21), // Tiradentes
                new DateTime(DateTime.Now.Year, 5, 1), //Dia do Trabalho
                new DateTime(DateTime.Now.Year, 9, 7), //Independência
                new DateTime(DateTime.Now.Year, 10, 12), //Dia  da Nossa Senhora Aparecida
                new DateTime(DateTime.Now.Year, 11, 2), //Finados
                new DateTime(DateTime.Now.Year, 11, 15), //Proclamação da República
                new DateTime(DateTime.Now.Year, 12, 25) //Natal
            };

            return holidays.Contains(date);
        }

        // Referencia que a variável dt.DayOfWeek é igual ao sábado ou igual ao domingo
        private static bool WorkDay(DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday
                 || dt.DayOfWeek == DayOfWeek.Sunday;
        }


        // Mostra que quando a função Holiday for diferente da WorkDay, adicionar mais um dia 

        public static DateTime NextWorkDay(DateTime dt)
        {
            while (IsHoliday(dt) || WorkDay(dt))
                dt = dt.AddDays(1);
            return dt;
        }

        static void Main(string[] args)
        {                 
            Console.WriteLine("Pular fim de semanas e feriados");

            var hoje = DateTime.Now;

            Console.WriteLine($"\n\n\nHoje é {InitialsDayOfWeek(hoje)} dia {hoje.ToShortDateString()}.");

            Console.WriteLine("\n\n");

            var dtaux = new DateTime(DateTime.Now.Year, 12, 25);

            var proximodiautil = NextWorkDay(dtaux);

            Console.WriteLine($"Parametro: {dtaux.ToShortDateString()} - o próximo dia útil é: {proximodiautil.ToShortDateString()}");
        }
    }
}
