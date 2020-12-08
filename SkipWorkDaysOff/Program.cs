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
                    return "DOM";
                case DayOfWeek.Monday:
                    return "SEG";
                case DayOfWeek.Tuesday:
                    return "TER";
                case DayOfWeek.Wednesday:
                    return "QUA";
                case DayOfWeek.Thursday:
                    return "QUI";
                case DayOfWeek.Friday:
                    return "SEX";
                case DayOfWeek.Saturday:
                    return "SAB";
                default:
                    return "-";
            }
        }

        // Inicializando hashset
        private static HashSet<DateTime> Holidays = new HashSet<DateTime>();


        // Verificar se o elemento está presente no hashset ou não
        private static bool Holiday(DateTime date)
        {
            return Holidays.Contains(date);
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
            while (Holiday(dt) || WorkDay(dt))
                dt = dt.AddDays(1);
            return dt;
        }

        static void Main(string[] args)
        {

            // Adicionar feriados com DateTime referenciando pelo ano atual

            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 1)); // Ano Novo
            Holidays.Add(new DateTime(DateTime.Now.Year, 4, 10)); // Paixão de Cristo
            Holidays.Add(new DateTime(DateTime.Now.Year, 4, 21)); // Tiradentes
            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 1)); //Dia do Trabalho
            Holidays.Add(new DateTime(DateTime.Now.Year, 9, 7)); //Independência
            Holidays.Add(new DateTime(DateTime.Now.Year, 10, 12)); //Dia  da Nossa Senhora Aparecida
            Holidays.Add(new DateTime(DateTime.Now.Year, 11, 2)); //Finados
            Holidays.Add(new DateTime(DateTime.Now.Year, 11, 15)); //Proclamação da República
            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 25)); //Natal

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
