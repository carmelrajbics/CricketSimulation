using System.Collections.Generic;

namespace CricketSimulation.Interfaces
{
    public interface IPlayers
    {
        List<Player> getPlayersList();
        List<Player> getPlayersOnStrike(List<Player> players, int wicketLost);

        void playerschangeStrike(List<Player> playing);
        void updateScoreBoardForPlayer(int runs, Player player);

    }
}
