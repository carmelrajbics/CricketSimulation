using System.Configuration;
using CricketSimulation.Interfaces;
using Unity;

namespace CricketSimulation.BootStrapper
{
    public class DependencyInjection
    {
        private static UnityContainer _container;

        public static UnityContainer SetUp()
        {
            if (_container == null)
            {
                _container = new UnityContainer();
                _container.RegisterType<IPlayers, Player>();
                _container.RegisterType<ICommentary, Commentary>();
                _container.RegisterType<IWeightedRandomGenerator, WeightedRandomGenerator>();
                _container.RegisterInstance("totalRunToWin", int.Parse(ConfigurationManager.AppSettings["totalRunToWin"]));
                _container.RegisterInstance("noOfOvers", int.Parse(ConfigurationManager.AppSettings["noOfOvers"]));
            }
            return _container;
        }
    }
}
