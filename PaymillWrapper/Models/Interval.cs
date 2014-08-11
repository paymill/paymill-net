using PaymillWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymillWrapper.Models
{
    [Newtonsoft.Json.JsonConverter(typeof(StringToIntervalConverter))]
    public class Interval
    {
        public static Interval.Period period(int interval, TypeUnit unit)
        {
            return new Interval.Period(interval, unit);
        }
        public static Interval.PeriodWithChargeDay periodWithChargeDay(int interval, TypeUnit unit, Weekday weekday)
        {
            return new Interval.PeriodWithChargeDay(interval, unit, weekday);
        }

        public static Interval.PeriodWithChargeDay periodWithChargeDay(int interval, TypeUnit unit)
        {
            return new Interval.PeriodWithChargeDay(interval, unit, null);
        }
         [Newtonsoft.Json.JsonConverter(typeof(StringToPeriodConverter))]
        public sealed class Period
        {

            public int Interval { get; set; }
            public TypeUnit Unit { get; set; }

            public Period(String interval)
            {
                try
                {
                    String[] intervalParts = interval.Split();
                    this.Interval = int.Parse(intervalParts[0]);
                    this.Unit = TypeUnit.Create(intervalParts[1]);
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new ArgumentException("Invalid period:" + interval);
                }
            }

            internal Period(int interval, TypeUnit unit)
            {
                this.Interval = interval;
                this.Unit = unit;
            }



            public override string ToString()
            {
                return this.Interval + " " + this.Unit;
            }
        }
         [Newtonsoft.Json.JsonConverter(typeof(StringToPeriodWithChargeDaydConverter))]
        public sealed class PeriodWithChargeDay
        {
            public int Interval { get; set; }
            public TypeUnit Unit { get; set; }
            public Weekday Weekday { get; set; }

            public PeriodWithChargeDay(String interval)
            {
                try
                {
                    String[] weekdayParts = interval.Split(',');
                    if (weekdayParts.Count() > 1)
                    {
                        this.Weekday = Weekday.Create(weekdayParts[1]);
                    }
                    String[] intervalParts = weekdayParts[0].Split();
                    this.Interval = int.Parse(intervalParts[0]);
                    this.Unit = TypeUnit.Create(intervalParts[1]);
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new ArgumentException("Invalid period:" + interval);
                }
            }

            internal PeriodWithChargeDay(int interval, TypeUnit unit, Weekday weekday)
            {
                this.Interval = interval;
                this.Unit = unit;
                this.Weekday = weekday;
            }


            public override string ToString() {
                return (Weekday == null) ? this.Interval + " " + this.Unit : this.Interval + " " + this.Unit + "," + Weekday;
            }
        }
        [Newtonsoft.Json.JsonConverter(typeof(StringToBaseEnumTypeConverter<Weekday>))]
        public sealed class Weekday : EnumBaseType
        {
            public static readonly Interval.Weekday MONDAY;
            public static readonly Interval.Weekday TUESDAY;
            public static readonly Interval.Weekday WEDNESDAY;
            public static readonly Interval.Weekday THURSDAY;
            public static readonly Interval.Weekday FRIDAY;
            public static readonly Interval.Weekday SATURDAY;
            public static readonly Interval.Weekday SUNDAY;
            private Weekday(String name, Boolean unknowValue = false)
                : base(name, unknowValue)
            {

            }
            static Weekday()
            {
                MONDAY = new Weekday("MONDAY");
                TUESDAY = new Weekday("TUESDAY");
                WEDNESDAY = new Weekday("WEDNESDAY");
                THURSDAY = new Weekday("THURSDAY");
                FRIDAY = new Weekday("FRIDAY");
                SATURDAY = new Weekday("SATURDAY");
                SUNDAY = new Weekday("SUNDAY");
            }
            // <summary>
            /// Creates the Weekday.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            /// <exception cref="System.ArgumentException">Invalid value for Interval.Weekday</exception>
            public static Weekday Create(String value)
            {
                return (Weekday)Weekday.GetItemByValue(value, typeof(Weekday));
            }
            public override String ToString()
            {
                return this.Value;
            }
        }
        [Newtonsoft.Json.JsonConverter(typeof(StringToBaseEnumTypeConverter<TypeUnit>))]
        public sealed class TypeUnit : EnumBaseType
        {
            public static readonly Interval.TypeUnit DAY;
            public static readonly Interval.TypeUnit WEEK;
            public static readonly Interval.TypeUnit MONTH;
            public static readonly Interval.TypeUnit YEAR;
            public static readonly Interval.TypeUnit UNKNOWN;
            private TypeUnit(String name, Boolean unknowValue = false)
                : base(name, unknowValue)
            {

            }
            public TypeUnit()
                : base("", false)
            {
            }
            static TypeUnit()
            {
                DAY = new TypeUnit("DAY");
                WEEK = new TypeUnit("WEEK");
                MONTH = new TypeUnit("MONTH");
                YEAR = new TypeUnit("YEAR");
                UNKNOWN = new TypeUnit("", true);
            }
            /// <summary>
            /// Creates the unit.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            /// <exception cref="System.ArgumentException">Invalid value for Interval.Unit</exception>
            public static TypeUnit Create(String value)
            {
                return (TypeUnit)TypeUnit.GetItemByValue(value, typeof(TypeUnit));
            }
        }
        public int Count { get; set; }
        public EnumBaseType Unit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Count"/> class.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public Interval(String interval)
        {
            String[] parts = interval.Split(' ');
            this.Count = int.Parse(parts[0]);
            this.Unit = Create(parts[1]);
        }
        /// <summary>
        /// Creates the unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid value for Interval.Unit</exception>
        public static TypeUnit Create(String value)
        {
            return (TypeUnit)TypeUnit.GetItemByValue(value, typeof(TypeUnit));
        }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override String ToString()
        {
            return String.Format("{0} {1}", this.Count, this.Unit);
        }

    }
}
