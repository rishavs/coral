
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