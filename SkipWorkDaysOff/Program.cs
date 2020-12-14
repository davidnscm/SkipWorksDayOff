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

        // Referencia os dias que são úteis
        private static bool WorkDay(DateTime dt)
        {
             return dt.DayOfWeek == DayOfWeek.Monday
                 || dt.DayOfWeek == DayOfWeek.Tuesday
                 || dt.DayOfWeek == DayOfWeek.Wednesday
                 || dt.DayOfWeek == DayOfWeek.Thursday
                 || dt.DayOfWeek == DayOfWeek.Friday;
        }
        private static bool IsWeekend(DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday
                 || dt.DayOfWeek == DayOfWeek.Sunday;
        }

        // Mostra que quando a função IsHoliday for diferente da IsWeekend, adicionar um dia
        /* acceptnextmonth é utilizado para escolher se aceita o próximo mês, 
           se aceitar retorna o próximo dia útil, se não aceitar retorna ao dia útil anterior */
        public static DateTime NextWorkDay(DateTime dt, bool acceptnextmonth)
        {
            DateTime result = dt;
            while (IsHoliday(result) || IsWeekend(result))
            {
                result = result.AddDays(1);
            }
            if (dt.Month != result.Month)
            {
                if (acceptnextmonth)
                {
                    return result;
                }
                else
                {
                    return PreviousWorkDay(dt);
                }
            }
            else
            {
                return result;
            }
        }
        
        // Mostra que quando a função IsHoliday for diferente da IsWeekend, diminuir um dia
        public static DateTime PreviousWorkDay(DateTime dt)
        {
            while (IsHoliday(dt) || IsWeekend(dt))
                dt = dt.AddDays(-1);
            return dt;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Pular fim de semanas e feriados");

            var hoje = DateTime.Now;

            Console.WriteLine($"\nHoje é {InitialsDayOfWeek(hoje)} dia {hoje.ToShortDateString()}.");

            Console.WriteLine("\n");

            var dtauxproximo = new DateTime(DateTime.Now.Year, 12, 25);
            var proximodiautil = NextWorkDay(dtauxproximo, true);
            Console.WriteLine($"Parametro: {dtauxproximo.ToShortDateString()} - o próximo dia útil é: {proximodiautil.ToShortDateString()}");

            var dtauxanterior = new DateTime(DateTime.Now.Year, 12, 26);
            var diautilanterior = PreviousWorkDay(dtauxanterior);
            Console.WriteLine($"Parametro: {dtauxanterior.ToShortDateString()} - o dia útil anterior é: {diautilanterior.ToShortDateString()}");

            var dtaux1 = new DateTime(2020, 5, 31);
            var diautil1 = NextWorkDay(dtaux1, false);
            Console.WriteLine($"\nParametro: {dtaux1.ToShortDateString()} - Se não aceitar o próximo mês o resultado é: {diautil1.ToShortDateString()}");

            var dtaux2 = new DateTime(2020, 5, 31);
            var diautil2 = NextWorkDay(dtaux2, true);
            Console.WriteLine($"Parametro: {dtaux2.ToShortDateString()} - Se aceitar o próximo mês o resultado é: {diautil2.ToShortDateString()}");
        }    
    }
}