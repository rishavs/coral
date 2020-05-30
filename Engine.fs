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
        let setupScene (gState: State): State =
            let fps     = EnText ("FPS: 0", font, Position = Vector2f(10.0f, 10.0f))
            let ticks   = EnText ("Ticks: 0", font, Position = Vector2f(10.0f, 50.0f))
            let ballPos = EnText ("Ball Position: 0, 0", font, Position = Vector2f(10.0f, 100.0f))
            // let fps = EnText {
            //     Obj = fpsDrawable
            //     IsVisible = true 
            //     IsActive = true
            // }
            // let ticksDrawable = new SFML.Graphics.Text("Ticks: 0" , font)
            // ticksDrawable.Position <- Vector2f(10.0f, 50.0f)
            // let ticks = EnText {
            //     Obj = ticksDrawable
            //     IsVisible = true 
            //     IsActive = true
            // }
            // let ballPosDrawable = new SFML.Graphics.Text("Ball Position: " + string(Vector2f(0.0f, 0.0f)), font)
            // ballPosDrawable.Position <- Vector2f(10.0f, 100.0f)
            // let ballPos = EnText {
            //     Obj = ballPosDrawable
            //     IsVisible = true 
            //     IsActive = true
            // }
                       
            let updatedEntities =
                Map.empty
                    .Add("FPSDebugHUD", fps)
                    .Add("TicksDebugHUD", ticks)
                    .Add("BallsPos", ballPos)

            { gState with
                  Entities = updatedEntities }

        // --------------------------------------------------
        // Input Function
        // --------------------------------------------------
        // This function is called every tick.
        // It is used to capture all user inputs
        let getInput (gState: State): State =
            window.DispatchEvents()
            let mutable commands: Set<UserCommands> = Set.empty
            if Keyboard.IsKeyPressed(Keyboard.Key.Up) then commands <- commands.Add MoveUp
            if Keyboard.IsKeyPressed(Keyboard.Key.Right) then commands <- commands.Add MoveRight 
            if Keyboard.IsKeyPressed(Keyboard.Key.Down) then commands <- commands.Add MoveDown 
            if Keyboard.IsKeyPressed(Keyboard.Key.Left) then commands <- commands.Add MoveLeft 
            if Keyboard.IsKeyPressed(Keyboard.Key.Escape) then commands <- commands.Add CloseGame 
            if Keyboard.IsKeyPressed(Keyboard.Key.F1) then commands <- commands.Add ToggleDebugHUD 
            
            {gState with UserCommandsList = commands}

        // --------------------------------------------------
        // Update Function
        // --------------------------------------------------
        // This function is called every tick. Based on the command, it adds, 
        // It is used to update the game state based on game logic and inputs
        let updateDisplayList (gState: State): State = 
            gState
        // --------------------------------------------------
        // Update Function
        // --------------------------------------------------
        // This function is called every tick.
        // It is used to update the game state based on game logic and inputs
        let updateState (gState: State): State = 

            let mutable entitiesMap = gState.Entities
            // let fpsObj = entitiesMap.["fps"]
            // fpsObj.Obj <- "x"

            // if not (entitiesMap.ContainsKey("fpsHUD") ) && gState.ShowDebugHUD 
            // then 
            //     let fpsHUD  = EnFPS(Position = Vector2f(10.0f, 10.0f))

            let mutable pos = gState.BallPosition
            if gState.UserCommandsList.Contains MoveUp
            then pos <- (pos + Vector2f(0.0f, -1.0f))
            
            if gState.UserCommandsList.Contains MoveRight 
            then pos <- (pos + Vector2f(1.0f, 0.0f))
        
            if gState.UserCommandsList.Contains MoveDown 
            then pos <- (pos + Vector2f(0.0f, 1.0f))
    
            if gState.UserCommandsList.Contains MoveLeft 
            then pos <- (pos + Vector2f(-1.0f, 0.0f))

            if gState.UserCommandsList.Contains CloseGame 
            then window.Close()

            let debugHUDState = 
                if gState.UserCommandsList.Contains ToggleDebugHUD 
                then not gState.ShowDebugHUD else gState.ShowDebugHUD

            {gState with TickCount = gState.TickCount + 1; BallPosition = pos; ShowDebugHUD = debugHUDState}

        // --------------------------------------------------
        // Draw Function
        // --------------------------------------------------
        // This function is called every tick.
        // It is used to draw all the entities in the Entities list
        let drawState (gState: State): State =
            let renderLayer (entities: Map<string, Entity>) =
                entities
                |> Map.iter (fun id ent ->
                    match ent with
                    | IText ->
                        window.Draw ent
                    | _ -> ()
                )

   
            window.Clear()
            renderLayer gState.Entities
            window.Display()
                
            gState

        // --------------------------------------------------
        // Clock Function
        // --------------------------------------------------
        let setDeltaTime (gState: State) =
            let elapsed = int clock.ElapsedMilliseconds
            let tickDuration = 16

            // Ensure that the min delta time is fixed to 16.67 or with a FPS of 60
            if elapsed < tickDuration
            then System.Threading.Thread.Sleep(tickDuration - elapsed)

            let dt = int clock.ElapsedMilliseconds
            clock.Restart()
            { gState with DeltaTime = dt }

        // --------------------------------------------------
        // Game Loop Function
        // --------------------------------------------------
        let rec runLoop (gState: State) =
            gState 
            |> getInput
            |> updateState
            |> drawState
            |> setDeltaTime
            |> runLoop

        gamestate
        |> setupScene
        |> runLoop 