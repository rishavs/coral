namespace Coral

open System
open System.Diagnostics

open SFML.Audio
open SFML.Graphics
open SFML.System
open SFML.Window


module Engine =

    let start (config, gamestate) =
        let window = new RenderWindow(VideoMode(config.Width, config.Height), config.Title)

        let clock = Stopwatch()
        let font = new Font("assets/courier.ttf")

        // --------------------------------------------------
        // Load Function
        // --------------------------------------------------
        // This function is used to load assets and to generate 
        // display list. It runs only once.
        let loadEntities (gamestate: State): State =
            let myBalls =
                EnCircle(Color = Color.Green, Radius = 10.0f, Position = gamestate.Position)

            let fpsCounter  = EnFPS(Position = Vector2f(10.0f, 10.0f))
            let myBallPos   = EnText(Value = string gamestate.Position, Position = Vector2f(10.0f, 40.0f))

            let updatedEntities =
                Map.empty
                    .Add("myBalls", myBalls)
                    .Add("myFPS", fpsCounter)
                    .Add("myBallsPos", myBallPos)

            { gamestate with
                  Entities = updatedEntities }

        // --------------------------------------------------
        // Input Function
        // --------------------------------------------------
        // This function is called every tick.
        // It is used to capture all user inputs
        let getInput (gamestate: State): State =

            let mutable keys: Set<Keyboard.Key> = Set.empty
            if Keyboard.IsKeyPressed(Keyboard.Key.Up) then keys <- keys.Add Keyboard.Key.Up 
            if Keyboard.IsKeyPressed(Keyboard.Key.Right) then keys <- keys.Add Keyboard.Key.Right 
            if Keyboard.IsKeyPressed(Keyboard.Key.Down) then keys <- keys.Add Keyboard.Key.Down 
            if Keyboard.IsKeyPressed(Keyboard.Key.Left) then keys <- keys.Add Keyboard.Key.Left 
            
            {gamestate with KeysPressed = keys}

        // --------------------------------------------------
        // Update Function
        // --------------------------------------------------
        // This function is called every tick.
        // It is used to update the game state based on game logic and inputs
        let updateState (gamestate: State): State = 
            window.DispatchEvents()

            let mutable pos = gamestate.Position
            if gamestate.KeysPressed.Contains Keyboard.Key.Up 
                then pos <- (pos + Vector2f(0.0f, -1.0f))
            if gamestate.KeysPressed.Contains Keyboard.Key.Right 
                then pos <- (pos + Vector2f(1.0f, 0.0f))
            if gamestate.KeysPressed.Contains Keyboard.Key.Down 
                then pos <- (pos + Vector2f(0.0f, 1.0f))
            if gamestate.KeysPressed.Contains Keyboard.Key.Left 
                then pos <- (pos + Vector2f(-1.0f, 0.0f))
                       
            let mutable displayList = gamestate.Entities
            displayList <- displayList.Add ("myBalls", EnCircle(Color = Color.Red, Radius = 10.0f, Position = pos)) 
           
            if gamestate.KeysPressed.Count > 0 then 
                printfn "%A" gamestate.KeysPressed
                printfn "%A" pos
                printfn "%A" displayList.["myBalls"]

            {gamestate with Entities = displayList}

        // --------------------------------------------------
        // Draw Function
        // --------------------------------------------------
        // This function is called every tick.
        // It is used to draw all the entities in the Entities list
        let drawState (gamestate: State): State =
            printfn "%A" gamestate.Entities
            let renderLayer (entitiesMap: Map<string, Entity>) =
                entitiesMap
                |> Map.iter (fun id ent ->
                    match ent with
                    | EnText (value, position) ->
                        let txt = new Text(value, font)
                        txt.Position <- position
                        window.Draw txt
                    | EnFPS (position) ->
                        let fps = new Text("FPS: " + string gamestate.DeltaTime, font)
                        fps.Position <- position
                        window.Draw fps
                    | EnCircle (color, radius, position) ->
                        let shape = new CircleShape(radius, FillColor = color)
                        shape.Position <- position
                        window.Draw shape)

            match window.IsOpen with
            | false -> ()
            | true ->
                window.Clear()
                renderLayer gamestate.Entities
                window.Display()
            gamestate

        // --------------------------------------------------
        // Clock Function
        // --------------------------------------------------
        let setDeltaTime (gamestate: State) =
            let elapsed = int clock.ElapsedMilliseconds

            // Ensure that the min delta time is fixed to 16.67 or with a FPS of 60
            if elapsed < 16
            then System.Threading.Thread.Sleep(16 - elapsed)

            let dt = int clock.ElapsedMilliseconds
            clock.Restart()
            { gamestate with DeltaTime = dt }

        // --------------------------------------------------
        // Game Loop Function
        // --------------------------------------------------
        let rec runLoop (gamestate: State) =
            gamestate 
            |> getInput 
            |> updateState
            |> drawState
            |> setDeltaTime
            |> runLoop

        gamestate 
        |> loadEntities 
        |> runLoop
