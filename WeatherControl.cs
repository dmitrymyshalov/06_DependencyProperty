using System.Windows;

namespace Dependency
{
    internal class WeatherControl : DependencyObject
    {
        public enum PrecipitationEnum { Sunny, Cloudy, Rain, Snow }

        private const int MIN_TEMPERATURE = -50;
        private const int MAX_TEMPERATURE = 50;

        private string _windDirection;
        private int _windSpeed;
        private PrecipitationEnum _precipitation;

        public static readonly DependencyProperty TemperatureProperty;

        public string WindDirection
        {
            get => _windDirection;
            set => _windDirection = value;
        }

        public int WindSpeed
        {
            get => _windSpeed;
            set => _windSpeed = value;
        }

        public PrecipitationEnum Precipitation
        {
            get => _precipitation;
            set => _precipitation = value;
        }

        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }

        public WeatherControl(string windDirection, int windSpeed, PrecipitationEnum precipitation)
        {
            this.WindDirection = windDirection;
            this.WindSpeed = windSpeed;
            this.Precipitation = precipitation;
        }

        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemperature)),
                    new ValidateValueCallback(ValidateTemperature));
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)
        {
            int intValue = (int)baseValue;
            if (intValue < MIN_TEMPERATURE)
            {
                return MIN_TEMPERATURE;
            }
            if (intValue > MAX_TEMPERATURE)
            {
                return MAX_TEMPERATURE;
            }
            return intValue;
        }

        private static bool ValidateTemperature(object value)
        {
            int intValue = (int)value;
            return intValue >= MIN_TEMPERATURE && intValue <= MAX_TEMPERATURE;
        }
    }
}