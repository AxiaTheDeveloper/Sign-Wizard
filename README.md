# Sign Wizard
 
2nd Place Pengembangan Aplikasi Permainan Gemastik 2023 & Winner of GameXcellence 2023 Technical Excellence Award.
Unity 2021.3.10f1

[Sign Wizard Itch.io Link](https://dhyox.itch.io/signwizard) **(Windows Only)**


(programmer notes ('25)) -> The newest one of this code is branch: main-ver-2.0.4

For my 2nd project at my internship, we developed this game for Gemastik Game dev competition 2023.

For this project I implemented SRP of SOLID principle. 

For optimization: used Comparetag for comparing collider.
For game programming pattern: I used singleton for managers and state pattern for the game state control.
For design pattern: I still only used model view pattern.

It's a rpg game where you make potions by using sign language. The main mechanic for using the sign language to get ingredients, make the potions, or use the mortar and pestle, and the mechanics to move the foot placement in the river puzzle is like using the typing mechanic but instead normal words that you saw on the screen, it's sign language.

It's my 2nd time creating the dialogue system (I took from my 1st project) and using the timeline system. The dialogue system is still really messy because even if I tried to use SO for storing the dialogue. I still need to make a separate game object because each code saves the characters data (character dialogue sprite.

For the tools and frameworks outside normal framework from unity, we used: Cinemachine & LeanTween

Refactoring of the code is still in progress. Some main things I want to change:
1. using newer experiences when updating the dialogue system for other games I developed, I will update it so the dialogue system is just there as a manager to listen if others asked them to play dialogue of dialoguetitle while looking for the data (not just the dialogue line but also the character sprite) in the scriptable objects. Therefore, removing the messy dialogue system.
2. Rework many tight couplings using events and SOLID principles (Interfaces, abstracts, etc). Example: interactObject & inventoryUI
