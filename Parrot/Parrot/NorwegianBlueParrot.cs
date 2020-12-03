using System;

namespace Parrot
{
    public class NorwegianBlueParrot : ICanGiveSpeed
    {
        private readonly double _voltage;
        private readonly bool _isNailed;

        public NorwegianBlueParrot(double voltage, bool isNailed)
        {
            _voltage = voltage;
            _isNailed = isNailed;
        }

        public double GetSpeed()
        {
            return _isNailed ? 0 : Math.Min(24.0, _voltage * 12.0);
        }
    }
}