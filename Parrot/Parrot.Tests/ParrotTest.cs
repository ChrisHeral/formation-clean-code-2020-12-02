using Xunit;

namespace Parrot.Tests
{
    public class ParrotTest
    {
        [Fact]
        public void GetSpeedNorwegianBlueParrot_nailed()
        {
            ICanGiveSpeed canGiveSpeed = new NorwegianBlueParrot(0.0, true);
            Assert.Equal(0.0, canGiveSpeed.GetSpeed());
        }

        [Fact]
        public void GetSpeedNorwegianBlueParrot_not_nailed()
        {
            ICanGiveSpeed canGiveSpeed = new NorwegianBlueParrot(1.5, false);
            Assert.Equal(18.0, canGiveSpeed.GetSpeed());
        }

        [Fact]
        public void GetSpeedNorwegianBlueParrot_not_nailed_high_voltage()
        {
            ICanGiveSpeed canGiveSpeed = new NorwegianBlueParrot(4, false);
            Assert.Equal(24.0, canGiveSpeed.GetSpeed());
        }

        [Fact]
        public void GetSpeedOfAfricanParrot_With_No_Coconuts()
        {
            ICanGiveSpeed african = new AfricanParrot(0);
            Assert.Equal(12.0, african.GetSpeed());
        }

        [Fact]
        public void GetSpeedOfAfricanParrot_With_One_Coconut()
        {
            ICanGiveSpeed african = new AfricanParrot(1);
            Assert.Equal(3.0, african.GetSpeed());
        }

        [Fact]
        public void GetSpeedOfAfricanParrot_With_Two_Coconuts()
        {
            ICanGiveSpeed african = new AfricanParrot(2);

            Assert.Equal(0.0, african.GetSpeed());
        }

        [Fact]
        public void GetSpeedOfEuropeanParrot()
        {
            ICanGiveSpeed europeanCanGiveSpeed = new EuropeanParrot();
            Assert.Equal(12.0, europeanCanGiveSpeed.GetSpeed());
        }

        [Fact]
        public void Glop()
        {
            var parrot = new Parrot(ParrotTypeEnum.AFRICAN,0,0,false);
        }
    }
}