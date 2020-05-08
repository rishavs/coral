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

        let gamestate: State = {
            Position = Vector2f(100.0f, 100.0f)
            CurrentScene = MainMenuScn
            DeltaTime = 0
            Drawables = Map.empty
        }
        
        Engine.start (config, gamestate)
        0 // return an integer exit code

