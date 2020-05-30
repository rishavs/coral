namespace Coral

open SFML.System
open SFML.Graphics
open SFML.Window

// ------------------------------------------------------
// Shared types and definitions go here
// ------------------------------------------------------
type Config =
    { Width: uint32
      Height: uint32
      Title: string }

// type EnText = {
//     Obj: SFML.Graphics.Text
//     IsVisible : bool
//     IsActive : bool
// }

// type EnCircle = {
//     Obj: SFML.Graphics.CircleShape
//     IsVisible : bool
//     IsActive : bool
// }

// type IText(str: string, fnt:Font, isVisible:bool, isActive:bool) = 
//     inherit SFML.Graphics.Text(str, fnt)
//     member this.IsVisibale:bool = isVisible
//     member this.IsActive: bool = isActive

// type ICircle(r: float32, isvisible:bool, isactive:bool) = 
//     inherit SFML.Graphics.CircleShape(r)
//     member this.IsVisible = isvisible
//     member this.IsActive = isactive

type Entity =
    | EnCircle of SFML.Graphics.CircleShape
    | EnText of SFML.Graphics.Text

type UserCommands =
    | MoveUp
    | MoveRight
    | MoveDown
    | MoveLeft
    | CloseGame
    | ToggleDebugHUD


type AvailableScenes =
    | MainMenuScn
    | GameScn

type State = { 
    BallPosition: Vector2f
    CurrentScene: AvailableScenes
    DeltaTime: int
    TickCount: int
    ShowDebugHUD: bool
    CurrentTickLoadsNewEntities: bool
    CurrentTickUpdatesDrawables: bool
    CurrentTickSetsupScene: bool
    UserCommandsList : Set<UserCommands>
    KeysPressed: Set<Keyboard.Key>
    Entities: Map<string, Entity>
}

type HandledKeys =
    | Up
    | Right
    | Down
    | Left

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
