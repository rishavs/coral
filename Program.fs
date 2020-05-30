namespace Coral

module Program = 
    open SFML.System

    [<EntryPoint>]
    let main argv =
        let config: Config = {
            Width = 1600u
            Height = 900u
            Title = "My Awesome Game"
        } 
        // Define starting entities


        // Define starting game state
        let gamestate = {
            BallPosition                = Vector2f(100.0f, 100.0f)
            CurrentScene                = MainMenuScn
            DeltaTime                   = 0
            TickCount                   = 0
            ShowDebugHUD                = true
            CurrentTickLoadsNewEntities = false
            CurrentTickUpdatesDrawables = false
            CurrentTickSetsupScene      = true
            UserCommandsList            = Set.empty
            KeysPressed                 = Set.empty
            Entities                    = Map.empty
        }
     
        Engine.start (config, gamestate)
        0 // return an integer exit code

