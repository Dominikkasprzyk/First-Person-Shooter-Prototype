# First Person Shooter - prototype in Unity

First Person Shooter game protype utilizing event based architecture with the use of unity events, unity actions and custom game events to ensure modularity and scalabilty.

## Table of Contest

* [Controls](#Controls)
* [Prototype contents](#Prototype-contents)
* [Event based architecture](#Event-based-architecture)
* [Technologies](#Technologies)

## Controls

#### Keyboard & Mouse
Classic FPS controls:
Move mouse to look and turn around. Use AWSD keys to move your character. To jump press SPACEBAR key. To attack press left mouse button. Use mouse scroll to change your weapon.

## Prototype contents

* Classic FPS movement (see [controls](#controls)),
* Destroyable objects,
* Different weapons,
* Fabric system (specific weapons can destroy specific materials),
* Tutorial scene,
* Rotating weapons (facing whatever you are aiming at),
* Weapon dependent crosshair system (differen weapon - differen crosshair).

  ### Cosmetic elements used in this prototype:
* [Lasers and explosions particles](https://assetstore.unity.com/packages/vfx/particles/spells/rpg-vfx-bundle-133704),
* [Weapon models](https://assetstore.unity.com/packages/3d/props/weapons/weapon-master-scifi-weapon-1-lite-134423),
* [Sound effects](https://assetstore.unity.com/packages/audio/sound-fx/shooting-sound-177096),
* [Health bars](https://assetstore.unity.com/packages/2d/gui/icons/elemental-meters-173133),
* [Font](https://assetstore.unity.com/packages/2d/fonts/free-pixel-font-thaleah-140059).

## Event based architecure

Using events allows for flexible, modular and higly scalable games. This approach allows you completly seperate different parts of your game avoiding errors when implementning new mechanics and refactoring old bits of code.

### This prototype utilizes:
* Unity Events - allowing for connecting specific actions from inspector (for example instantiating explosion when object is destroyed), 
* Unity Actions - allowing for listening and reacting to events from specific broadcaster (for example PlayerMovement script listens to InputManager events to know when to move player's character),
* Game Events (custom scriptable objects) - allowing for listeners to react to an event without him knowing anything about the broadcaster and without broadcaster knowing about the listener (for example when the weapon type is changed, event is raised without specific addressee. Next UI Manager changes player's crosshair without the need of watching any specific broadcaster),

### More about this type of architecture can be found here:
* [Game Architecture with Scriptable Objects](https://youtu.be/raQ3iHhE_Kk?si=w9i9lRURwXUbgs6x),
* [Game architecture with ScriptableObjects](https://youtu.be/WLDgtRNK2VE?si=GlHQINsp48bGBXC7).


## Technologies
Unity version 2021.3.5f1, C# programming language
