using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
    class StartUp
    {
        static void Main()
        {
            var contex = new FootballBettingContext();

            contex.Database.EnsureCreated();
        }
    }
}
