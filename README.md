# First Person Shooter - prototype in Unity

First Person Shooter game prototype utilizing event-based architecture with the use of unity events, unity actions, and custom game events to ensure modularity and scalability.

## Table of Contents

* [Controls](#Controls)
* [Prototype contents](#Prototype-contents)
* [Event-based architecture](#Event-based-architecture)
* [Technologies](#Technologies)

## Controls

#### Keyboard & Mouse
Classic FPS controls:
Move the mouse to look and turn around. Use AWSD keys to move your character. To jump press the SPACEBAR key. To attack press the left mouse button. Use the mouse scroll to change your weapon.

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

## Event-based architecure

The event-based architecture allows for flexible and highly scalable games by separating projects into different modules. Communication between those modules happens via events which helps to avoid errors when implementing new mechanics or refactoring old bits of code. A new module can be simply connected to others by listening or raising events, while other modules remain untouched. This modular approach also improves code reusability and helps to pinpoint problems during debugging.

### This prototype utilizes:
* Unity Events - allowing for connecting specific actions from the inspector (for example instantiating explosion when the object is destroyed), 
* Unity Actions - allowing for listening and reacting to events from specific broadcaster (for example PlayerMovement script listens to InputManager events to know when to move a player's character),
* Game Events (custom scriptable objects) - allowing for listeners to react to an event without knowing anything about the broadcaster and without the broadcaster knowing about the listener (for example when the weapon type is changed, the event is raised without a specific addressee. Next UI Manager changes player's crosshair without the need of watching any specific broadcaster).

### More about this type of architecture can be found here:
* [Game Architecture with Scriptable Objects](https://youtu.be/raQ3iHhE_Kk?si=w9i9lRURwXUbgs6x),
* [Game architecture with ScriptableObjects](https://youtu.be/WLDgtRNK2VE?si=GlHQINsp48bGBXC7).


## Technologies
Unity version 2021.3.5f1, C# programming language
