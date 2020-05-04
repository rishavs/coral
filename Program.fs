// Learn more about F# at http://fsharp.org

open System
open SFML.Graphics
open SFML.Window
open SFML.System

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

type SomeState = { 
    Position : Vector2f;
    }

let startGame() =
    use window = new RenderWindow(VideoMode(1600u, 900u), "SFML works!")
    use shape = new CircleShape(10.0f, FillColor = Color.Green)
    use keyboard = new HandleKeyboard(window)

    window.Closed.Add(fun evArgs -> window.Close())

    let rec mainLoop state =
        window.DispatchEvents()

        if keyboard.IsKeyPressed Keyboard.Key.Escape then
            window.Close()

        let newPosition = state.Position + keyboard.GetMovement()
        shape.Position <- newPosition

        if window.IsOpen then
            window.Clear()
            window.Draw(shape)
            window.Display()

            mainLoop {Position = newPosition}

    mainLoop {Position = Vector2f(0.0f, 0.0f)}

startGame()

// [<EntryPoint>]
// let main argv =
//     printfn "Hello World from F#!"
//     0 // return an integer exit code

