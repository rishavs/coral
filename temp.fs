
// type HandleKeyboard(window: RenderWindow) =
//     let mutable keyState = Set.empty

//     let keyPressedHandle =
//         window.KeyPressed.Subscribe(fun key -> keyState <- keyState.Add key.Code)

//     let keyReleasedHandle =
//         window.KeyReleased.Subscribe(fun key -> keyState <- keyState.Remove key.Code)

//     let validMovementKey (keyPress: Keyboard.Key) =
//         match keyPress with
//         | Keyboard.Key.Up
//         | Keyboard.Key.Left
//         | Keyboard.Key.Down
//         | Keyboard.Key.Right -> true
//         | _ -> false

//     let keyToMovement (keyPress: Keyboard.Key) =
//         match keyPress with
//         | Keyboard.Key.Up -> Vector2f(0.0f, -1.0f)
//         | Keyboard.Key.Left -> Vector2f(-1.0f, 0.0f)
//         | Keyboard.Key.Down -> Vector2f(0.0f, 1.0f)
//         | Keyboard.Key.Right -> Vector2f(1.0f, 0.0f)
//         | _ -> Vector2f(0.0f, 0.0f)

//     member this.IsKeyPressed(key: Keyboard.Key) = keyState |> Set.contains key

//     member this.GetMovement() =
//         keyState
//         |> Set.filter validMovementKey
//         |> Seq.map keyToMovement
//         |> Seq.fold (+) (Vector2f(0.0f, 0.0f))

//     interface IDisposable with
//         member this.Dispose() =
//             keyPressedHandle.Dispose()
//             keyReleasedHandle.Dispose()

// --------------------------------

            // let validKeys = [Keyboard.Key.Up; Keyboard.Key.Down; Keyboard.Key.Left; Keyboard.Key.Right]
            // let mySet = (Set.empty or mySet,[keys to test]) ||> List.fold (fun set key -> if Keyboard.IsKeyPressed(key) then set.Add key else set)
            
            // let mySet = 
                
            //     List.fold (fun set key -> 
            //         if Keyboard.IsKeyPressed(key) then set.Add key else set
            //     ) Set.empty validKeys
// --------------------------------

            // let updateEntities (entitiesMap: Map<string, Entity>) =
            //     gamestate.Entities
            //     |> Map.map (fun id ent ->
            //         match id with
            //         | "myBalls" -> gamestate.Entities
            //         | _ -> gamestate.Entities
            //         )



            // let mutable fpsVal:string = "FPS: " + string gamestate.DeltaTime
            

            // let updated displatList = {gamestate.Entities with }     
            
            // let keyToMovement (keyPress: Keyboard.Key) =
            //     match keyPress with
            //     | Keyboard.Key.Up -> Vector2f(0.0f, -1.0f)
            //     | Keyboard.Key.Left -> Vector2f(-1.0f, 0.0f)
            //     | Keyboard.Key.Down -> Vector2f(0.0f, 1.0f)
            //     | Keyboard.Key.Right -> Vector2f(1.0f, 0.0f)
            //     | _ -> Vector2f(0.0f, 0.0f)

            // // member this.IsKeyPressed(key: Keyboard.Key) = keyState |> Set.contains key

            // let GetMovement() =
            //     keyState
            //     |> Set.filter validMovementKey
            //     |> Seq.map keyToMovement
            //     |> Seq.fold (+) (Vector2f(0.0f, 0.0f))



//             namespace Coral

// open System
// open System.Diagnostics

// open SFML.Audio
// open SFML.Graphics
// open SFML.System
// open SFML.Window


// module Engine =

//     let start (config, gamestate) =
//         let window = new RenderWindow(VideoMode(config.Width, config.Height), config.Title)

//         let clock = Stopwatch()
//         let font = new Font("assets/courier.ttf")

//         // --------------------------------------------------
//         // Load Function
//         // --------------------------------------------------
//         // This function is used to load assets and to generate 
//         // display list. It runs only once.
//         let loadEntities (gState: State): State =
//             let myBalls =
//                 EnCircle(Color = Color.Green, Radius = 10.0f, Position = gState.Position)

//             let fpsCounter  = EnFPS(Position = Vector2f(10.0f, 10.0f))
//             let myBallPos   = EnText(Value = string gState.Position, Position = Vector2f(10.0f, 40.0f))

//             let updatedEntities =
//                 Map.empty
//                     .Add("myBalls", myBalls)
//                     .Add("myFPS", fpsCounter)
//                     .Add("myBallsPos", myBallPos)

//             { gState with
//                   Entities = updatedEntities }

//         // --------------------------------------------------
//         // Input Function
//         // --------------------------------------------------
//         // This function is called every tick.
//         // It is used to capture all user inputs
//         let getInput (gState: State): State =

//             let mutable keys: Set<Keyboard.Key> = Set.empty
//             if Keyboard.IsKeyPressed(Keyboard.Key.Up) then keys <- keys.Add Keyboard.Key.Up 
//             if Keyboard.IsKeyPressed(Keyboard.Key.Right) then keys <- keys.Add Keyboard.Key.Right 
//             if Keyboard.IsKeyPressed(Keyboard.Key.Down) then keys <- keys.Add Keyboard.Key.Down 
//             if Keyboard.IsKeyPressed(Keyboard.Key.Left) then keys <- keys.Add Keyboard.Key.Left 
            
//             {gState with KeysPressed = keys}

//         // --------------------------------------------------
//         // Update Function
//         // --------------------------------------------------
//         // This function is called every tick.
//         // It is used to update the game state based on game logic and inputs
//         let updateState (gState: State): State = 
//             window.DispatchEvents()

//             let mutable pos = gState.Position
//             if gState.KeysPressed.Contains Keyboard.Key.Up 
//                 then pos <- (pos + Vector2f(0.0f, -1.0f))
//             if gState.KeysPressed.Contains Keyboard.Key.Right 
//                 then pos <- (pos + Vector2f(1.0f, 0.0f))
//             if gState.KeysPressed.Contains Keyboard.Key.Down 
//                 then pos <- (pos + Vector2f(0.0f, 1.0f))
//             if gState.KeysPressed.Contains Keyboard.Key.Left 
//                 then pos <- (pos + Vector2f(-1.0f, 0.0f))
                       
//             let mutable displayList = gState.Entities
//             displayList <- displayList.Add ("myBalls", EnCircle(Color = Color.Red, Radius = 10.0f, Position = pos)) 
           
//             // if gState.KeysPressed.Count > 0 then 
//             //     printfn "%A" gState.KeysPressed
//             //     printfn "%A" pos
//             //     printfn "%A" displayList.["myBalls"]

//             {gState with Entities = displayList}

//         // --------------------------------------------------
//         // Draw Function
//         // --------------------------------------------------
//         // This function is called every tick.
//         // It is used to draw all the entities in the Entities list
//         let drawState (gState: State): State =

//             let renderLayer (entitiesMap: Map<string, Entity>) =
//                 entitiesMap
//                 |> Map.iter (fun id ent ->
//                     match ent with
//                     | EnText (value, position) ->
//                         let txt = new Text(value, font)
//                         txt.Position <- position
//                         window.Draw txt
//                     | EnFPS (position) ->
//                         let fps = new Text("FPS: " + string gState.DeltaTime, font)
//                         fps.Position <- position
//                         window.Draw fps
//                     | EnCircle (color, radius, position) ->
//                         let shape = new CircleShape(radius, FillColor = color)
//                         shape.Position <- position
//                         window.Draw shape)

//             match window.IsOpen with
//             | false -> ()
//             | true ->
//                 window.Clear()
//                 renderLayer gState.Entities
//                 window.Display()
//             gState

//         // --------------------------------------------------
//         // Clock Function
//         // --------------------------------------------------
//         let setDeltaTime (gState: State) =
//             let elapsed = int clock.ElapsedMilliseconds

//             // Ensure that the min delta time is fixed to 16.67 or with a FPS of 60
//             if elapsed < 16
//             then System.Threading.Thread.Sleep(16 - elapsed)

//             let dt = int clock.ElapsedMilliseconds
//             clock.Restart()
//             { gState with DeltaTime = dt }

//         // --------------------------------------------------
//         // Game Loop Function
//         // --------------------------------------------------
//         let rec runLoop (gState: State) =
//             gState 
//             |> getInput 
//             |> updateState
//             |> drawState
//             |> setDeltaTime
//             |> runLoop

//         gamestate 
//         |> loadEntities 
//         |> runLoop
