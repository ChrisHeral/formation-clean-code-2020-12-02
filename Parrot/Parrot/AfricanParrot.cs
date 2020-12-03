using System;

namespace Parrot
{
    public class AfricanParrot : ICanGiveSpeed
    {
        private readonly int _numberOfCoconuts;

        public AfricanParrot(int numberOfCoconuts)
        {
            _numberOfCoconuts = numberOfCoconuts;
        }

        public double GetSpeed()
        {
            return Math.Max(0, 12.0 - 9.0 * _numberOfCoconuts);
        }
    }
}