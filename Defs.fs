namespace Coral
open SFML.System
open System.Timers

// ------------------------------------------------------
// Shared types and definitions go here
// ------------------------------------------------------
type Config = {
    Width: uint32
    Height: uint32
    Title: string
}

type Entity =
    | EnText of value : string * Position: Vector2f
    | EnFPS of Position: Vector2f
    | EnCircle of color:SFML.Graphics.Color * radius: float32 * Position: Vector2f

// type DrawablesList = Map<string, Entity>


type AvailableScenes =
    | MainMenuScn
    | GameScn

type State = {
    Position    : Vector2f
    CurrentScene: AvailableScenes
    DeltaTime   : int
    Drawables   : Map<string, Entity>
}
// type BtnState =
//     | Default of Color
//     | Hover of Color
//     | Click of Color


    // | EnSprite of texture: Love.Texture * x: float32 * y: float32 
//     | EnBtn of width: float32 * height : float32 * btnState : BtnState * x: float32 * y: float32

// type DrawableEntities = {
//     mutable BgLayer  : Dictionary<string, Entity> 
//     mutable ObjLayer : Dictionary<string, Entity>  
//     mutable UiLayer  : Dictionary<string, Entity> 
//     mutable DbgLayer : Dictionary<string, Entity> 
// } 

// type Resources = { 
//     Scene   : AvailableScenes
//     Speed   : float32
//     Img     : Image
// }


// module Data = 
//     // ------------------------------------------------------
//     // Constants and statis resources are declared here
//     // ------------------------------------------------------
//     let Res = { 
//         Scene   = GameScn
//         Speed   = 300.0f
//         Img     = Graphics.NewImage "assets/red_ball.png"
//     }    

//     // ------------------------------------------------------
//     // Mutable game state goes in here
//     // ------------------------------------------------------
//     let ResetModel(): Model = {
//         CurrentScene                = MainMenuScn 
//         Counter                     = 0
//         FPS                         = 0.0f
//         TimeDelta                   = 0.0f
//         BallPos                     = Vector2(300.0f, 300.0f)
//         MainMenuStartBtnState       = Default (Color.AliceBlue) 
//         MainMenuSettingsBtnState    = Default (Color.AliceBlue)
//         MainMenuExitBtnState        = Default (Color.AliceBlue)
//     }
//     let mutable State = ResetModel() 
    
//     // ------------------------------------------------------
//     // Mutable display list goes in here
//     // ------------------------------------------------------
//     let ResetDisplayList() = {
//         BgLayer  = Dictionary<string, Entity>()  
//         ObjLayer = Dictionary<string, Entity>()  
//         UiLayer  = Dictionary<string, Entity>() 
//         DbgLayer = Dictionary<string, Entity>() 
//     } 
//     let mutable DisplayList = ResetDisplayList()
    
//     // indexes for faster lookup of interactive elements
//     let entityClickable = seq<string>
//     let entityHoverable = seq<string>

