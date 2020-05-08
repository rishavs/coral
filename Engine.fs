namespace Coral

open System;
open SFML.Audio;
open SFML.Graphics;
open SFML.System;
open SFML.Window;

type HandleKeyboard(window : RenderWindow) =
    let mutable keyState = Set.empty

    let keyPressedHandle = 
        window.KeyPressed.Subscribe(fun key -> 
            keyState <- keyState.Add key.Code)

    let keyReleasedHandle = 
        window.KeyReleased.Subscribe(fun key -> 
            keyState <- keyState.Remove key.Code)

    let validMovementKey (keyPress : Keyboard.Key) =
        match keyPress with
        | Keyboard.Key.Up
        | Keyboard.Key.Left
        | Keyboard.Key.Down
        | Keyboard.Key.Right -> true
        | _ -> false

    let keyToMovement (keyPress : Keyboard.Key) =
        match keyPress with
        | Keyboard.Key.Up    -> Vector2f( 0.0f, -1.0f)
        | Keyboard.Key.Left  -> Vector2f(-1.0f,  0.0f)
        | Keyboard.Key.Down  -> Vector2f( 0.0f,  1.0f)
        | Keyboard.Key.Right -> Vector2f( 1.0f,  0.0f)
        | _ -> Vector2f(0.0f, 0.0f)

    member this.IsKeyPressed (key : Keyboard.Key) = 
        keyState |> Set.contains key

    member this.GetMovement () =
        keyState
        |> Set.filter validMovementKey
        |> Seq.map keyToMovement
        |> Seq.fold (+) (Vector2f(0.0f, 0.0f))

    interface IDisposable with
        member this.Dispose() =
            keyPressedHandle.Dispose()
            keyReleasedHandle.Dispose()



module Engine = 

    let start (config, gamestate) =
        let window = new RenderWindow(VideoMode(config.Width, config.Height), config.Title)
        let clock = new Clock()
        let font = new Font("assets/courier.ttf")
        // let shape = new CircleShape(10.0f, FillColor = Color.Green)
        // let keyboard = new HandleKeyboard(window)

        // This function runs only once as is used to edit the display list
        let loadDrawables (gamestate: State): State = 
            let myBalls = EnCircle(color = Color.Green, radius = 10.0f, Position = gamestate.Position)
            let fpsCounter = EnFPS(Position = Vector2f(10.0f, 10.0f))
            let updatedDrawables = 
                Map.empty
                    .Add("myBalls", myBalls)
                    .Add("myFPS", fpsCounter)
            {gamestate with Drawables = updatedDrawables}

        // This function is called every tick. 
        // It is used to capture all user inputs
        let getInput (gamestate: State): State = 
            match window.IsOpen with
            | false ->  ()
            | true ->
                window.DispatchEvents()
                // if keyboard.IsKeyPressed Keyboard.Key.Escape then window.Close()
            gamestate

        // This function is called every tick. 
        // It is used to update the game state based on game logic and inputs
        let updateState (gamestate: State): State = 
            gamestate

        // This function is called every tick. 
        // It is used to draw all the entities in the drawables list
        let drawState (gamestate: State): State = 
            let renderLayer(entitiesMap: Map<string, Entity>) =
                entitiesMap
                |> Map.iter ( fun id ent ->
                    match ent with
                    | EnText (value, position)   -> 
                        let txt = new Text(value, font)
                        txt.Position <- position
                        window.Draw txt
                    | EnFPS (position)   -> 
                        let fps = new Text("FPS: " + string gamestate.DeltaTime, font)
                        fps.Position <- position
                        window.Draw fps
                    | EnCircle (color, radius, position) -> 
                        let shape = new CircleShape(radius, FillColor = color)
                        shape.Position <- position
                        window.Draw shape
                )

            match window.IsOpen with
            | false ->  ()
            | true ->
                window.Clear()
                renderLayer gamestate.Drawables
                window.Display()
            gamestate
        
        let setDeltaTime (gamestate: State) =
            let elapsed = clock.get_ElapsedTime().AsMilliseconds()
            
            // Ensure that the min delta time is fixed to 16.67 or with a FPS of 60
            if elapsed < 16 then System.Threading.Thread.Sleep(16 - elapsed)

            let dt = clock.Restart().AsMilliseconds()
            { gamestate with DeltaTime = dt}

        let rec runLoop (gamestate: State) =
                gamestate // 
                |> getInput         // update State based on game logic
                |> updateState      // update State based on game logic
                |> drawState        // draw calls
                |> setDeltaTime     // draw calls
                |> runLoop          // recurse
            
        gamestate
        |> loadDrawables
        |> runLoop
    