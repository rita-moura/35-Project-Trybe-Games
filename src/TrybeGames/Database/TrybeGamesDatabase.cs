namespace TrybeGames;

public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        return  
        (
            from game in Games
            where game.DeveloperStudio == gameStudio.Id
            select game
        ).ToList();
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        return 
        (
            from game in Games
            from play in game.Players 
            where player.Id == play
            select game
        ).ToList();
    }

    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        return 
        (
            from game in Games
            where playerEntry.GamesOwned.Contains(game.Id)
            select game
        ).ToList();
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        return
        (
            from game in Games
            join studio in GameStudios
            on game.DeveloperStudio equals studio.Id
            select new GameWithStudio
            {
                GameName = game.Name,
                StudioName = studio.Name,
                NumberOfPlayers = game.Players.Count
            }
        ).ToList();                     
    }
    
    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        return 
        (
            from game in Games
            select game.GameType
        ).Distinct().ToList();
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        return 
        (
            from studio in GameStudios
            select new StudioGamesPlayers
            {
                GameStudioName = studio.Name,
                Games = 
                (
                    from game in Games
                    where game.DeveloperStudio == studio.Id
                    select new GamePlayer
                    {
                        GameName = game.Name,
                        Players = 
                        (
                            from playerId in game.Players
                            join player in Players
                            on playerId equals player.Id
                            select player
                        ).ToList()
                    }
                ).ToList()
            }
        ).ToList();
    }

}
