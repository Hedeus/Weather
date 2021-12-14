using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models.Weather
{
    class PrecipitationBool
    {
        public bool WithoutPrecipitation { get; set; }
        public bool Rain { get; set; }
        public bool Snow { get; set; }
        public bool Hail { get; set; }
    }
    class DayWeather
    {
        public byte Month { get; set; }
        public int Day { get; set; }
        public sbyte Temperature { get; set; }
        public ushort Pressure { get; set; }
        public int PreciInt { get; set; }
        public PrecipitationBool Precipitation { get; set; }

        public DayWeather()
        {
            Precipitation = new PrecipitationBool();
        }

        public void PrecipitationToBool()
        {
            PrecipitationBool result = new PrecipitationBool();
            if ((PreciInt & 1) > 0)
                result.WithoutPrecipitation = true;
            if ((PreciInt & 2) > 0)
                result.Rain = true;
            if ((PreciInt & 4) > 0)
                result.Snow = true;
            if ((PreciInt & 8) > 0)
                result.Hail = true;
            Precipitation = result;
        }
        public void PresipitationToInt()
        {
            int result = 0;
            if (Precipitation.WithoutPrecipitation)
                result |= 1;
            if (Precipitation.Rain)
                result |= 2;
            if (Precipitation.Snow)
                result |= 4;
            if (Precipitation.Hail)
                result |= 8;
            PreciInt = result;
        }
    }

    class DesiredDay
    {

        public bool IsDate { get; set; }
        public bool IsTemperature { get; set; }
        public bool IsPress { get; set; }
        public byte StartMonth { get; set; }
        public byte EndMonth { get; set; }
        public int StartDay { get; set; }
        public int EndDay { get; set; }
        public sbyte StartTemperature { get; set; }
        public sbyte EndTemperature { get; set; }
        public ushort StartPressure { get; set; }
        public ushort EndPressure { get; set; }
        public PrecipitationBool Precipitation { get; set; }

        public DesiredDay()
        {
            Precipitation = new PrecipitationBool();
        }
        public int PresipitationToInt()
        {
            int result = 0;
            if (Precipitation.WithoutPrecipitation)
                result |= 1;
            if (Precipitation.Rain)
                result |= 2;
            if (Precipitation.Snow)
                result |= 4;
            if (Precipitation.Hail)
                result |= 8;
            return result;
        }

        public bool IsPrecipitation()
        {
            if (Precipitation.WithoutPrecipitation)
                return true;
            if (Precipitation.Rain)
                return true;
            if (Precipitation.Snow)
                return true;
            if (Precipitation.Hail)
                return true;
            return false;
        }
    }
}
